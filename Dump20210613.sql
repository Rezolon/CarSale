-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: carsale
-- ------------------------------------------------------
-- Server version	8.0.23

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
-- Table structure for table `car(nalichiye)`
--

DROP TABLE IF EXISTS `car(nalichiye)`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `car(nalichiye)` (
  `id_car` int NOT NULL AUTO_INCREMENT,
  `id_model` int NOT NULL,
  `№kyzova` varchar(45) NOT NULL,
  `№dvigatel` varchar(45) NOT NULL,
  `№PTC` varchar(45) DEFAULT NULL,
  `color` varchar(45) NOT NULL,
  `release` varchar(45) NOT NULL,
  `buying` int DEFAULT NULL,
  PRIMARY KEY (`id_car`),
  KEY `fk_toCar_idx` (`id_model`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_toCar` FOREIGN KEY (`id_model`) REFERENCES `modeli` (`id_model`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `car(nalichiye)`
--

LOCK TABLES `car(nalichiye)` WRITE;
/*!40000 ALTER TABLE `car(nalichiye)` DISABLE KEYS */;
INSERT INTO `car(nalichiye)` VALUES (16,25,'XT22247FR2','XTG22215441','09888222','белый','2016',0),(17,21,'TY2221112F','PG22212121','08776611','синий','2013',0),(18,19,'LX22125777','X2212121T4','08255444','белый','2018',0),(19,24,'GT22121544','TG22221111','08077444','зеленый','2018',0),(20,12,'XTA2099222','TF54545458','50884444','белый','2014',0),(21,20,'R221212433','RH12121244','21212111','желтый','2015',1),(22,22,'TR9022243F','HY22111222','80451112','белый','2016',1),(23,23,'FR2221211F','FH22215555','06909390','серый','2017',1);
/*!40000 ALTER TABLE `car(nalichiye)` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `complektacii`
--

DROP TABLE IF EXISTS `complektacii`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `complektacii` (
  `id_complektacii` int NOT NULL AUTO_INCREMENT,
  `id_car` int NOT NULL,
  `id_kyzov` int NOT NULL,
  `id_privod` int NOT NULL,
  `mest` int NOT NULL,
  `id_dvigatel'` int NOT NULL,
  `id_Ob"yem dvigatelya` int NOT NULL,
  `loshadinyye sily` int NOT NULL,
  `id_KPP` int NOT NULL,
  PRIMARY KEY (`id_complektacii`),
  KEY `fk_key_idx` (`id_kyzov`),
  KEY `fk_key1_idx` (`id_privod`),
  KEY `fk_key2_idx` (`id_dvigatel'`),
  KEY `fc_key3_idx` (`id_Ob"yem dvigatelya`),
  KEY `fk_key4_idx` (`id_KPP`),
  KEY `fk_key5_idx1` (`id_car`),
  CONSTRAINT `fc_key3` FOREIGN KEY (`id_Ob"yem dvigatelya`) REFERENCES `ob"yem dvigatelya` (`id_Ob"yem dvigatelya`),
  CONSTRAINT `fk_ComplectToCar` FOREIGN KEY (`id_car`) REFERENCES `car(nalichiye)` (`id_car`),
  CONSTRAINT `fk_key` FOREIGN KEY (`id_kyzov`) REFERENCES `kyzov` (`id_kyzov`),
  CONSTRAINT `fk_key1` FOREIGN KEY (`id_privod`) REFERENCES `privod` (`id_privod`),
  CONSTRAINT `fk_key2` FOREIGN KEY (`id_dvigatel'`) REFERENCES `dvigatel'` (`id_dvigatel'`),
  CONSTRAINT `fk_key4` FOREIGN KEY (`id_KPP`) REFERENCES `kpp` (`id_KPP`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `complektacii`
--

LOCK TABLES `complektacii` WRITE;
/*!40000 ALTER TABLE `complektacii` DISABLE KEYS */;
INSERT INTO `complektacii` VALUES (17,16,1,3,5,2,4,250,5),(18,17,2,1,5,6,4,123,1),(19,18,6,3,7,3,4,260,4),(20,19,4,2,5,6,1,350,3),(21,20,2,1,5,4,2,120,1),(22,21,2,3,5,6,3,69,2),(23,22,1,1,5,3,5,100,1),(24,23,3,1,5,5,4,150,1);
/*!40000 ALTER TABLE `complektacii` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dop_options`
--

DROP TABLE IF EXISTS `dop_options`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dop_options` (
  `id_option` int NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `price` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_option`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dop_options`
--

LOCK TABLES `dop_options` WRITE;
/*!40000 ALTER TABLE `dop_options` DISABLE KEYS */;
INSERT INTO `dop_options` VALUES (1,'Кирамические тормоза','100000'),(2,'Подогрев сидений','15000'),(3,'Подогрев рулевого колеса','10000'),(4,'Кондиционер','60000'),(5,'Спортивный обвес','45000'),(6,'Турбина','120000'),(7,'Система старт\\стоп','30000');
/*!40000 ALTER TABLE `dop_options` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dvigatel'`
--

DROP TABLE IF EXISTS `dvigatel'`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dvigatel'` (
  `id_dvigatel'` int NOT NULL,
  `name_dvigatel'` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id_dvigatel'`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dvigatel'`
--

LOCK TABLES `dvigatel'` WRITE;
/*!40000 ALTER TABLE `dvigatel'` DISABLE KEYS */;
INSERT INTO `dvigatel'` VALUES (2,'S38B36',54000),(3,'Toyota D4-D',24000),(4,' ВАЗ-21126-77',12000),(5,'LF',25000),(6,'MPI',32000);
/*!40000 ALTER TABLE `dvigatel'` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kpp`
--

DROP TABLE IF EXISTS `kpp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kpp` (
  `id_KPP` int NOT NULL,
  `name_KPP` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id_KPP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kpp`
--

LOCK TABLES `kpp` WRITE;
/*!40000 ALTER TABLE `kpp` DISABLE KEYS */;
INSERT INTO `kpp` VALUES (1,'мех.5 ступк',600000),(2,'мех.6 ступк',120000),(3,'автомат(Mersedes)',560000),(4,'автомат.7 супк',657000),(5,'авомат.6 ступк',50000);
/*!40000 ALTER TABLE `kpp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kyzov`
--

DROP TABLE IF EXISTS `kyzov`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kyzov` (
  `id_kyzov` int NOT NULL,
  `name_kyzov` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id_kyzov`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kyzov`
--

LOCK TABLES `kyzov` WRITE;
/*!40000 ALTER TABLE `kyzov` DISABLE KEYS */;
INSERT INTO `kyzov` VALUES (1,'седан',400000),(2,'хэчбек',300000),(3,'лифтбэк',450000),(4,'купе',500000),(5,'спортбэк',760000),(6,'универсал',750000);
/*!40000 ALTER TABLE `kyzov` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `marki`
--

DROP TABLE IF EXISTS `marki`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `marki` (
  `id_marki` int NOT NULL,
  `marki` varchar(45) NOT NULL,
  PRIMARY KEY (`id_marki`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marki`
--

LOCK TABLES `marki` WRITE;
/*!40000 ALTER TABLE `marki` DISABLE KEYS */;
INSERT INTO `marki` VALUES (1,'LADA'),(2,'BMW'),(3,'Toyota'),(4,'Lexus'),(5,'Mazda'),(6,'Ford'),(7,'Kia Motors'),(8,'Skoda'),(9,'Mercedes-Benz'),(10,'Audi'),(11,'Opel');
/*!40000 ALTER TABLE `marki` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `modeli`
--

DROP TABLE IF EXISTS `modeli`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `modeli` (
  `id_model` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `price` int DEFAULT NULL,
  `id_marka` int NOT NULL,
  `photo` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id_model`),
  KEY `fk_toModel_idx` (`id_marka`),
  CONSTRAINT `fk_toMode` FOREIGN KEY (`id_marka`) REFERENCES `marki` (`id_marki`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `modeli`
--

LOCK TABLES `modeli` WRITE;
/*!40000 ALTER TABLE `modeli` DISABLE KEYS */;
INSERT INTO `modeli` VALUES (12,'priora',300000,1,'carImage\\priora.png'),(13,'granta',650000,1,'carImage\\granta.png'),(14,'kalina',350000,1,'carImage\\kalina.png'),(15,'corolla',750000,3,'carImage\\corolla.png'),(16,'camry',120000,3,'carImage\\camru.png'),(17,'X3',100000,2,'carImage\\X3.png'),(18,'F90',700000,2,'carImage\\F90.png'),(19,'LX',200000,4,'carImage\\lx.png'),(20,'3',340000,5,'carImage\\4.png'),(21,'focus',450000,6,'carImage\\focus.png'),(22,'rio',730000,7,'carImage\\12.png'),(23,'octaviai',100000,8,'carImage\\octa.png'),(24,'AMG GT',1200000,9,'carImage\\mersedes.png'),(25,'A3',500000,10,'carImage\\A3.png'),(26,'corsa D',360000,11,'carImage\\corsa.png');
/*!40000 ALTER TABLE `modeli` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ob"yem dvigatelya`
--

DROP TABLE IF EXISTS `ob"yem dvigatelya`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ob"yem dvigatelya` (
  `id_Ob"yem dvigatelya` int NOT NULL,
  `name_Ob"yem dvigatelya` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id_Ob"yem dvigatelya`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ob"yem dvigatelya`
--

LOCK TABLES `ob"yem dvigatelya` WRITE;
/*!40000 ALTER TABLE `ob"yem dvigatelya` DISABLE KEYS */;
INSERT INTO `ob"yem dvigatelya` VALUES (1,'turbo V8 ',546000),(2,'16 клапанный ',100000),(3,'8 клапанный',600000),(4,'3.6 атмосферный',450000),(5,'1.2 атмосферный',120000),(6,'3.5 gt',749000);
/*!40000 ALTER TABLE `ob"yem dvigatelya` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `id_order` int NOT NULL AUTO_INCREMENT,
  `id_car` int NOT NULL,
  `date` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `patronomyc` varchar(45) DEFAULT NULL,
  `passport` varchar(45) DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `price` int DEFAULT NULL,
  PRIMARY KEY (`id_order`),
  KEY `fk_OrderToCar` (`id_car`),
  CONSTRAINT `fk_OrderToCar` FOREIGN KEY (`id_car`) REFERENCES `car(nalichiye)` (`id_car`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (16,21,'10.06.2021','Тагирова','Яна','Альфридовна','80 16 502222','+7 (555) 555-5555',425000);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders_options`
--

DROP TABLE IF EXISTS `orders_options`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders_options` (
  `id_orders_options` int NOT NULL AUTO_INCREMENT,
  `id_order` int DEFAULT NULL,
  `id_option` int DEFAULT NULL,
  PRIMARY KEY (`id_orders_options`),
  KEY `fk_ord_idx` (`id_order`),
  KEY `fk_opt_idx` (`id_option`),
  CONSTRAINT `fk_opt` FOREIGN KEY (`id_option`) REFERENCES `dop_options` (`id_option`),
  CONSTRAINT `fk_ord` FOREIGN KEY (`id_order`) REFERENCES `orders` (`id_order`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders_options`
--

LOCK TABLES `orders_options` WRITE;
/*!40000 ALTER TABLE `orders_options` DISABLE KEYS */;
INSERT INTO `orders_options` VALUES (41,16,2),(42,16,3),(43,16,4);
/*!40000 ALTER TABLE `orders_options` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `privod`
--

DROP TABLE IF EXISTS `privod`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `privod` (
  `id_privod` int NOT NULL,
  `name_privod` varchar(45) NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`id_privod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `privod`
--

LOCK TABLES `privod` WRITE;
/*!40000 ALTER TABLE `privod` DISABLE KEYS */;
INSERT INTO `privod` VALUES (1,'передний привод',120000),(2,'задний привод',220000),(3,'полный привод',360000);
/*!40000 ALTER TABLE `privod` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id_users` int NOT NULL,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `type_users` varchar(45) NOT NULL,
  PRIMARY KEY (`id_users`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'REZOLON','123','Admin'),(2,'admin','123','Admin'),(3,'rab','123','User');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-13 23:22:29
