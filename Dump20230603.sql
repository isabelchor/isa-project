CREATE DATABASE  IF NOT EXISTS `exhibition` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `exhibition`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: localhost    Database: exhibition
-- ------------------------------------------------------
-- Server version	8.0.33

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
-- Table structure for table `artist`
--

DROP TABLE IF EXISTS `artist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `artist` (
  `name` varchar(45) NOT NULL,
  `birthday` varchar(45) NOT NULL,
  `artistID` int NOT NULL AUTO_INCREMENT,
  `password` varchar(50) NOT NULL,
  PRIMARY KEY (`artistID`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `artist`
--

LOCK TABLES `artist` WRITE;
/*!40000 ALTER TABLE `artist` DISABLE KEYS */;
INSERT INTO `artist` VALUES ('Gadi',' 28/9/1971',1,'gadi123'),('David','24/11/2005',2,'dudupop'),('dennis','29/06/2005',4,'12e'),('admin','00/00/0000',5,'admin'),('fakedennis','29/06/2006',6,'12e');
/*!40000 ALTER TABLE `artist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drawing`
--

DROP TABLE IF EXISTS `drawing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drawing` (
  `cost` int NOT NULL,
  `published` varchar(45) NOT NULL,
  `technique` varchar(45) NOT NULL,
  `namedrawing` varchar(45) NOT NULL,
  `drawingID` int NOT NULL AUTO_INCREMENT,
  `artistID` int NOT NULL,
  `typeID` int NOT NULL,
  PRIMARY KEY (`drawingID`),
  KEY `sss_idx` (`artistID`),
  KEY `D_TID_FK_idx` (`typeID`),
  CONSTRAINT `D_TID_FK` FOREIGN KEY (`typeID`) REFERENCES `type` (`ID`),
  CONSTRAINT `sss` FOREIGN KEY (`artistID`) REFERENCES `artist` (`artistID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drawing`
--

LOCK TABLES `drawing` WRITE;
/*!40000 ALTER TABLE `drawing` DISABLE KEYS */;
INSERT INTO `drawing` VALUES (50,'2022-05-10','Pencil','Portrait of a Woman',1,1,5),(100,'2023-01-15','Charcoal','Landscape at Sunset',2,2,8),(75,'2022-11-20','Ink','Still Life with Fruits',3,1,3),(200,'2023-04-05','Watercolor','Seaside Serenity',4,4,2),(80,'2022-09-08','Pastel','Cityscape in Spring',5,2,7),(120,'2023-06-01','Oil','Abstract Expression',6,1,6),(150,'2023-03-20','Acrylic','Still Life with Vase',8,1,1),(70,'2022-08-15','Charcoal','Portrait of a Man',9,2,9),(180,'2023-02-05','Pencil','Wildlife Encounter',10,4,10);
/*!40000 ALTER TABLE `drawing` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `replicas`
--

DROP TABLE IF EXISTS `replicas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `replicas` (
  `location` varchar(45) NOT NULL,
  `replicaID` int NOT NULL AUTO_INCREMENT,
  `drawingID` int NOT NULL,
  PRIMARY KEY (`replicaID`),
  KEY `R_ID_FK_idx` (`drawingID`),
  CONSTRAINT `R_ID_FK` FOREIGN KEY (`drawingID`) REFERENCES `drawing` (`drawingID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `replicas`
--

LOCK TABLES `replicas` WRITE;
/*!40000 ALTER TABLE `replicas` DISABLE KEYS */;
INSERT INTO `replicas` VALUES ('migdal haemek',1,1);
/*!40000 ALTER TABLE `replicas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type`
--

DROP TABLE IF EXISTS `type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `type` (
  `ID` int NOT NULL,
  `types` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type`
--

LOCK TABLES `type` WRITE;
/*!40000 ALTER TABLE `type` DISABLE KEYS */;
INSERT INTO `type` VALUES (1,'Realism'),(2,'Cubism'),(3,'Impressionism'),(4,'Surrealism'),(5,'Abstract'),(6,'Pointillism'),(7,'Pop Art'),(8,'Expressionism'),(9,'Fauvism'),(10,'Dadaism'),(11,'Photorealism');
/*!40000 ALTER TABLE `type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-03 22:32:23
