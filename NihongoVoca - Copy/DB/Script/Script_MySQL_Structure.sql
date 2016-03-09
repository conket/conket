/*
SQLyog Enterprise - MySQL GUI v6.5
MySQL - 5.1.39-community : Database - ivs_wms
Generate Date: 21 May, 2015 9:03:01 AM
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;
/*!40101 SET SQL_MODE=''*/;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

create database if not exists `ivs_wms`;

USE `ivs_wms`;

/*Table structure for table `ms_fixedassetstype` */
DROP TABLE IF EXISTS `ms_fixedassetstype`;
CREATE TABLE `ms_fixedassetstype` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `Code` varchar(10) NOT NULL ,
   `Name1` varchar(100) NOT NULL ,
   `Name2` varchar(100) DEFAULT NULL,
   `Name3` varchar(100) DEFAULT NULL,
   `Status` varchar(1) NOT NULL  DEFAULT '0',
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`Code`),
KEY `FK_msfixedassetstype_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_msfixedassetstype_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_msfixedassetstype_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_msfixedassetstype_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `ms_capital` */
DROP TABLE IF EXISTS `ms_capital`;
CREATE TABLE `ms_capital` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `Code` varchar(10) NOT NULL ,
   `Name1` varchar(100) NOT NULL ,
   `Name2` varchar(100) DEFAULT NULL,
   `Name3` varchar(100) DEFAULT NULL,
   `Status` varchar(1) NOT NULL  DEFAULT '0',
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`Code`),
KEY `FK_mscapital_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_mscapital_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_mscapital_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_mscapital_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `ms_acquisitionreason` */
DROP TABLE IF EXISTS `ms_acquisitionreason`;
CREATE TABLE `ms_acquisitionreason` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `Code` varchar(10) NOT NULL ,
   `Name1` varchar(100) NOT NULL ,
   `Name2` varchar(100) DEFAULT NULL,
   `Name3` varchar(100) DEFAULT NULL,
   `Type` varchar(1) DEFAULT NULL,
   `Status` varchar(1) NOT NULL  DEFAULT '0',
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`Code`),
KEY `FK_msacquisitionreas_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_msacquisitionreas_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_msacquisitionreas_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_msacquisitionreas_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fixedassets` */
DROP TABLE IF EXISTS `t_fixedassets`;
CREATE TABLE `t_fixedassets` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `Code` varchar(30) NOT NULL ,
   `Name1` varchar(300) NOT NULL ,
   `Name2` varchar(300) DEFAULT NULL,
   `Name3` varchar(300) DEFAULT NULL,
   `Type` varchar(10) DEFAULT NULL,
   `Department` varchar(10) NOT NULL ,
   `TransferDate` datetime DEFAULT NULL,
   `IncreaseDate` datetime DEFAULT NULL,
   `AbandonmentDate` datetime DEFAULT NULL,
   `DecreaseDate` datetime DEFAULT NULL,
   `AcquisitionReason` varchar(10) NOT NULL ,
   `Capital` varchar(10) NOT NULL ,
   `MFGYear` varchar(4) DEFAULT NULL,
   `MadeIn` varchar(20) DEFAULT NULL,
   `Manufacturer` varchar(50) DEFAULT NULL,
   `Supplier` varchar(50) DEFAULT NULL,
   `PONo` varchar(20) DEFAULT NULL,
   `AcquisitionDate` datetime DEFAULT NULL,
   `HistoryCostForeignCurrencie` decimal(27,6) DEFAULT NULL,
   `CurrencyUnit` varchar(3) DEFAULT NULL,
   `ExchangeRate` decimal(18,6) DEFAULT NULL,
   `HistoryCost` decimal(27,6) DEFAULT NULL,
   `Increase_Decrease` decimal(27,6) DEFAULT NULL,
   `NotSubjectToDepreciation` varchar(1) NOT NULL ,
   `DeprStartDate` datetime DEFAULT NULL,
   `UsefullLife` int() DEFAULT NULL,
   `MonthlyDepreciationAmount` decimal(27,6) DEFAULT NULL,
   `AccumulatedDepreciation` decimal(27,6) DEFAULT NULL,
   `NetBookValue` decimal(27,6) DEFAULT NULL,
   `RemainingUsefullLife` int() DEFAULT NULL,
   `Status` varchar(1) DEFAULT NULL,
   `Remark` Text DEFAULT NULL,
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`Code`),
KEY `FK_t_ixedassets_Type_msfixedassetstype_Code` (`Type`) ,
KEY `FK_t_ixedassets_Department_msdepartments_Code` (`Department`) ,
KEY `FK_t_ixedassets_AcquisitionReason_msAcquisitionReason_Code` (`AcquisitionReason`) ,
KEY `FK_t_ixedassets_Capital_msCapital_Code` (`Capital`) ,
KEY `FK_t_ixedassets_CurrencyUnit_mscurrency_Code` (`CurrencyUnit`) ,
KEY `FK_t_ixedassets_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_t_ixedassets_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_t_ixedassets_Type_msfixedassetstype_Code` FOREIGN KEY (`Type`) REFERENCES `ms_fixedassetstype` (`Code`),
CONSTRAINT  `FK_t_ixedassets_Department_msdepartments_Code` FOREIGN KEY (`Department`) REFERENCES `ms_departments` (`Code`),
CONSTRAINT  `FK_t_ixedassets_AcquisitionReason_msAcquisitionReason_Code` FOREIGN KEY (`AcquisitionReason`) REFERENCES `MS_AcquisitionReason` (`Code`),
CONSTRAINT  `FK_t_ixedassets_Capital_msCapital_Code` FOREIGN KEY (`Capital`) REFERENCES `MS_Capital` (`Code`),
CONSTRAINT  `FK_t_ixedassets_CurrencyUnit_mscurrency_Code` FOREIGN KEY (`CurrencyUnit`) REFERENCES `ms_currency` (`Code`),
CONSTRAINT  `FK_t_ixedassets_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_t_ixedassets_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_spareparts` */
DROP TABLE IF EXISTS `t_fa_spareparts`;
CREATE TABLE `t_fa_spareparts` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `FACode` varchar(30) NOT NULL ,
   `SPCode` varchar(10) NOT NULL ,
   `SPName` varchar(100) DEFAULT NULL,
   `Unit` varchar(100) DEFAULT NULL,
   `Quatity` varchar(100) DEFAULT NULL,
   `Value` decimal(27,6) DEFAULT NULL,
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`FACode`,`SPCode`),
KEY `FK_t_a_spareparts_FACode_t_ixedassets_Code` (`FACode`) ,
KEY `FK_t_a_spareparts_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_t_a_spareparts_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_t_a_spareparts_FACode_t_ixedassets_Code` FOREIGN KEY (`FACode`) REFERENCES `t_fixedassets` (`Code`),
CONSTRAINT  `FK_t_a_spareparts_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_t_a_spareparts_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_depricationclosing` */
DROP TABLE IF EXISTS `t_fa_depricationclosing`;
CREATE TABLE `t_fa_depricationclosing` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `Period` varchar(6) NOT NULL ,
   `MonthlyDAmount` decimal(27,6) DEFAULT NULL,
   `Description` varchar(150) DEFAULT NULL,
   `ProcessingFlg` varchar(1) DEFAULT NULL,
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`Period`),
KEY `FK_t_a_depricationcl_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_t_a_depricationcl_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_t_a_depricationcl_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_t_a_depricationcl_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_monthlydeprication` */
DROP TABLE IF EXISTS `t_fa_monthlydeprication`;
CREATE TABLE `t_fa_monthlydeprication` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `Period` varchar(6) NOT NULL ,
   `FACode` varchar(30) NOT NULL ,
   `Department` varchar(10) NOT NULL ,
   `DeprStartDate` datetime DEFAULT NULL,
   `UsefullLife` int() DEFAULT NULL,
   `MonthlyDepreciationAmount` decimal(27,6) DEFAULT NULL,
   `HistoryCost` decimal(27,6) DEFAULT '0',
   `Increase_Decrease` decimal(27,6) DEFAULT '0',
   `AccumulatedDepreciation` decimal(27,6) DEFAULT '0',
   `NetBookValue` decimal(27,6) DEFAULT '0',
   `UpdateFlg` varchar(1) DEFAULT NULL,
   `Insert_PIC` varchar(10) DEFAULT NULL,
   `Insert_Date` datetime DEFAULT NULL,
   `Insert_PG` varchar(50) DEFAULT NULL,
   `Update_PIC` varchar(10) DEFAULT NULL,
   `Update_Date` datetime DEFAULT NULL,
   `Update_PG` varchar(50) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`Period`,`FACode`),
KEY `FK_t_a_monthlydepric_FACode_t_ixedassets_Code` (`FACode`) ,
KEY `FK_t_a_monthlydepric_Department_msdepartments_Code` (`Department`) ,
KEY `FK_t_a_monthlydepric_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_t_a_monthlydepric_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_t_a_monthlydepric_FACode_t_ixedassets_Code` FOREIGN KEY (`FACode`) REFERENCES `t_fixedassets` (`Code`),
CONSTRAINT  `FK_t_a_monthlydepric_Department_msdepartments_Code` FOREIGN KEY (`Department`) REFERENCES `ms_departments` (`Code`),
CONSTRAINT  `FK_t_a_monthlydepric_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_t_a_monthlydepric_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_transaction` */
DROP TABLE IF EXISTS `t_transaction`;
CREATE TABLE `t_transaction` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `DocumentDate` datetime NOT NULL ,
   `Type` int(1) NOT NULL ,
   `Remark` text DEFAULT NULL,
   `Insert_PIC` varchar(10) NOT NULL ,
   `Insert_Date` datetime NOT NULL ,
   `Insert_PG` varchar(50) NOT NULL ,
   `Update_PIC` varchar(10) NOT NULL ,
   `Update_Date` datetime NOT NULL ,
   `Update_PG` varchar(50) NOT NULL ,
PRIMARY KEY (`ID`),
KEY `FK_t_ransaction_Insert_PIC_msusers_Code` (`Insert_PIC`) ,
KEY `FK_t_ransaction_Update_PIC_msusers_Code` (`Update_PIC`) ,
CONSTRAINT  `FK_t_ransaction_Insert_PIC_msusers_Code` FOREIGN KEY (`Insert_PIC`) REFERENCES `ms_users` (`Code`),
CONSTRAINT  `FK_t_ransaction_Update_PIC_msusers_Code` FOREIGN KEY (`Update_PIC`) REFERENCES `ms_users` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_adjustment` */
DROP TABLE IF EXISTS `t_fa_adjustment`;
CREATE TABLE `t_fa_adjustment` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `ParentID` int(11) NOT NULL ,
   `FACode` varchar(30) NOT NULL ,
   `Department` varchar(10) NOT NULL ,
   `History Cost` decimal(27,6) NOT NULL  DEFAULT '0',
   `RemainDeprecibale` decimal(27,6) NOT NULL  DEFAULT '0',
   `RemainDeprecibaleAdj` decimal(27,6) NOT NULL ,
   `RemainUsefulllife` int() NOT NULL ,
   `RemainUsefulllifeAdj` int() NOT NULL ,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`ParentID`,`FACode`),
KEY `FK_t_a_adjustment_ParentID_t_ransaction_ID` (`ParentID`) ,
KEY `FK_t_a_adjustment_FACode_t_ixedassets_Code` (`FACode`) ,
KEY `FK_t_a_adjustment_Department_msdepartments_Code` (`Department`) ,
CONSTRAINT  `FK_t_a_adjustment_ParentID_t_ransaction_ID` FOREIGN KEY (`ParentID`) REFERENCES `t_transaction` (`ID`),
CONSTRAINT  `FK_t_a_adjustment_FACode_t_ixedassets_Code` FOREIGN KEY (`FACode`) REFERENCES `t_fixedassets` (`Code`),
CONSTRAINT  `FK_t_a_adjustment_Department_msdepartments_Code` FOREIGN KEY (`Department`) REFERENCES `ms_departments` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_transfer` */
DROP TABLE IF EXISTS `t_fa_transfer`;
CREATE TABLE `t_fa_transfer` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `ParentID` int(11) NOT NULL ,
   `FACode` varchar(30) NOT NULL ,
   `HistoryCost` decimal(27,6) DEFAULT '0',
   `RemainDeprecibale` decimal(27,6) DEFAULT '0',
   `FrDepartment` varchar(10) DEFAULT NULL,
   `ToDepartment` varchar(10) DEFAULT NULL,
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`ParentID`,`FACode`),
KEY `FK_t_a_transfer_ParentID_t_ransaction_ID` (`ParentID`) ,
KEY `FK_t_a_transfer_FACode_t_ixedassets_Code` (`FACode`) ,
KEY `FK_t_a_transfer_FrDepartment_msdepartments_Code` (`FrDepartment`) ,
KEY `FK_t_a_transfer_ToDepartment_msdepartments_Code` (`ToDepartment`) ,
CONSTRAINT  `FK_t_a_transfer_ParentID_t_ransaction_ID` FOREIGN KEY (`ParentID`) REFERENCES `t_transaction` (`ID`),
CONSTRAINT  `FK_t_a_transfer_FACode_t_ixedassets_Code` FOREIGN KEY (`FACode`) REFERENCES `t_fixedassets` (`Code`),
CONSTRAINT  `FK_t_a_transfer_FrDepartment_msdepartments_Code` FOREIGN KEY (`FrDepartment`) REFERENCES `ms_departments` (`Code`),
CONSTRAINT  `FK_t_a_transfer_ToDepartment_msdepartments_Code` FOREIGN KEY (`ToDepartment`) REFERENCES `ms_departments` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_abandonment` */
DROP TABLE IF EXISTS `t_fa_abandonment`;
CREATE TABLE `t_fa_abandonment` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `ParentID` int(11) NOT NULL ,
   `FACode` varchar(30) NOT NULL ,
   `Department` varchar(10) NOT NULL ,
   `HistoryCost` decimal(27,6) NOT NULL  DEFAULT '0',
   `RemainingDeprecibaleAmount` decimal(27,6) NOT NULL  DEFAULT '0',
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`ParentID`,`FACode`),
KEY `FK_t_a_abandonment_ParentID_t_ransaction_ID` (`ParentID`) ,
KEY `FK_t_a_abandonment_FACode_t_ixedassets_Code` (`FACode`) ,
KEY `FK_t_a_abandonment_Department_msdepartments_Code` (`Department`) ,
CONSTRAINT  `FK_t_a_abandonment_ParentID_t_ransaction_ID` FOREIGN KEY (`ParentID`) REFERENCES `t_transaction` (`ID`),
CONSTRAINT  `FK_t_a_abandonment_FACode_t_ixedassets_Code` FOREIGN KEY (`FACode`) REFERENCES `t_fixedassets` (`Code`),
CONSTRAINT  `FK_t_a_abandonment_Department_msdepartments_Code` FOREIGN KEY (`Department`) REFERENCES `ms_departments` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `t_fa_decrease` */
DROP TABLE IF EXISTS `t_fa_decrease`;
CREATE TABLE `t_fa_decrease` (
   `ID` int(11) NOT NULL  AUTO_INCREMENT,
   `ParentID` int(11) NOT NULL ,
   `FACode` varchar(30) NOT NULL ,
   `Department` varchar(10) NOT NULL ,
   `HistoryCost` decimal(27,6) NOT NULL  DEFAULT '0',
   `RemainingDeprecibaleAmount` decimal(27,6) NOT NULL  DEFAULT '0',
PRIMARY KEY (`ID`),
UNIQUE KEY `idxUnique` (`ParentID`,`FACode`),
KEY `FK_t_a_decrease_ParentID_t_ransaction_ID` (`ParentID`) ,
KEY `FK_t_a_decrease_FACode_t_ixedassets_Code` (`FACode`) ,
KEY `FK_t_a_decrease_Department_msdepartments_Code` (`Department`) ,
CONSTRAINT  `FK_t_a_decrease_ParentID_t_ransaction_ID` FOREIGN KEY (`ParentID`) REFERENCES `t_transaction` (`ID`),
CONSTRAINT  `FK_t_a_decrease_FACode_t_ixedassets_Code` FOREIGN KEY (`FACode`) REFERENCES `t_fixedassets` (`Code`),
CONSTRAINT  `FK_t_a_decrease_Department_msdepartments_Code` FOREIGN KEY (`Department`) REFERENCES `ms_departments` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;

