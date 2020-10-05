using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Newtonsoft.Json;
using WEBAPI.Models;
using WEBAPIQR.Models;

//returning custom made classes is to prevent loop check for navigation over the original classes when sent as json
//plus that these navigations are used for performance on EFcore as well as security and anti code injection

namespace QRcodeAuthentication.Controllers {
    [ApiController]
    [Route ("QRcode")]
    public class QRcodeController : ControllerBase {
        private qrContext db = new qrContext ();

        [HttpPost]
        [Route ("loginAuthentication")]
        public ActionResult Authentication (Person param) {
            try
            {
                Person person = db.Person.Where(p => p.UserName.Equals(param.UserName)).FirstOrDefault();
                if (person != null)
                {
                    if (person.Password.Equals(param.Password))
                    {

                        if (person.Type.Equals(1))
                        {
                            Professor p = db.Professor.Where(p => p.IdProfessor.Equals(person.IdPerson)).FirstOrDefault();
                            //return JsonConvert.SerializeObject(p, new JsonSerializerSettings()
                            //{
                            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            //});
                            return Ok(new responseProfessor(p, person.Type));
                            //professor page
                        }
                        else if (person.Type.Equals(2))
                        {
                            //return JsonConvert.SerializeObject(person, new JsonSerializerSettings()
                            //{
                            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            //});
                            return Ok(new responseAdmin(person));
                            //admin page
                        }
                        else
                        {
                            //studentpage
                            Student s = db.Student.Where(s => s.IdStudent.Equals(person.IdPerson)).FirstOrDefault();
                            //return JsonConvert.SerializeObject(s, new JsonSerializerSettings()
                            //{
                            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            //});
                            return Ok(new responseStudent(s, person.Type));
                        }
                    }
                    else
                    {
                        return NotFound(new { results = "password not correct" });
                        //return "password not correct";
                    }
                }
                else
                {
                    return NotFound(new { results = "username not found" });
                    //return "username not found";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route("getSchedule")]
        public object getSchedule(scheduleStudent sp) //only id that is just not to create a new object for integer
        {
            try
            {
                List<Enrollment> ls = db.Enrollment.Where(e => e.IdStudent == sp.IdStudent).ToList();
                List<Schedule> Allschedule = new List<Schedule>();
                Student student = db.Student.Find(sp.IdStudent);
                Allschedule = db.Schedule.Where(s => s.IdLevel == student.IdLevel).ToList();
                List<Schedule> forStudent = new List<Schedule>();

                foreach (Schedule item in Allschedule)
                {
                    foreach (Enrollment s in ls)
                    {

                        if (item.IdCourse == s.IdCourse)
                        {
                            forStudent.Add(item);
                        }
                    }
                }

                if (forStudent.Count == 0)
                {
                    return NotFound(new { results = "Sorry No Schedules For You Try Contacting The Admin !" });
                }
            return forStudent;
        }catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("getGrades")]
        public object getGrades (int idStudent) {
            try { 
            if (!db.Studentattendance.Where(x => x.IdStudent.Equals(idStudent)).ToList().Any())
            {
                return NotFound(new { results = "Sorry Can't See Any Attendance For YOU Try Contacting The Admin!" });
            }
            return db.Studentattendance.Where (x => x.IdStudent.Equals (idStudent)).ToList ();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("getSchedulesProf")]
        //this gets a prof's  list of Schedules
        public object getProfSchedules (Professor p) //just for the id
        {
            try { 
            List<Instruction> listI = db.Instruction.Where (i => i.IdProfessor.Equals (p.IdProfessor)).ToList ();
            List<Schedule> listS = new List<Schedule> ();
            foreach (Instruction i in listI) {
                listS.AddRange (db.Schedule.Where (s => s.IdCourse.Equals (i.IdCourse)).ToList ());
            }
            List<CustomScheduleForName> listCS = new List<CustomScheduleForName> ();
            foreach (Schedule s in listS) {
                string NameClass = db.Class.Find (s.IdClass).Name;
                string NameCourse = db.Course.Find (s.IdCourse).Name;
                listCS.Add (new CustomScheduleForName (s.IdSchedule, NameClass, s.IdClass, s.IdCourse, s.Day, s.Time, NameCourse, s.IdLevel));
            }
            if (listS.Count == 0)
            {
                return NotFound(new { results = "Sorry no schedules for You Try Contacting The Admin !" });
            }
            return listCS;
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        //--------------------------
        [HttpPost ("code")]
        [Route ("GenerateQRString")]
        //this is called after that a list of schedules is shown for the professor
        public object GenerateQRString (QRStringStructure qrs) { //contains idClass and Course Time and Date : 08/18/2018 07:22:16
            try{
            string code = "";
            DayOfWeek DayWeek = DayOfWeek.Monday;
            switch (qrs.Day) {
                case "Monday":
                    break;
                case "Tuesday":
                    DayWeek = DayOfWeek.Tuesday;
                    break;
                case "Wednesday":
                    DayWeek = DayOfWeek.Wednesday;
                    break;
                case "Thursday":
                    DayWeek = DayOfWeek.Thursday;
                    break;
                case "Friday":
                    DayWeek = DayOfWeek.Friday;
                    break;
                case "Saturday":
                    DayWeek = DayOfWeek.Saturday;
                    break;
                case "Sunday":
                    DayWeek = DayOfWeek.Sunday;
                    break;
            }
            if (DateTime.Now.DayOfWeek != DayWeek ||
                (DateTime.Now.TimeOfDay - TimeSpan.Parse (qrs.Time) > TimeSpan.Parse ("01:15:00")) ||
                (DateTime.Now.TimeOfDay - TimeSpan.Parse (qrs.Time) < TimeSpan.Parse ("00:00:00"))) // if it's the same day of week and within an hour and a quarter of the given time
            {
                return NotFound(new { results = "Please check your selection ! The problem is that you entered the wrong schedule corresponding to the current time" });
            } //checking if the day and time chosen are valid with the current Date and Time
            code += qrs.idClass + ";" + qrs.idCourse + ";" + DateTime.Now.ToString (); //to use tokennizer with ; as delimiter rather in angular or modify TakeAttendance to take the QRString
            if (!db.Professorattendance.AsEnumerable ().Where (PA => PA.IdClass == qrs.idClass &&
                    PA.IdProfessor == qrs.idProfessor &&
                    PA.Date.DayOfYear == DateTime.Now.DayOfYear &&
                    PA.Date.Year == DateTime.Now.Year &&
                    PA.Date.TimeOfDay.Subtract (DateTime.Now.TimeOfDay).CompareTo (TimeSpan.Parse ("01:15:00")) < 0 &&
                    PA.Date.TimeOfDay.Subtract (DateTime.Now.TimeOfDay).CompareTo (TimeSpan.Parse ("00:00:00")) >= 0).Any ()) {
                db.Professorattendance.Add (new Professorattendance (qrs.idClass, qrs.idProfessor, DateTime.Now));
                db.SaveChanges ();
            }
            return new StringClass (code);
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        } //date time should be as yyyy/mm/dd hh:mm:ss for the scan to be able to convert it back to a datetime object

        [HttpPost]
        [Route ("AddStudentGrades")]
        public object AddGrades (listStAContainer c) {
            try { 
            foreach (responseStudentAttendance StA in c.listStA) {
                Studentattendance studentAttendance = db.Studentattendance.Where (
                    x => x.IdClass.Equals (StA.IdClass) &&
                    x.IdCourse.Equals (StA.IdCourse) && x.IdStudent.Equals (StA.IdStudent) &&
                    x.Date.Equals (StA.Date)).FirstOrDefault ();
                studentAttendance.Grade = StA.Grade;
                db.Studentattendance.Update (studentAttendance);
            }
            db.SaveChanges ();
            return Ok();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpGet]
        [Route ("GenerateExcelFile")]
        public object getExcelFile (string IdCourse, string date) { //format dd/MM/yyyy
            try { 
            System.Text.Encoding.RegisterProvider (System.Text.CodePagesEncodingProvider.Instance);
            List<Schedule> listS = db.Schedule.AsEnumerable ()
                .Where (s => s.IdCourse == IdCourse && s.Day == DateTime.ParseExact (date, "dd/MM/yyyy", null).DayOfWeek.ToString ()).ToList ();

            String[] split = date.Split ('/');
            string folderPath = "xclAttendance\\" + split[2] + "\\" + split[1] + "\\" + split[0] + "\\" + IdCourse;
                if (listS.Count == 0)
                {
                    return NotFound(new { results = "No Sessions on that date for that course !" });
                }
            foreach (Schedule sc in listS) {
                int i = 2;
                using (var workbook = new XLWorkbook ()) {
                    var worksheet = workbook.Worksheets.Add ("Sample Sheet");
                    worksheet.Cell ("A1").Value = "Student First Name";
                    worksheet.Cell ("B1").Value = "Student Last Name";
                    worksheet.Cell ("C1").Value = "Student ID";
                    worksheet.Cell ("D1").Value = "Student Grade";
                    List<Studentattendance> list = db.Studentattendance.AsEnumerable ()
                        .Where (
                            x =>
                            x.IdCourse.Equals (IdCourse) &&
                            x.Date.ToString ("dd/MM/yyyy").Equals (date) &&
                            x.Date.TimeOfDay - TimeSpan.Parse (sc.Time) < TimeSpan.Parse ("01:15:00") &&
                            x.Date.TimeOfDay - TimeSpan.Parse (sc.Time) > TimeSpan.Parse ("00:00:00")
                        ).ToList ();
                    Student s;
                    foreach (Studentattendance sa in list) {
                        s = db.Student.Find (sa.IdStudent);
                        worksheet.Cell ("A" + i).Value = s.FirstName;
                        worksheet.Cell ("B" + i).Value = s.LastName;
                        worksheet.Cell ("C" + i).Value = s.IdStudent;
                        worksheet.Cell ("D" + i).Value = sa.Grade;
                        i++;
                    }
                    String[] splitTime = sc.Time.Split (':');
                    String FolderStructure = folderPath + "\\" + splitTime[0] + "h " + splitTime[1] + "m " + splitTime[2] + "s";
                    workbook.SaveAs (FolderStructure + ".xlsx");
                }
            }
            db.DeleteZipFile ();
            ZipFile.CreateFromDirectory (folderPath, "result.zip");
            return new WEBAPIQR.Models.FileResult ("demo.zip", Directory.GetCurrentDirectory () + "\\result.zip", "application/zip");
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("TakeAttendance")]
        public object TakeAttendance (QR_idStudent objec) {
            try { 
            string[] QRSplitted = objec.QRCode.Split (';'); //class;course;dateTime
            string dateTime = QRSplitted[2];
            string IdCourse = QRSplitted[1];
            string IdClass = QRSplitted[0];
            if (DateTime.Now.DayOfWeek != DateTime.Parse (dateTime).DayOfWeek ||
                ((DateTime.Now.TimeOfDay - DateTime.Parse (dateTime).TimeOfDay) > TimeSpan.Parse ("01:15:00")) ||
                ((DateTime.Now.TimeOfDay - DateTime.Parse (dateTime).TimeOfDay) < TimeSpan.Parse ("00:00:00")) ||
                !db.Enrollment.Where (e => e.IdStudent.Equals (objec.IdStudent) && e.IdCourse.Equals (IdCourse)).Any ())
            // if it's the same day of week and within an hour and a quarter of the given time and if the student is enrolled on this course
            {
                return NotFound (new { results = "ERROR YOU ARE CHEATING !!!" });
            }
            //check if the student is added to the session before
            if (!db.Studentattendance.AsEnumerable ().Where (sa => (sa.Date.DayOfYear == DateTime.Parse (dateTime).DayOfYear) &&
                    (sa.Date.Year == DateTime.Parse (dateTime).Year) &&
                    (sa.Date.DayOfYear == DateTime.Parse (dateTime).DayOfYear) &&
                    (sa.Date.TimeOfDay.Subtract (DateTime.Parse (dateTime).TimeOfDay).CompareTo (TimeSpan.Parse ("01:15:00")) < 0) &&
                    (sa.Date.TimeOfDay.Subtract (DateTime.Parse (dateTime).TimeOfDay).CompareTo (TimeSpan.Parse ("00:00:00")) >= 0) &&
                    (sa.IdClass == IdClass) &&
                    (sa.IdCourse == IdCourse) &&
                    (sa.IdStudent == objec.IdStudent)).ToList ().Any ()) {
                db.Studentattendance.Add (new Studentattendance (IdClass, objec.IdStudent, DateTime.Now, 0, IdCourse));
                db.SaveChanges ();
                return Ok (true);
            }
            return NotFound (new { results = "ERROR YOU CAN'T SCAN AGAIN !! YOUR ATTENDANCE TO THIS SAME SESSION WAS ALREADY TAKEN" });
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("getProfCourses")]
        public object getProfCourses (Professor p) //just for the id
        {
            try { 
            List<Instruction> listI = db.Instruction.Where (i => i.IdProfessor.Equals (p.IdProfessor)).ToList ();
            List<Course> listS = new List<Course> ();
            foreach (Instruction i in listI) {
                listS.AddRange (db.Course.Where (c => c.IdCourse.Equals (i.IdCourse)).ToList ());
            }
            List<StringClass> l = new List<StringClass> ();
            foreach (Course c in listS) {
                l.Add (new StringClass (c.IdCourse));
            }
            if (l.Count == 0)
            {

                return NotFound(new { results = "Sorry You Are Not Assigned to Any Course ! Try Contacting The Admin " });
            }
            return l;
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("getStudentsInSession")]
        public object getStudentsInSession (StringClass code) {
            try { 
            string[] QRSplitted = code.str.Split (';'); //class;course;dateTime
            string dateTime = QRSplitted[2];
            string IdCourse = QRSplitted[1];
            string IdClass = QRSplitted[0];
            DateTime datetimeStruct = DateTime.Parse (dateTime);

            Studentattendance sa = db.Studentattendance.AsEnumerable ().Last ();

            List<responseStudentAttendance> lsRSA = new List<responseStudentAttendance> ();
            List<Studentattendance> lsSA = db.Studentattendance.AsEnumerable ().Where (sa => (sa.Date.DayOfYear == DateTime.Parse (dateTime).DayOfYear) &&
                (sa.Date.Year == DateTime.Parse (dateTime).Year) &&
                (sa.IdClass == IdClass) &&
                (sa.IdCourse == IdCourse)).ToList ();
            foreach (Studentattendance sa1 in lsSA) {
                Student s = db.Student.AsEnumerable ().Where (s => s.IdStudent == sa1.IdStudent).First ();
                TimeSpan ts = TimeSpan.Parse (
                    db.Schedule.AsEnumerable ().Where (S =>
                        S.Day == datetimeStruct.DayOfWeek.ToString () &&
                        datetimeStruct.TimeOfDay.Subtract (TimeSpan.Parse (S.Time)).CompareTo (TimeSpan.Parse ("01:15:00")) < 0 &&
                        datetimeStruct.TimeOfDay.Subtract (TimeSpan.Parse (S.Time)).CompareTo (TimeSpan.Parse ("00:00:00")) >= 0 &&
                        S.IdClass == IdClass &&
                        S.IdCourse == IdCourse
                    ).First ().Time);
                if (sa1.Date.TimeOfDay
                    .Subtract (ts)
                    .CompareTo (TimeSpan.Parse ("01:15:00")) < 0 &&
                    sa1.Date.TimeOfDay.Subtract (ts).CompareTo (TimeSpan.Parse ("00:00:00")) >= 0)
                    lsRSA.Add (new responseStudentAttendance (sa1, s));
            }
            if (lsRSA.Count == 0)
            {
                return NotFound(new { results = "No Students Attended This Class ! If You Think This is Wrong Consider Contacting The Admin" });
            }
            return lsRSA;
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("getStudentSessions")]
        public object getStudentSessions (Student s) //only for the id
        {
            try { 
            s = db.Student.Find (s.IdStudent);
            List<Studentattendance> studentattendances = db.Studentattendance.Where (sa => sa.IdStudent == s.IdStudent).ToList ();
            List<responseStudentAttendance> responseStudentAttendances = new List<responseStudentAttendance> ();
            foreach (Studentattendance sa in studentattendances) {
                string time = db.Schedule.AsEnumerable ().Where (S =>
                    S.Day == sa.Date.DayOfWeek.ToString () &&
                    sa.Date.TimeOfDay.Subtract (TimeSpan.Parse (S.Time)).CompareTo (TimeSpan.Parse ("01:15:00")) < 0 &&
                    sa.Date.TimeOfDay.Subtract (TimeSpan.Parse (S.Time)).CompareTo (TimeSpan.Parse ("00:00:00")) >= 0 &&
                    S.IdClass == sa.IdClass &&
                    S.IdCourse == sa.IdCourse
                ).First ().Time;
                responseStudentAttendances.Add (new responseStudentAttendance (sa, s, DateTime.Parse (sa.Date.ToShortDateString () + " " + time)));
            }
            if (responseStudentAttendances.Count == 0)
            {
                return NotFound(new { results = "No Students Attended This Class ! If You Think This is Wrong Consider Contacting The Admin" });
            }
            return responseStudentAttendances;
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddPerson")]
        public Object AddPerson (Person p) {
            try { 
            if (!db.Person.AsEnumerable ().Where (sa => (sa.IdPerson == p.IdPerson)).ToList ().Any ()) {
                db.Person.Add (p);
                db.SaveChanges ();
                return Ok (true);
            } else
                return NotFound (new { results = "ERROR YOU CAN'T ADD THE SAME PERSON TWICE !!" });
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddStudent")]
        public Object AddStudent (Student s) {
            try { 
            Person p = db.Person.Find (s.IdStudent);
            if (p.IdPerson == s.IdStudent) {
                db.Student.Add (s);
                db.SaveChanges ();
                return Ok (true);

            } else
                return NotFound (new { results = "ERROR YOU CAN'T ADD THIS STUDENT" });
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddProfessor")]
        public Object AddProfessor (Professor s) {
            try { 
            Person p = db.Person.Find (s.IdProfessor);
            if (p.IdPerson == s.IdProfessor) {
                db.Professor.Add (s);
                db.SaveChanges ();
                return Ok (true);

            } else
                return NotFound (new { results = "ERROR YOU CAN'T ADD THIS STUDENT" });
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("EnrollStudentInCourse")]
        public Object EnrollStudentInCourse (EnrollCourseStudent s) {
            try { 
            Enrollment e = new Enrollment ();
            Student p = db.Student.Find (s.IdStudent);
            if (p.IdStudent == s.IdStudent) {
                e.IdStudent = s.IdStudent;
                e.IdCourse = s.IdCourse;
                db.Enrollment.Add (e);
                db.SaveChanges ();
                return Ok (true);

            } else
                return NotFound (new { results = "ERROR YOU CAN'T ENROLL THIS STUDENT IN THIS COURSE" });
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("DoctorInstruction")]
        public Object DoctorInstruction (ProfessorCourseInstruction pf) {
            try { 
            Instruction e = new Instruction ();
            Professor p = db.Professor.Find (pf.IdProfessor);
            if (p.IdProfessor == pf.IdProfessor) {
                e.IdProfessor = pf.IdProfessor;
                e.IdCourse = pf.IdCourse;
                db.Instruction.Add (e);
                db.SaveChanges ();
                return Ok (true);

            } else
                return NotFound (new { results = "ERROR YOU CAN'T ASSIGN THIS COURSE TO THAT DOCTOR" });
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddSchedule")]
        public Object AddSchedule (Schedule s) {
            try { 
            db.Schedule.Add (s);
            db.SaveChanges ();
            return Ok (true);
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddCourse")]
        public Object AddCourse (Course c) {
            try { 
            db.Course.Add (c);
            db.SaveChanges ();
            return Ok (true);
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddLevel")]
        public Object AddLevel (Level l) {
            try { 
            db.Level.Add (l);
            db.SaveChanges ();
            return Ok (true);
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpGet]
        [Route ("getLevels")]

        public object getLevels () {
            try { 
            return db.Level.ToList ();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpPost]
        [Route ("AddClass")]
        public Object AddClass (Class l) {
            try { 
            db.Class.Add (l);
            db.SaveChanges ();
            return Ok (true);
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpGet]
        [Route ("getCourses")]

        public object getCourses () {
            try { 
            return db.Course.ToList ();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpGet]
        [Route ("getClass")]

        public object getClass () {
            try
            {
                return db.Class.ToList();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpGet]
        [Route ("getStudents")]

        public object getStudents () {
            try
            {
                if (!db.Student.ToList().Any())
                {

                    return NotFound(new { results = "Sorry But No Professors Inserted !" });
                }
                return db.Student.ToList();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }

        [HttpGet]
        [Route ("getProfessors")]

        public object getProfessors () {
            try
            {
                if (!db.Professor.ToList().Any())
                {

                    return NotFound(new { results = "Sorry But No Professors Inserted !" });
                }
                return db.Professor.ToList();
            }
            catch (Exception e)
            {
                return NotFound(new { results = "Server Crashed Something Went Wrong !" });
            }
        }
    }
}