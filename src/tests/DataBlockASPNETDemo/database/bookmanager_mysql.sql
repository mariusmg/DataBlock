SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT;
SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS;
SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION;
SET NAMES utf8;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE=NO_AUTO_VALUE_ON_ZERO */;


CREATE DATABASE /*!32312 IF NOT EXISTS*/ `bookmanager`;
USE `bookmanager`;
CREATE TABLE `book` (
  `BookId` int(11) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL default '',
  `Pages` int(11) default '0',
  `ISBN` varchar(100) default '',
  `Genre` varchar(100) default '',
  `Grade` smallint(6) default '0',
  PRIMARY KEY  (`BookId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
INSERT INTO `book` (`BookId`,`Name`,`Pages`,`ISBN`,`Genre`,`Grade`) VALUES (1,'Atlantis',90,NULL,'fantasy',8),(2,'Winnetou',800,'4444456','western',9),(3,'C# by Samples',112,NULL,'tehnical',3);
CREATE TABLE `bookauthors` (
  `Id` int(11) NOT NULL auto_increment,
  `AuthorId` int(11) NOT NULL default '0',
  `BookId` int(11) NOT NULL default '0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
INSERT INTO `bookauthors` (`Id`,`AuthorId`,`BookId`) VALUES (1,1,1),(2,2,1),(3,3,1),(4,3,4);
CREATE TABLE `author` (
  `AuthorId` int(11) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL default '',
  `Age` int(11) default '0',
  `Location` varchar(50) default '',
  PRIMARY KEY  (`AuthorId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
INSERT INTO `author` (`AuthorId`,`Name`,`Age`,`Location`) VALUES (1,'Karl May',70,'Kholn'),(2,'Mihai Eminescu',453,'Iasi'),(3,'Jules Verne',56,'Paris'),(4,'Ion Creanga',33,'Gura Humorului');
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT;
SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS;
SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
