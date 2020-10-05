using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WEBAPI.Models
{
    public partial class qrContext : DbContext
    {
        public qrContext()
        {
        }

        public qrContext(DbContextOptions<qrContext> options)
            : base(options)
        {
        }
        public void DeleteZipFile()
        {
            File.Delete("result.zip");
        }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Instruction> Instruction { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Professorattendance> Professorattendance { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studentattendance> Studentattendance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=toor;database=qr");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.IdClass)
                    .HasName("PRIMARY");

                entity.ToTable("class");

                entity.HasIndex(e => e.IdClass)
                    .HasName("idClass_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdClass)
                    .HasColumnName("idClass")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FloorNumber).HasColumnName("Floor_Number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.IdCourse)
                    .HasName("PRIMARY");

                entity.ToTable("course");

                entity.HasIndex(e => e.IdCourse)
                    .HasName("idCOURSE_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdCourse)
                    .HasColumnName("idCourse")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Idenrollment)
                    .HasName("PRIMARY");

                entity.ToTable("enrollment");

                entity.HasIndex(e => e.IdCourse)
                    .HasName("idCourse_idx");

                entity.HasIndex(e => e.IdStudent)
                    .HasName("idStudent_idx");

                entity.HasIndex(e => e.Idenrollment)
                    .HasName("idenrollment_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idenrollment).HasColumnName("idenrollment");

                entity.Property(e => e.IdCourse)
                    .IsRequired()
                    .HasColumnName("idCourse")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IdStudent).HasColumnName("idStudent");

                entity.HasOne(d => d.IdCourseNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdCourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idCourse");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idStudent");
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.HasKey(e => e.IdInstruction)
                    .HasName("PRIMARY");

                entity.ToTable("instruction");

                entity.HasIndex(e => e.IdCourse)
                    .HasName("idCourse_idx2");

                entity.HasIndex(e => e.IdProfessor)
                    .HasName("idProfessor_idx2");

                entity.Property(e => e.IdInstruction).HasColumnName("idInstruction");

                entity.Property(e => e.IdCourse)
                    .IsRequired()
                    .HasColumnName("idCourse")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IdProfessor).HasColumnName("idProfessor");

                entity.HasOne(d => d.IdCourseNavigation)
                    .WithMany(p => p.Instruction)
                    .HasForeignKey(d => d.IdCourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idCourse2");

                entity.HasOne(d => d.IdProfessorNavigation)
                    .WithMany(p => p.Instruction)
                    .HasForeignKey(d => d.IdProfessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idProfessor2");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.IdLevel)
                    .HasName("PRIMARY");

                entity.ToTable("level");

                entity.HasIndex(e => e.IdLevel)
                    .HasName("idLevel_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdLevel).HasColumnName("idLevel");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasColumnName("department")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("PRIMARY");

                entity.ToTable("person");

                entity.HasIndex(e => e.IdPerson)
                    .HasName("idPerson_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UserName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdPerson).HasColumnName("idPerson");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.IdProfessor)
                    .HasName("PRIMARY");

                entity.ToTable("professor");

                entity.HasIndex(e => e.Email)
                    .HasName("Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdProfessor)
                    .HasName("idProfessor_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile)
                    .HasName("Mobile_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdProfessor).HasColumnName("idProfessor");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProfessorNavigation)
                    .WithOne(p => p.Professor)
                    .HasForeignKey<Professor>(d => d.IdProfessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idProfessor12");
            });

            modelBuilder.Entity<Professorattendance>(entity =>
            {
                entity.HasKey(e => e.IdProfessorAttendance)
                    .HasName("PRIMARY");

                entity.ToTable("professorattendance");

                entity.HasIndex(e => e.IdClass)
                    .HasName("idClass_idx");

                entity.HasIndex(e => e.IdProfessor)
                    .HasName("idProfessor_idx");

                entity.Property(e => e.IdProfessorAttendance).HasColumnName("idProfessorAttendance");

                entity.Property(e => e.IdClass)
                    .IsRequired()
                    .HasColumnName("idClass")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IdProfessor).HasColumnName("idProfessor");

                entity.HasOne(d => d.IdClassNavigation)
                    .WithMany(p => p.Professorattendance)
                    .HasForeignKey(d => d.IdClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idClass");

                entity.HasOne(d => d.IdProfessorNavigation)
                    .WithMany(p => p.Professorattendance)
                    .HasForeignKey(d => d.IdProfessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idProfessor");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.IdSchedule)
                    .HasName("PRIMARY");

                entity.ToTable("schedule");

                entity.HasIndex(e => e.IdClass)
                    .HasName("idClass_idx4");

                entity.HasIndex(e => e.IdLevel)
                    .HasName("idLevel33_idx");

                entity.Property(e => e.IdSchedule).HasColumnName("idSchedule");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdClass)
                    .IsRequired()
                    .HasColumnName("idClass")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IdCourse)
                    .IsRequired()
                    .HasColumnName("idCourse")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IdLevel).HasColumnName("idLevel");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClassNavigation)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.IdClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idClass11");

                entity.HasOne(d => d.IdLevelNavigation)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.IdLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idLevel33");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent)
                    .HasName("PRIMARY");

                entity.ToTable("student");

                entity.HasIndex(e => e.Email)
                    .HasName("Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdLevel)
                    .HasName("idLevel22_idx");

                entity.HasIndex(e => e.IdStudent)
                    .HasName("idStudents_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile)
                    .HasName("Mobile_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdStudent).HasColumnName("idStudent");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdLevel).HasColumnName("idLevel");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLevelNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idLevel22");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idStudent12");
            });

            modelBuilder.Entity<Studentattendance>(entity =>
            {
                entity.HasKey(e => e.IdStudentAttendance)
                    .HasName("PRIMARY");

                entity.ToTable("studentattendance");

                entity.HasIndex(e => e.IdClass)
                    .HasName("idClass_idx");

                entity.HasIndex(e => e.IdCourse)
                    .HasName("idCourse_idx");

                entity.HasIndex(e => e.IdStudent)
                    .HasName("idStudent_idx");

                entity.Property(e => e.IdStudentAttendance).HasColumnName("idStudentAttendance");

                entity.Property(e => e.IdClass)
                    .IsRequired()
                    .HasColumnName("idClass")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IdCourse)
                    .IsRequired()
                    .HasColumnName("idCourse")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IdStudent).HasColumnName("idStudent");

                entity.HasOne(d => d.IdClassNavigation)
                    .WithMany(p => p.Studentattendance)
                    .HasForeignKey(d => d.IdClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idClass1");

                entity.HasOne(d => d.IdCourseNavigation)
                    .WithMany(p => p.Studentattendance)
                    .HasForeignKey(d => d.IdCourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idCourseee");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Studentattendance)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idStudent1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
