-- MySQL dump 10.13  Distrib 8.0.28, for macos11 (x86_64)
--
-- Host: us-cdbr-east-05.cleardb.net    Database: heroku_54233b1e776c413
-- ------------------------------------------------------
-- Server version	5.6.50-log

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
-- Table structure for table `scores`
--

DROP TABLE IF EXISTS `scores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `scores` (
  `score_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `level_id` int(11) NOT NULL,
  `total_points` int(11) NOT NULL,
  `created_at` datetime NOT NULL,
  `perfect_hits` int(11) NOT NULL,
  `good_hits` int(11) NOT NULL,
  `accuracy` int(11) NOT NULL,
  `max_combo` int(11) NOT NULL,
  PRIMARY KEY (`score_id`),
  UNIQUE KEY `score_id` (`score_id`),
  KEY `user_id` (`user_id`),
  KEY `level_id` (`level_id`),
  CONSTRAINT `scores_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`),
  CONSTRAINT `scores_ibfk_2` FOREIGN KEY (`level_id`) REFERENCES `levels` (`level_id`)
) ENGINE=InnoDB AUTO_INCREMENT=684 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scores`
--

LOCK TABLES `scores` WRITE;
/*!40000 ALTER TABLE `scores` DISABLE KEYS */;
INSERT INTO `scores` VALUES (414,4,14,1750800,'2022-04-29 17:58:07',170,48,63,85),(424,24,14,1923650,'2022-04-29 18:01:07',171,62,61,116),(434,4,24,4990000,'2022-04-29 18:01:15',219,110,64,160),(444,4,14,570450,'2022-04-29 18:10:48',144,76,52,42),(454,4,24,7532350,'2022-04-29 18:27:37',283,90,72,190),(464,24,24,242350,'2022-04-29 18:43:01',124,61,22,22),(474,24,24,0,'2022-04-29 18:50:02',0,0,0,0),(484,4,24,6757650,'2022-04-30 21:42:46',284,65,73,226),(494,24,14,487100,'2022-05-02 16:49:27',117,40,31,47),(504,24,14,28750,'2022-05-02 17:10:04',58,16,8,6),(514,24,24,3138450,'2022-05-02 17:12:22',266,82,67,131),(524,4,14,155550,'2022-05-02 18:20:20',68,35,19,21),(534,4,14,335600,'2022-05-02 19:10:13',83,54,30,29),(544,4,14,82950,'2022-05-02 19:12:55',62,32,16,11),(554,4,14,264700,'2022-05-02 19:14:54',60,34,15,42),(564,4,14,628700,'2022-05-02 19:17:06',90,49,35,54),(574,54,14,2769000,'2022-05-02 23:01:32',159,59,61,146),(584,54,44,751500,'2022-05-02 23:03:40',159,0,1,38),(594,24,14,1007050,'2022-05-03 12:06:24',146,66,57,55),(604,24,24,1872350,'2022-05-03 12:16:24',177,60,26,90),(614,24,24,3978250,'2022-05-03 12:18:42',271,95,72,120),(624,4,14,1645000,'2022-05-03 14:37:24',189,52,69,73),(634,4,14,1399100,'2022-05-03 14:45:23',173,60,62,70),(644,4,14,751900,'2022-05-03 15:47:24',96,82,41,55),(654,4,44,354300,'2022-05-03 17:52:03',151,0,1,25),(664,4,14,1265150,'2022-05-04 17:39:29',151,67,56,77),(674,24,44,1222200,'2022-05-04 23:20:08',157,0,1,83);
/*!40000 ALTER TABLE `scores` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-04 18:45:22