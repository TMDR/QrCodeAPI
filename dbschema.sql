-- MySQL dump 10.13  Distrib 8.0.21, for Win64 (x86_64)
--
-- Host: localhost    Database: qr
-- ------------------------------------------------------
-- Server version	8.0.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `class`
--

DROP TABLE IF EXISTS `class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `class` (
  `idClass` varchar(8) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Floor_Number` int NOT NULL,
  PRIMARY KEY (`idClass`),
  UNIQUE KEY `idClass_UNIQUE` (`idClass`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `course`
--

DROP TABLE IF EXISTS `course`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `course` (
  `idCourse` varchar(5) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Credits` int NOT NULL,
  `TotalDuration` int NOT NULL,
  PRIMARY KEY (`idCourse`),
  UNIQUE KEY `idCOURSE_UNIQUE` (`idCourse`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `enrollment`
--

DROP TABLE IF EXISTS `enrollment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enrollment` (
  `idenrollment` int NOT NULL AUTO_INCREMENT,
  `idCourse` varchar(5) NOT NULL,
  `idStudent` int NOT NULL,
  PRIMARY KEY (`idenrollment`),
  UNIQUE KEY `idenrollment_UNIQUE` (`idenrollment`),
  KEY `idCourse_idx` (`idCourse`),
  KEY `idStudent_idx` (`idStudent`),
  CONSTRAINT `idCourse` FOREIGN KEY (`idCourse`) REFERENCES `course` (`idCourse`),
  CONSTRAINT `idStudent` FOREIGN KEY (`idStudent`) REFERENCES `student` (`idStudent`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `instruction`
--

DROP TABLE IF EXISTS `instruction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `instruction` (
  `idInstruction` int NOT NULL AUTO_INCREMENT,
  `idProfessor` int NOT NULL,
  `idCourse` varchar(5) NOT NULL,
  PRIMARY KEY (`idInstruction`),
  KEY `idProfessor_idx` (`idProfessor`),
  KEY `idCourse_idx` (`idCourse`),
  KEY `idProfessor_idx2` (`idProfessor`),
  KEY `idCourse_idx2` (`idCourse`),
  CONSTRAINT `idCourse2` FOREIGN KEY (`idCourse`) REFERENCES `course` (`idCourse`),
  CONSTRAINT `idProfessor2` FOREIGN KEY (`idProfessor`) REFERENCES `professor` (`idProfessor`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `level`
--

DROP TABLE IF EXISTS `level`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `level` (
  `idLevel` int NOT NULL AUTO_INCREMENT,
  `year` int NOT NULL,
  `department` varchar(45) NOT NULL,
  PRIMARY KEY (`idLevel`),
  UNIQUE KEY `idLevel_UNIQUE` (`idLevel`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `person`
--

DROP TABLE IF EXISTS `person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `person` (
  `idPerson` int NOT NULL,
  `UserName` varchar(20) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Type` int NOT NULL,
  PRIMARY KEY (`idPerson`),
  UNIQUE KEY `idPerson_UNIQUE` (`idPerson`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `professor`
--

DROP TABLE IF EXISTS `professor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `professor` (
  `idProfessor` int NOT NULL,
  `First_Name` varchar(30) NOT NULL,
  `Last_Name` varchar(30) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Mobile` varchar(15) DEFAULT NULL,
  `Address` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`idProfessor`),
  UNIQUE KEY `idProfessor_UNIQUE` (`idProfessor`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Mobile_UNIQUE` (`Mobile`),
  CONSTRAINT `idProfessor12` FOREIGN KEY (`idProfessor`) REFERENCES `person` (`idPerson`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `professorattendance`
--

DROP TABLE IF EXISTS `professorattendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `professorattendance` (
  `idProfessorAttendance` int NOT NULL AUTO_INCREMENT,
  `idClass` varchar(8) NOT NULL,
  `idProfessor` int NOT NULL,
  `Date` datetime NOT NULL,
  PRIMARY KEY (`idProfessorAttendance`),
  KEY `idProfessor_idx` (`idProfessor`),
  KEY `idClass_idx` (`idClass`),
  CONSTRAINT `idClass` FOREIGN KEY (`idClass`) REFERENCES `class` (`idClass`),
  CONSTRAINT `idProfessor` FOREIGN KEY (`idProfessor`) REFERENCES `professor` (`idProfessor`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `idSchedule` int NOT NULL AUTO_INCREMENT,
  `idClass` varchar(8) NOT NULL,
  `Day` varchar(20) NOT NULL,
  `Time` varchar(20) NOT NULL,
  `idCourse` varchar(45) NOT NULL,
  `idLevel` int NOT NULL,
  PRIMARY KEY (`idSchedule`),
  KEY `idClass_idx` (`idClass`),
  KEY `idClass_idx1` (`idClass`),
  KEY `idClass_idx2` (`idClass`),
  KEY `idClass_idx3` (`idClass`),
  KEY `idClass_idx4` (`idClass`),
  KEY `idClass_idx11` (`idClass`),
  KEY `idLevel33_idx` (`idLevel`),
  CONSTRAINT `idClass11` FOREIGN KEY (`idClass`) REFERENCES `class` (`idClass`),
  CONSTRAINT `idLevel33` FOREIGN KEY (`idLevel`) REFERENCES `level` (`idLevel`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `idStudent` int NOT NULL,
  `First_Name` varchar(20) NOT NULL,
  `Last_Name` varchar(20) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Mobile` varchar(15) DEFAULT NULL,
  `idLevel` int NOT NULL,
  PRIMARY KEY (`idStudent`),
  UNIQUE KEY `idStudents_UNIQUE` (`idStudent`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Mobile_UNIQUE` (`Mobile`),
  KEY `idLevel22_idx` (`idLevel`),
  CONSTRAINT `idLevel22` FOREIGN KEY (`idLevel`) REFERENCES `level` (`idLevel`),
  CONSTRAINT `idStudent12` FOREIGN KEY (`idStudent`) REFERENCES `person` (`idPerson`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `studentattendance`
--

DROP TABLE IF EXISTS `studentattendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentattendance` (
  `idStudentAttendance` int NOT NULL AUTO_INCREMENT,
  `idClass` varchar(8) NOT NULL,
  `idStudent` int NOT NULL,
  `Date` datetime NOT NULL,
  `Grade` int NOT NULL,
  `idCourse` varchar(45) NOT NULL,
  PRIMARY KEY (`idStudentAttendance`),
  KEY `idClass_idx` (`idClass`),
  KEY `idStudent_idx` (`idStudent`),
  KEY `idCourse_idx` (`idCourse`),
  KEY `idCourse1_idx` (`idCourse`),
  KEY `idCourse232321_idx` (`idCourse`),
  KEY `idc_idx` (`idCourse`),
  CONSTRAINT `idClass1` FOREIGN KEY (`idClass`) REFERENCES `class` (`idClass`),
  CONSTRAINT `idCourseee` FOREIGN KEY (`idCourse`) REFERENCES `course` (`idCourse`),
  CONSTRAINT `idStudent1` FOREIGN KEY (`idStudent`) REFERENCES `student` (`idStudent`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-10-05 19:54:50
