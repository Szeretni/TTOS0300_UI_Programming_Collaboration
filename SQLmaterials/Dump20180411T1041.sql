CREATE DATABASE  IF NOT EXISTS `SCHEMA10` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `SCHEMA10`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 91.154.199.251    Database: SCHEMA10
-- ------------------------------------------------------
-- Server version	5.5.59-0+deb8u1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Building`
--

DROP TABLE IF EXISTS `Building`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Building` (
  `BuildingId` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(45) NOT NULL,
  `Price` int(11) NOT NULL,
  PRIMARY KEY (`BuildingId`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Building`
--

LOCK TABLES `Building` WRITE;
/*!40000 ALTER TABLE `Building` DISABLE KEYS */;
INSERT INTO `Building` VALUES (1,'Hostel',100),(2,'Hostel',100),(3,'Hostel',100),(4,'Hostel',100),(5,'Hostel',100),(6,'Hostel',100),(7,'Hostel',100),(8,'Hostel',100),(9,'Hostel',100),(10,'Hostel',100),(11,'Hostel',100),(12,'Hostel',100),(13,'Hostel',100),(14,'Hostel',100),(15,'Hostel',100),(16,'Hostel',100),(17,'Hostel',100),(18,'Hostel',100),(19,'Hostel',100),(20,'Hostel',100),(21,'Hostel',100),(22,'Hostel',100),(23,'Hostel',100),(24,'Hostel',100),(25,'Hostel',100),(26,'Hostel',100),(27,'Hostel',100),(28,'Hostel',100),(29,'Hostel',100),(30,'Hostel',100),(31,'Hostel',100),(32,'Hostel',100),(33,'Hostel',100),(34,'Hostel',100),(35,'Hostel',100),(36,'Hostel',100),(37,'Hostel',100),(38,'Hostel',100),(39,'Hostel',100),(40,'Hostel',100),(41,'Hotel',200),(42,'Hotel',200),(43,'Hotel',200),(44,'Hotel',200),(45,'Hotel',200),(46,'Hotel',200),(47,'Hotel',200),(48,'Hotel',200),(49,'Hotel',200),(50,'Hotel',200);
/*!40000 ALTER TABLE `Building` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Cash`
--

DROP TABLE IF EXISTS `Cash`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Cash` (
  `CashId` int(11) NOT NULL AUTO_INCREMENT,
  `Value` int(11) NOT NULL,
  PRIMARY KEY (`CashId`)
) ENGINE=InnoDB AUTO_INCREMENT=201 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Cash`
--

LOCK TABLES `Cash` WRITE;
/*!40000 ALTER TABLE `Cash` DISABLE KEYS */;
INSERT INTO `Cash` VALUES (1,500),(2,500),(3,500),(4,500),(5,500),(6,500),(7,500),(8,500),(9,500),(10,500),(11,500),(12,500),(13,500),(14,500),(15,500),(16,500),(17,500),(18,500),(19,500),(20,500),(21,500),(22,500),(23,500),(24,500),(25,500),(26,500),(27,500),(28,500),(29,500),(30,500),(31,100),(32,100),(33,100),(34,100),(35,100),(36,100),(37,100),(38,100),(39,100),(40,100),(41,100),(42,100),(43,100),(44,100),(45,100),(46,100),(47,100),(48,100),(49,100),(50,100),(51,100),(52,100),(53,100),(54,100),(55,100),(56,100),(57,100),(58,100),(59,100),(60,100),(61,50),(62,50),(63,50),(64,50),(65,50),(66,50),(67,50),(68,50),(69,50),(70,50),(71,50),(72,50),(73,50),(74,50),(75,50),(76,50),(77,50),(78,50),(79,50),(80,50),(81,50),(82,50),(83,50),(84,50),(85,50),(86,50),(87,50),(88,50),(89,50),(90,50),(91,20),(92,20),(93,20),(94,20),(95,20),(96,20),(97,20),(98,20),(99,20),(100,20),(101,10),(102,10),(103,10),(104,10),(105,10),(106,10),(107,10),(108,10),(109,10),(110,10),(111,10),(112,10),(113,10),(114,10),(115,10),(116,10),(117,10),(118,10),(119,10),(120,10),(121,5),(122,5),(123,5),(124,5),(125,5),(126,5),(127,5),(128,5),(129,5),(130,5),(131,5),(132,5),(133,5),(134,5),(135,5),(136,5),(137,5),(138,5),(139,5),(140,5),(141,1),(142,1),(143,1),(144,1),(145,1),(146,1),(147,1),(148,1),(149,1),(150,1),(151,1),(152,1),(153,1),(154,1),(155,1),(156,1),(157,1),(158,1),(159,1),(160,1),(161,1),(162,1),(163,1),(164,1),(165,1),(166,1),(167,1),(168,1),(169,1),(170,1),(171,1),(172,1),(173,1),(174,1),(175,1),(176,1),(177,1),(178,1),(179,1),(180,1),(181,1),(182,1),(183,1),(184,1),(185,1),(186,1),(187,1),(188,1),(189,1),(190,1),(191,1),(192,1),(193,1),(194,1),(195,1),(196,1),(197,1),(198,1),(199,1),(200,1);
/*!40000 ALTER TABLE `Cash` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `CashPerPlayerPerGame`
--

DROP TABLE IF EXISTS `CashPerPlayerPerGame`;
/*!50001 DROP VIEW IF EXISTS `CashPerPlayerPerGame`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `CashPerPlayerPerGame` AS SELECT 
 1 AS `GameSessionId`,
 1 AS `PlayerName`,
 1 AS `TotalCash`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `Cell`
--

DROP TABLE IF EXISTS `Cell`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Cell` (
  `CellId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Rent` int(11) DEFAULT NULL,
  `Price` int(11) DEFAULT NULL,
  `SerieId` int(11) DEFAULT NULL,
  `CellTypeId` int(11) NOT NULL,
  PRIMARY KEY (`CellId`),
  KEY `fk_Cell_CellType1_idx` (`CellTypeId`),
  CONSTRAINT `fk_Cell_CellType1` FOREIGN KEY (`CellTypeId`) REFERENCES `CellType` (`CellTypeId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Cell`
--

LOCK TABLES `Cell` WRITE;
/*!40000 ALTER TABLE `Cell` DISABLE KEYS */;
INSERT INTO `Cell` VALUES (1,'Start',0,0,0,1),(2,'Korkeavuorenkatu',10,60,1,2),(3,'Community Chest',0,0,0,3),(4,'Kasarmikatu',10,60,1,2),(5,'Pasilan asema',20,200,2,4),(6,'Rantatie',15,100,3,2),(7,'Chance',0,0,4,5),(8,'Kauppatori',15,100,3,2),(9,'Esplanadi',20,120,3,2),(10,'Prison',0,0,0,6),(11,'Hameentie',25,140,4,2),(12,'Siltasaari',25,140,4,2),(13,'Kaisaniemenkatu',30,160,4,2),(14,'Sornaisten asema',20,200,2,4),(15,'Liisankatu',35,180,5,2),(16,'Community Chest',0,0,0,3),(17,'Snellmaninkatu',35,180,5,2),(18,'Unioninkatu',40,200,5,2),(19,'Free parking',0,0,0,6),(20,'Lonnrotinkatu',45,220,6,2),(21,'Chance',0,0,4,5),(22,'Annankatu',45,220,6,2),(23,'Simonkatu',50,240,6,2),(24,'Rautatieasema',20,200,2,4),(25,'Mikonkatu',55,260,7,2),(26,'Aleksanterinkatu',55,260,7,2),(27,'Keskuskatu',60,280,7,2),(28,'Go to Jail',0,0,0,6),(29,'Tehtaankatu',65,300,8,2),(30,'Eira',65,300,8,2),(31,'Community Chest',0,0,0,3),(32,'Bulevardi',70,320,8,2),(33,'Tavara-asema',20,200,2,4),(34,'Chance',0,0,4,5),(35,'Mannerheimintie',75,350,9,2),(36,'Erottaja',85,400,9,2);
/*!40000 ALTER TABLE `Cell` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CellType`
--

DROP TABLE IF EXISTS `CellType`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CellType` (
  `CellTypeId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`CellTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CellType`
--

LOCK TABLES `CellType` WRITE;
/*!40000 ALTER TABLE `CellType` DISABLE KEYS */;
INSERT INTO `CellType` VALUES (1,'Start'),(2,'Properties'),(3,'Community'),(4,'Transportation'),(5,'Chance'),(6,'Prison'),(7,'Parking'),(8,'Sentence');
/*!40000 ALTER TABLE `CellType` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `GameSession`
--

DROP TABLE IF EXISTS `GameSession`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `GameSession` (
  `GameSessionId` int(11) NOT NULL AUTO_INCREMENT,
  `WinnerId` int(11) DEFAULT NULL,
  `WinnerCash` int(11) DEFAULT NULL,
  `WinnerCell` int(11) DEFAULT NULL,
  PRIMARY KEY (`GameSessionId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GameSession`
--

LOCK TABLES `GameSession` WRITE;
/*!40000 ALTER TABLE `GameSession` DISABLE KEYS */;
INSERT INTO `GameSession` VALUES (1,NULL,NULL,NULL),(2,1,500,15),(3,2,300,20);
/*!40000 ALTER TABLE `GameSession` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `GameSession_has_player`
--

DROP TABLE IF EXISTS `GameSession_has_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `GameSession_has_player` (
  `PlayerId` int(11) NOT NULL,
  `GameSessionId` int(11) NOT NULL,
  `CellId` int(11) NOT NULL DEFAULT '0' COMMENT 'Player position.',
  PRIMARY KEY (`PlayerId`,`GameSessionId`),
  KEY `fk_Player_has_Attendance_Attendance1_idx` (`GameSessionId`),
  KEY `fk_Player_has_Attendance_Player1_idx` (`PlayerId`),
  KEY `fk_GameSession_has_player_Cell1_idx` (`CellId`),
  CONSTRAINT `fk_Player_has_Attendance_Player1` FOREIGN KEY (`PlayerId`) REFERENCES `Player` (`PlayerId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Attendance_Attendance1` FOREIGN KEY (`GameSessionId`) REFERENCES `GameSession` (`GameSessionId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_GameSession_has_player_Cell1` FOREIGN KEY (`CellId`) REFERENCES `Cell` (`CellId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GameSession_has_player`
--

LOCK TABLES `GameSession_has_player` WRITE;
/*!40000 ALTER TABLE `GameSession_has_player` DISABLE KEYS */;
INSERT INTO `GameSession_has_player` VALUES (1,1,1),(2,1,1),(3,1,1),(1,2,5),(3,2,8);
/*!40000 ALTER TABLE `GameSession_has_player` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Player`
--

DROP TABLE IF EXISTS `Player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Player` (
  `PlayerId` int(11) NOT NULL AUTO_INCREMENT,
  `PlayerName` varchar(45) NOT NULL,
  PRIMARY KEY (`PlayerId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Player`
--

LOCK TABLES `Player` WRITE;
/*!40000 ALTER TABLE `Player` DISABLE KEYS */;
INSERT INTO `Player` VALUES (1,'Hannu'),(2,'Antti'),(3,'Ville');
/*!40000 ALTER TABLE `Player` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Player_has_Building`
--

DROP TABLE IF EXISTS `Player_has_Building`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Player_has_Building` (
  `PlayerId` int(11) NOT NULL,
  `BuildingId` int(11) NOT NULL,
  `GameSessionId` int(11) NOT NULL,
  PRIMARY KEY (`PlayerId`,`BuildingId`,`GameSessionId`),
  UNIQUE KEY `unique_building_game` (`BuildingId`,`GameSessionId`),
  KEY `fk_Player_has_Building_Building1_idx` (`BuildingId`),
  KEY `fk_Player_has_Building_Player1_idx` (`PlayerId`),
  KEY `fk_Player_has_Building_Attendance1_idx` (`GameSessionId`),
  CONSTRAINT `fk_Player_has_Building_Player1` FOREIGN KEY (`PlayerId`) REFERENCES `Player` (`PlayerId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Building_Building1` FOREIGN KEY (`BuildingId`) REFERENCES `Building` (`BuildingId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Building_Attendance1` FOREIGN KEY (`GameSessionId`) REFERENCES `GameSession` (`GameSessionId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Player_has_Building`
--

LOCK TABLES `Player_has_Building` WRITE;
/*!40000 ALTER TABLE `Player_has_Building` DISABLE KEYS */;
INSERT INTO `Player_has_Building` VALUES (1,1,1),(1,2,1),(2,3,1);
/*!40000 ALTER TABLE `Player_has_Building` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Player_has_Cash`
--

DROP TABLE IF EXISTS `Player_has_Cash`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Player_has_Cash` (
  `PlayerId` int(11) NOT NULL,
  `CashId` int(11) NOT NULL,
  `GameSessionId` int(11) NOT NULL,
  PRIMARY KEY (`PlayerId`,`CashId`,`GameSessionId`),
  UNIQUE KEY `unique_game_cash` (`CashId`,`GameSessionId`),
  KEY `fk_Player_has_Cash_Cash1_idx` (`CashId`),
  KEY `fk_Player_has_Cash_Player1_idx` (`PlayerId`),
  KEY `fk_Player_has_Cash_Attendance1_idx` (`GameSessionId`),
  CONSTRAINT `fk_Player_has_Cash_Player1` FOREIGN KEY (`PlayerId`) REFERENCES `Player` (`PlayerId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cash_Cash1` FOREIGN KEY (`CashId`) REFERENCES `Cash` (`CashId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cash_Attendance1` FOREIGN KEY (`GameSessionId`) REFERENCES `GameSession` (`GameSessionId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Player_has_Cash`
--

LOCK TABLES `Player_has_Cash` WRITE;
/*!40000 ALTER TABLE `Player_has_Cash` DISABLE KEYS */;
INSERT INTO `Player_has_Cash` VALUES (1,1,1),(1,1,2),(1,2,1),(3,2,2),(2,3,1),(2,4,1),(3,5,1),(3,6,1),(1,31,1),(1,32,1),(1,33,1),(1,34,1),(2,35,1),(2,36,1),(2,37,1),(2,38,1),(3,39,1),(3,40,1),(3,41,1),(3,42,1),(1,61,1),(2,62,1),(3,63,1),(1,91,1),(2,92,1),(3,93,1),(1,101,1),(1,102,1),(2,103,1),(2,104,1),(3,105,1),(3,106,1),(1,121,1),(2,122,1),(3,123,1),(1,141,1),(1,142,1),(1,143,1),(1,144,1),(1,145,1),(2,146,1),(2,147,1),(2,148,1),(2,149,1),(2,150,1),(3,151,1),(3,152,1),(3,153,1),(3,154,1),(3,155,1);
/*!40000 ALTER TABLE `Player_has_Cash` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Player_has_Cell`
--

DROP TABLE IF EXISTS `Player_has_Cell`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Player_has_Cell` (
  `PlayerId` int(11) NOT NULL,
  `CellId` int(11) NOT NULL,
  `GameSessionId` int(11) NOT NULL,
  PRIMARY KEY (`PlayerId`,`CellId`,`GameSessionId`),
  UNIQUE KEY `unique_game_cell` (`CellId`,`GameSessionId`),
  KEY `fk_Player_has_Cell_Cell1_idx` (`CellId`),
  KEY `fk_Player_has_Cell_Player1_idx` (`PlayerId`),
  KEY `fk_Player_has_Cell_Attendance1_idx` (`GameSessionId`),
  CONSTRAINT `fk_Player_has_Cell_Player1` FOREIGN KEY (`PlayerId`) REFERENCES `Player` (`PlayerId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cell_Cell1` FOREIGN KEY (`CellId`) REFERENCES `Cell` (`CellId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cell_Attendance1` FOREIGN KEY (`GameSessionId`) REFERENCES `GameSession` (`GameSessionId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Player_has_Cell`
--

LOCK TABLES `Player_has_Cell` WRITE;
/*!40000 ALTER TABLE `Player_has_Cell` DISABLE KEYS */;
INSERT INTO `Player_has_Cell` VALUES (1,2,1),(3,8,1),(2,15,1);
/*!40000 ALTER TABLE `Player_has_Cell` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`user`@`%`*/ /*!50003 TRIGGER unbyuable_cells 
  BEFORE INSERT ON Player_has_Cell
  FOR EACH ROW 
BEGIN
  IF (NEW.CellId IN (1,3,7,10,16,19,21,28,31,34)) THEN
    CALL `The cell is unbuyable`;
  END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `Player_has_Token`
--

DROP TABLE IF EXISTS `Player_has_Token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Player_has_Token` (
  `PlayerId` int(11) NOT NULL,
  `TokenId` int(11) NOT NULL,
  `GameSessionId` int(11) NOT NULL,
  PRIMARY KEY (`PlayerId`,`TokenId`,`GameSessionId`),
  UNIQUE KEY `unique_token_game` (`TokenId`,`GameSessionId`),
  KEY `fk_Player_has_Token_Token1_idx` (`TokenId`),
  KEY `fk_Player_has_Token_Player1_idx` (`PlayerId`),
  KEY `fk_Player_has_Token_GameSession1_idx` (`GameSessionId`),
  CONSTRAINT `fk_Player_has_Token_Player1` FOREIGN KEY (`PlayerId`) REFERENCES `Player` (`PlayerId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Token_Token1` FOREIGN KEY (`TokenId`) REFERENCES `Token` (`TokenId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Token_GameSession1` FOREIGN KEY (`GameSessionId`) REFERENCES `GameSession` (`GameSessionId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Player_has_Token`
--

LOCK TABLES `Player_has_Token` WRITE;
/*!40000 ALTER TABLE `Player_has_Token` DISABLE KEYS */;
INSERT INTO `Player_has_Token` VALUES (1,1,1),(2,2,1),(3,3,1);
/*!40000 ALTER TABLE `Player_has_Token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Token`
--

DROP TABLE IF EXISTS `Token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Token` (
  `TokenId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`TokenId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Token`
--

LOCK TABLES `Token` WRITE;
/*!40000 ALTER TABLE `Token` DISABLE KEYS */;
INSERT INTO `Token` VALUES (1,'Battleship'),(2,'Cannon'),(3,'Cat'),(4,'Iron'),(5,'Penguin'),(6,'Racecar');
/*!40000 ALTER TABLE `Token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'SCHEMA10'
--

--
-- Dumping routines for database 'SCHEMA10'
--

--
-- Final view structure for view `CashPerPlayerPerGame`
--

/*!50001 DROP VIEW IF EXISTS `CashPerPlayerPerGame`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`user`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `CashPerPlayerPerGame` AS select `Player_has_Cash`.`GameSessionId` AS `GameSessionId`,`Player`.`PlayerName` AS `PlayerName`,sum(`Cash`.`Value`) AS `TotalCash` from ((`Player` join `Player_has_Cash` on((`Player`.`PlayerId` = `Player_has_Cash`.`PlayerId`))) join `Cash` on((`Player_has_Cash`.`CashId` = `Cash`.`CashId`))) group by `Player_has_Cash`.`GameSessionId`,`Player`.`PlayerName` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-04-11 10:41:20
