/*
Database: nihongo_voca
Generate Date: Tuesday, March 15, 2016 12:00:53 AM
*********************************************************************
*/

USE [nihongo_voca]
GO
/* ******** Drop Foreign Key ********/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvocacategories_VocaSetID_msvocasets_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vocacategories]'))
ALTER TABLE [dbo].[ms_vocacategories] DROP CONSTRAINT [FK_msvocacategories_VocaSetID_msvocasets_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvocakanjis_VocabularyID_msvocabularies_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vocakanjis]'))
ALTER TABLE [dbo].[ms_vocakanjis] DROP CONSTRAINT [FK_msvocakanjis_VocabularyID_msvocabularies_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvocakanjis_KanjiID_mskanjis_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vocakanjis]'))
ALTER TABLE [dbo].[ms_vocakanjis] DROP CONSTRAINT [FK_msvocakanjis_KanjiID_mskanjis_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mskanjiexamples_KanjiID_mskanjis_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_kanjiexamples]'))
ALTER TABLE [dbo].[ms_kanjiexamples] DROP CONSTRAINT [FK_mskanjiexamples_KanjiID_mskanjis_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mskanjiexamples_VocabularyID_msvocabularies_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_kanjiexamples]'))
ALTER TABLE [dbo].[ms_kanjiexamples] DROP CONSTRAINT [FK_mskanjiexamples_VocabularyID_msvocabularies_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msanswers_VocabularyID_msvocabularies_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_answers]'))
ALTER TABLE [dbo].[ms_answers] DROP CONSTRAINT [FK_msanswers_VocabularyID_msvocabularies_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msanswers_KanjiID_mskanjis_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_answers]'))
ALTER TABLE [dbo].[ms_answers] DROP CONSTRAINT [FK_msanswers_KanjiID_mskanjis_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msanswers_UpdatedBy_msusers_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_answers]'))
ALTER TABLE [dbo].[ms_answers] DROP CONSTRAINT [FK_msanswers_UpdatedBy_msusers_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvocabularydetails_CategoryID_msvocacategories_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vocabularydetails]'))
ALTER TABLE [dbo].[ms_vocabularydetails] DROP CONSTRAINT [FK_msvocabularydetails_CategoryID_msvocacategories_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvocabularydetails_VocabularyID_msvocabularies_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vocabularydetails]'))
ALTER TABLE [dbo].[ms_vocabularydetails] DROP CONSTRAINT [FK_msvocabularydetails_VocabularyID_msvocabularies_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvocabularydetails_KanjiID_mskanjis_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vocabularydetails]'))
ALTER TABLE [dbo].[ms_vocabularydetails] DROP CONSTRAINT [FK_msvocabularydetails_KanjiID_mskanjis_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msuservocabularies_VocaDetailID_msvocabularydetails_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_uservocabularies]'))
ALTER TABLE [dbo].[ms_uservocabularies] DROP CONSTRAINT [FK_msuservocabularies_VocaDetailID_msvocabularydetails_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msuservocabularies_UserID_msusers_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_uservocabularies]'))
ALTER TABLE [dbo].[ms_uservocabularies] DROP CONSTRAINT [FK_msuservocabularies_UserID_msusers_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mstestresults_CategoryID_msvocacategories_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_testresults]'))
ALTER TABLE [dbo].[ms_testresults] DROP CONSTRAINT [FK_mstestresults_CategoryID_msvocacategories_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mstestresults_UserID_msusers_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_testresults]'))
ALTER TABLE [dbo].[ms_testresults] DROP CONSTRAINT [FK_mstestresults_UserID_msusers_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msregistedvocasets_VocaSetID_msvocasets_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_registedvocasets]'))
ALTER TABLE [dbo].[ms_registedvocasets] DROP CONSTRAINT [FK_msregistedvocasets_VocaSetID_msvocasets_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msregistedvocasets_UserID_msusers_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_registedvocasets]'))
ALTER TABLE [dbo].[ms_registedvocasets] DROP CONSTRAINT [FK_msregistedvocasets_UserID_msusers_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_msvouchers_VocaSetID_msvocasets_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_vouchers]'))
ALTER TABLE [dbo].[ms_vouchers] DROP CONSTRAINT [FK_msvouchers_VocaSetID_msvocasets_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mspaymenthistories_VocaSetID_msvocasets_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_paymenthistories]'))
ALTER TABLE [dbo].[ms_paymenthistories] DROP CONSTRAINT [FK_mspaymenthistories_VocaSetID_msvocasets_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mspaymenthistories_UserID_msusers_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_paymenthistories]'))
ALTER TABLE [dbo].[ms_paymenthistories] DROP CONSTRAINT [FK_mspaymenthistories_UserID_msusers_ID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mspaymenthistories_VoucherID_msvouchers_ID]')AND parent_object_id = OBJECT_ID(N'[dbo].[ms_paymenthistories]'))
ALTER TABLE [dbo].[ms_paymenthistories] DROP CONSTRAINT [FK_mspaymenthistories_VoucherID_msvouchers_ID]
GO
/* ******** Object:  Index [IdxUnique] - Drop ********/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_users]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_users] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocasets]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_vocasets] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocacategories]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_vocacategories] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocabularies]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_vocabularies] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_kanjis]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_kanjis] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocakanjis]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_vocakanjis] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_kanjiexamples]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_kanjiexamples] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_answers]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_answers] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocabularydetails]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_vocabularydetails] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_uservocabularies]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_uservocabularies] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_testresults]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_testresults] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ms_vouchers]') AND name = N'IdxUnique')
DROP INDEX [IdxUnique] ON [dbo].[ms_vouchers] WITH ( ONLINE = OFF )
GO


USE [nihongo_voca]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* ***** Object:  Table [dbo].[ms_users]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_users]') AND type in (N'U'))
DROP TABLE [dbo].[ms_users]
GO
CREATE TABLE [dbo].[ms_users](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [UserName] [nvarchar] (50) NOT NULL ,
   [Password] [nvarchar] (256) NOT NULL ,
   [DisplayName] [nvarchar] (150) NOT NULL ,
   [UrlImage] [nvarchar] (150) NULL ,
   [Phone] [nvarchar] (20) NULL ,
   [Email] [nvarchar] (100) NULL ,
   [Address] [nvarchar] (150) NULL ,
   [Description] [ntext] NULL ,
   [Views] [int] NULL ,
   [Status] [nvarchar] (1) NOT NULL ,
   [SystemData] [nvarchar] (1) NOT NULL ,
   [IsAdmin] [nvarchar] (1) NULL ,
   [LoginState] [nvarchar] (1) NULL ,
   [LastVisitedDate] [datetime] NULL ,
   [AccumulatedPoint] [int] NULL ,
   [Point] [int] NULL ,
   [CreatedDate] [datetime] NULL ,
 CONSTRAINT [PK_msusers] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_users] ([UserName])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_Views]  DEFAULT ((0)) FOR [Views]
GO
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_SystemData]  DEFAULT ((0)) FOR [SystemData]
GO
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_LoginState]  DEFAULT ((0)) FOR [LoginState]
GO
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_AccumulatedPoint]  DEFAULT ((0)) FOR [AccumulatedPoint]
GO
ALTER TABLE [dbo].[ms_users] ADD  CONSTRAINT [DF_msusers_Point]  DEFAULT ((0)) FOR [Point]
GO
GO
/* ***** Object:  Table [dbo].[ms_vocasets]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocasets]') AND type in (N'U'))
DROP TABLE [dbo].[ms_vocasets]
GO
CREATE TABLE [dbo].[ms_vocasets](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [Code] [nvarchar] (10) NOT NULL ,
   [Name1] [nvarchar] (150) NOT NULL ,
   [Name2] [nvarchar] (150) NULL ,
   [Name3] [nvarchar] (150) NULL ,
   [Status] [nvarchar] (1) NOT NULL ,
   [Type] [nvarchar] (1) NOT NULL ,
   [IsKanji] [nvarchar] (1) NULL ,
   [PriorityLevel] [int] NULL ,
   [Fee] [decimal] (20,4) NULL ,
   [UsefulLife] [decimal] (2) NULL ,
   [UrlImage] [nvarchar] (150) NULL ,
   [UrlDisplay] [nvarchar] (150) NULL ,
   [NumOfCategories] [int] NULL ,
   [NumOfVocas] [int] NULL ,
   [NumOfRegistedPerson] [int] NULL ,
   [NumOfFinishedPerson] [int] NULL ,
   [IsSequence] [nvarchar] (1) NULL ,
   [Description] [ntext] NULL ,
   [CreatedDate] [datetime] NULL ,
 CONSTRAINT [PK_msvocasets] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_vocasets] ([Code])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_Type]  DEFAULT ((1)) FOR [Type]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_IsKanji]  DEFAULT ((0)) FOR [IsKanji]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_PriorityLevel]  DEFAULT ((5)) FOR [PriorityLevel]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_Fee]  DEFAULT ((0)) FOR [Fee]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_UsefulLife]  DEFAULT ((0)) FOR [UsefulLife]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_NumOfCategories]  DEFAULT ((0)) FOR [NumOfCategories]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_NumOfVocas]  DEFAULT ((0)) FOR [NumOfVocas]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_NumOfRegistedPerson]  DEFAULT ((0)) FOR [NumOfRegistedPerson]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_NumOfFinishedPerson]  DEFAULT ((0)) FOR [NumOfFinishedPerson]
GO
ALTER TABLE [dbo].[ms_vocasets] ADD  CONSTRAINT [DF_msvocasets_IsSequence]  DEFAULT ((0)) FOR [IsSequence]
GO
GO
/* ***** Object:  Table [dbo].[ms_vocacategories]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocacategories]') AND type in (N'U'))
DROP TABLE [dbo].[ms_vocacategories]
GO
CREATE TABLE [dbo].[ms_vocacategories](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [Code] [nvarchar] (10) NOT NULL ,
   [PreviousCode] [nvarchar] (10) NULL ,
   [VocaSetID] [int] NULL ,
   [IsKanji] [nvarchar] (1) NULL ,
   [Name1] [nvarchar] (100) NOT NULL ,
   [Name2] [nvarchar] (100) NULL ,
   [Name3] [nvarchar] (100) NULL ,
   [LineNumber] [int] NOT NULL ,
   [UrlImage] [nvarchar] (150) NULL ,
   [UrlDisplay] [nvarchar] (150) NULL ,
   [NumOfVocas] [int] NULL ,
   [IsTrial] [nvarchar] (1) NULL ,
   [RequiredTimePerVoca] [int] NULL ,
   [TotalRequiredTime] [int] NULL ,
   [Description] [ntext] NULL ,
 CONSTRAINT [PK_msvocacategories] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_vocacategories] ([Code])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_vocacategories] ADD  CONSTRAINT [DF_msvocacategories_IsKanji]  DEFAULT ((0)) FOR [IsKanji]
GO
ALTER TABLE [dbo].[ms_vocacategories] ADD  CONSTRAINT [DF_msvocacategories_NumOfVocas]  DEFAULT ((0)) FOR [NumOfVocas]
GO
ALTER TABLE [dbo].[ms_vocacategories] ADD  CONSTRAINT [DF_msvocacategories_RequiredTimePerVoca]  DEFAULT ((15)) FOR [RequiredTimePerVoca]
GO
ALTER TABLE [dbo].[ms_vocacategories] ADD  CONSTRAINT [DF_msvocacategories_TotalRequiredTime]  DEFAULT ((0)) FOR [TotalRequiredTime]
GO
GO
/* ***** Object:  Table [dbo].[ms_vocabularies]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocabularies]') AND type in (N'U'))
DROP TABLE [dbo].[ms_vocabularies]
GO
CREATE TABLE [dbo].[ms_vocabularies](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [Code] [nvarchar] (10) NOT NULL ,
   [WordClass] [nvarchar] (1) NULL ,
   [DisplayType] [nvarchar] (1) NOT NULL ,
   [Romaji] [nvarchar] (100) NULL ,
   [Hiragana] [nvarchar] (100) NULL ,
   [Romaji2] [nvarchar] (100) NULL ,
   [Hiragana2] [nvarchar] (100) NULL ,
   [Romaji_Katakana] [nvarchar] (100) NULL ,
   [Katakana] [nvarchar] (100) NULL ,
   [Romaji_Katakana2] [nvarchar] (100) NULL ,
   [Katakana2] [nvarchar] (100) NULL ,
   [Kanji] [nvarchar] (100) NULL ,
   [VMeaning] [nvarchar] (100) NULL ,
   [Description] [ntext] NULL ,
   [Type] [nvarchar] (1) NULL ,
   [UrlImage] [nvarchar] (150) NULL ,
   [UrlAudio] [nvarchar] (150) NULL ,
   [UrlAudio_Katakana] [nvarchar] (150) NULL ,
   [HasDiacritic] [nvarchar] (1) NULL ,
   [HasCombination] [nvarchar] (1) NULL ,
   [HasTsu] [nvarchar] (1) NULL ,
   [HasLongSound] [nvarchar] (1) NULL ,
 CONSTRAINT [PK_msvocabularies] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_vocabularies] ([Romaji],[Kanji])
GO
GO
/* ***** Object:  Table [dbo].[ms_kanjis]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_kanjis]') AND type in (N'U'))
DROP TABLE [dbo].[ms_kanjis]
GO
CREATE TABLE [dbo].[ms_kanjis](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [Code] [nvarchar] (10) NOT NULL ,
   [Kanji] [nvarchar] (50) NULL ,
   [Pinyin] [nvarchar] (50) NULL ,
   [Writing] [nvarchar] (100) NULL ,
   [Remembering] [nvarchar] (250) NULL ,
   [Strokes] [int] NULL ,
   [OnReading] [nvarchar] (100) NULL ,
   [KunReading] [nvarchar] (100) NULL ,
   [VMeaning] [nvarchar] (50) NULL ,
   [Description] [ntext] NULL ,
   [UrlImage] [nvarchar] (150) NULL ,
   [UrlAudio] [nvarchar] (150) NULL ,
 CONSTRAINT [PK_mskanjis] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_kanjis] ([Kanji])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_kanjis] ADD  CONSTRAINT [DF_mskanjis_Strokes]  DEFAULT ((1)) FOR [Strokes]
GO
GO
/* ***** Object:  Table [dbo].[ms_vocakanjis]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocakanjis]') AND type in (N'U'))
DROP TABLE [dbo].[ms_vocakanjis]
GO
CREATE TABLE [dbo].[ms_vocakanjis](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [VocabularyID] [int] NOT NULL ,
   [KanjiID] [int] NOT NULL ,
 CONSTRAINT [PK_msvocakanjis] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_vocakanjis] ([VocabularyID],[KanjiID])
GO
GO
/* ***** Object:  Table [dbo].[ms_kanjiexamples]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_kanjiexamples]') AND type in (N'U'))
DROP TABLE [dbo].[ms_kanjiexamples]
GO
CREATE TABLE [dbo].[ms_kanjiexamples](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [KanjiID] [int] NOT NULL ,
   [VocabularyID] [int] NOT NULL ,
 CONSTRAINT [PK_mskanjiexamples] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_kanjiexamples] ([KanjiID],[VocabularyID])
GO
GO
/* ***** Object:  Table [dbo].[ms_answers]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_answers]') AND type in (N'U'))
DROP TABLE [dbo].[ms_answers]
GO
CREATE TABLE [dbo].[ms_answers](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [VocabularyID] [int] NULL ,
   [KanjiID] [int] NULL ,
   [Answer] [nvarchar] (100) NULL ,
   [Description] [ntext] NULL ,
   [LastUserResponseDate] [datetime] NULL ,
   [LastUserResponse] [nvarchar] (250) NULL ,
   [UpdatedDate] [datetime] NULL ,
   [UpdatedBy] [int] NULL ,
 CONSTRAINT [PK_msanswers] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_answers] ([VocabularyID],[KanjiID],[Answer])
GO
GO
/* ***** Object:  Table [dbo].[ms_vocabularydetails]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_vocabularydetails]') AND type in (N'U'))
DROP TABLE [dbo].[ms_vocabularydetails]
GO
CREATE TABLE [dbo].[ms_vocabularydetails](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [CategoryID] [int] NOT NULL ,
   [VocabularyID] [int] NULL ,
   [KanjiID] [int] NULL ,
   [LineNumber] [int] NOT NULL ,
 CONSTRAINT [PK_msvocabularydetails] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_vocabularydetails] ([CategoryID],[VocabularyID],[KanjiID])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_vocabularydetails] ADD  CONSTRAINT [DF_msvocabularydetails_LineNumber]  DEFAULT ((1)) FOR [LineNumber]
GO
GO
/* ***** Object:  Table [dbo].[ms_uservocabularies]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_uservocabularies]') AND type in (N'U'))
DROP TABLE [dbo].[ms_uservocabularies]
GO
CREATE TABLE [dbo].[ms_uservocabularies](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [VocaDetailID] [int] NOT NULL ,
   [UserID] [int] NOT NULL ,
   [Level] [int] NULL ,
   [HasLearnt] [nvarchar] (1) NULL ,
   [StartDate] [datetime] NULL ,
   [EndDate] [datetime] NULL ,
   [HasMarked] [nvarchar] (1) NULL ,
   [Description] [ntext] NULL ,
   [UserDefine] [nvarchar] (250) NULL ,
   [UpdatedDate] [datetime] NULL ,
   [UpdatedBy] [int] NULL ,
 CONSTRAINT [PK_msuservocabularies] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_uservocabularies] ([VocaDetailID],[UserID])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_uservocabularies] ADD  CONSTRAINT [DF_msuservocabularies_HasMarked]  DEFAULT ((0)) FOR [HasMarked]
GO
GO
/* ***** Object:  Table [dbo].[ms_testresults]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_testresults]') AND type in (N'U'))
DROP TABLE [dbo].[ms_testresults]
GO
CREATE TABLE [dbo].[ms_testresults](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [Code] [nvarchar] (100) NULL ,
   [CategoryID] [int] NOT NULL ,
   [UserID] [int] NOT NULL ,
   [CreateDate] [datetime] NULL ,
   [NumOfVocas] [int] NULL ,
   [NumOfCorrectVocas] [int] NULL ,
   [IsPass] [nvarchar] (1) NULL ,
   [RequiredTimePerVoca] [int] NULL ,
   [TotalRequiredTime] [int] NULL ,
   [CompletedTime] [int] NULL ,
   [Status] [nvarchar] (1) NOT NULL ,
   [Description] [ntext] NULL ,
 CONSTRAINT [PK_mstestresults] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_testresults] ([Code])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_testresults] ADD  CONSTRAINT [DF_mstestresults_RequiredTimePerVoca]  DEFAULT ((15)) FOR [RequiredTimePerVoca]
GO
ALTER TABLE [dbo].[ms_testresults] ADD  CONSTRAINT [DF_mstestresults_TotalRequiredTime]  DEFAULT ((0)) FOR [TotalRequiredTime]
GO
ALTER TABLE [dbo].[ms_testresults] ADD  CONSTRAINT [DF_mstestresults_CompletedTime]  DEFAULT ((0)) FOR [CompletedTime]
GO
ALTER TABLE [dbo].[ms_testresults] ADD  CONSTRAINT [DF_mstestresults_Status]  DEFAULT ((1)) FOR [Status]
GO
GO
/* ***** Object:  Table [dbo].[ms_registedvocasets]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_registedvocasets]') AND type in (N'U'))
DROP TABLE [dbo].[ms_registedvocasets]
GO
CREATE TABLE [dbo].[ms_registedvocasets](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [VocaSetID] [int] NOT NULL ,
   [UserID] [int] NOT NULL ,
   [StartDate] [datetime] NULL ,
   [EndDate] [datetime] NULL ,
   [NumOfMonths] [int] NULL ,
   [NumOfDays] [int] NULL ,
   [Status] [nvarchar] (1) NOT NULL ,
   [Description] [ntext] NULL ,
 CONSTRAINT [PK_msregistedvocasets] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_registedvocasets] ADD  CONSTRAINT [DF_msregistedvocasets_Status]  DEFAULT ((1)) FOR [Status]
GO
GO
/* ***** Object:  Table [dbo].[ms_vouchers]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_vouchers]') AND type in (N'U'))
DROP TABLE [dbo].[ms_vouchers]
GO
CREATE TABLE [dbo].[ms_vouchers](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [Code] [nvarchar] (20) NULL ,
   [VocaSetID] [int] NOT NULL ,
   [VocaSetFee] [decimal] (20,4) NULL ,
   [DecreasePercent] [decimal] (5,2) NULL ,
   [DecreaseFee] [decimal] (20,4) NULL ,
   [RemainFee] [decimal] (20,4) NULL ,
   [EffectiveStartDate] [datetime] NULL ,
   [EffectiveEndDate] [datetime] NULL ,
   [Status] [nvarchar] (1) NOT NULL ,
   [Description] [ntext] NULL ,
 CONSTRAINT [PK_msvouchers] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* ***********Object:  Index [IdxUnique] ************/
CREATE UNIQUE CLUSTERED INDEX IdxUnique  ON [dbo].[ms_vouchers] ([Code])
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_vouchers] ADD  CONSTRAINT [DF_msvouchers_VocaSetFee]  DEFAULT ((0)) FOR [VocaSetFee]
GO
ALTER TABLE [dbo].[ms_vouchers] ADD  CONSTRAINT [DF_msvouchers_DecreasePercent]  DEFAULT ((0)) FOR [DecreasePercent]
GO
ALTER TABLE [dbo].[ms_vouchers] ADD  CONSTRAINT [DF_msvouchers_DecreaseFee]  DEFAULT ((0)) FOR [DecreaseFee]
GO
ALTER TABLE [dbo].[ms_vouchers] ADD  CONSTRAINT [DF_msvouchers_RemainFee]  DEFAULT ((0)) FOR [RemainFee]
GO
ALTER TABLE [dbo].[ms_vouchers] ADD  CONSTRAINT [DF_msvouchers_Status]  DEFAULT ((1)) FOR [Status]
GO
GO
/* ***** Object:  Table [dbo].[ms_paymenthistories]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ms_paymenthistories]') AND type in (N'U'))
DROP TABLE [dbo].[ms_paymenthistories]
GO
CREATE TABLE [dbo].[ms_paymenthistories](
   [ID] [int] IDENTITY(1,1)  NOT NULL ,
   [VocaSetID] [int] NOT NULL ,
   [UserID] [int] NOT NULL ,
   [PaymentDate] [datetime] NOT NULL ,
   [ReceivedDate] [datetime] NOT NULL ,
   [Fee] [decimal] (20,4) NOT NULL ,
   [PaymentMethod] [nvarchar] (1) NOT NULL ,
   [CardCode] [nvarchar] (100) NULL ,
   [CardSeri] [nvarchar] (100) NULL ,
   [CardName] [nvarchar] (100) NULL ,
   [FullName] [nvarchar] (150) NULL ,
   [Email] [nvarchar] (150) NULL ,
   [Phone] [nvarchar] (20) NULL ,
   [Address] [nvarchar] (150) NULL ,
   [BankName] [nvarchar] (150) NULL ,
   [VoucherID] [int] NULL ,
   [Status] [nvarchar] (1) NOT NULL ,
   [Description] [ntext] NULL ,
 CONSTRAINT [PK_mspaymenthistories] PRIMARY KEY NONCLUSTERED 
 (
     [ID] Asc
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* *********** Set default value ************/
ALTER TABLE [dbo].[ms_paymenthistories] ADD  CONSTRAINT [DF_mspaymenthistories_Fee]  DEFAULT ((0)) FOR [Fee]
GO
ALTER TABLE [dbo].[ms_paymenthistories] ADD  CONSTRAINT [DF_mspaymenthistories_Status]  DEFAULT ((1)) FOR [Status]
GO
GO
/* ******** Create Foreign Key ********/
ALTER TABLE [dbo].[ms_vocacategories]  WITH CHECK ADD  CONSTRAINT [FK_msvocacategories_VocaSetID_msvocasets_ID] FOREIGN KEY([VocaSetID]) REFERENCES [dbo].[ms_vocasets] ([ID])
GO
ALTER TABLE [dbo].[ms_vocakanjis]  WITH CHECK ADD  CONSTRAINT [FK_msvocakanjis_VocabularyID_msvocabularies_ID] FOREIGN KEY([VocabularyID]) REFERENCES [dbo].[ms_vocabularies] ([ID])
GO
ALTER TABLE [dbo].[ms_vocakanjis]  WITH CHECK ADD  CONSTRAINT [FK_msvocakanjis_KanjiID_mskanjis_ID] FOREIGN KEY([KanjiID]) REFERENCES [dbo].[ms_kanjis] ([ID])
GO
ALTER TABLE [dbo].[ms_kanjiexamples]  WITH CHECK ADD  CONSTRAINT [FK_mskanjiexamples_KanjiID_mskanjis_ID] FOREIGN KEY([KanjiID]) REFERENCES [dbo].[ms_kanjis] ([ID])
GO
ALTER TABLE [dbo].[ms_kanjiexamples]  WITH CHECK ADD  CONSTRAINT [FK_mskanjiexamples_VocabularyID_msvocabularies_ID] FOREIGN KEY([VocabularyID]) REFERENCES [dbo].[ms_vocabularies] ([ID])
GO
ALTER TABLE [dbo].[ms_answers]  WITH CHECK ADD  CONSTRAINT [FK_msanswers_VocabularyID_msvocabularies_ID] FOREIGN KEY([VocabularyID]) REFERENCES [dbo].[ms_vocabularies] ([ID])
GO
ALTER TABLE [dbo].[ms_answers]  WITH CHECK ADD  CONSTRAINT [FK_msanswers_KanjiID_mskanjis_ID] FOREIGN KEY([KanjiID]) REFERENCES [dbo].[ms_kanjis] ([ID])
GO
ALTER TABLE [dbo].[ms_answers]  WITH CHECK ADD  CONSTRAINT [FK_msanswers_UpdatedBy_msusers_ID] FOREIGN KEY([UpdatedBy]) REFERENCES [dbo].[ms_users] ([ID])
GO
ALTER TABLE [dbo].[ms_vocabularydetails]  WITH CHECK ADD  CONSTRAINT [FK_msvocabularydetails_CategoryID_msvocacategories_ID] FOREIGN KEY([CategoryID]) REFERENCES [dbo].[ms_vocacategories] ([ID])
GO
ALTER TABLE [dbo].[ms_vocabularydetails]  WITH CHECK ADD  CONSTRAINT [FK_msvocabularydetails_VocabularyID_msvocabularies_ID] FOREIGN KEY([VocabularyID]) REFERENCES [dbo].[ms_vocabularies] ([ID])
GO
ALTER TABLE [dbo].[ms_vocabularydetails]  WITH CHECK ADD  CONSTRAINT [FK_msvocabularydetails_KanjiID_mskanjis_ID] FOREIGN KEY([KanjiID]) REFERENCES [dbo].[ms_kanjis] ([ID])
GO
ALTER TABLE [dbo].[ms_uservocabularies]  WITH CHECK ADD  CONSTRAINT [FK_msuservocabularies_VocaDetailID_msvocabularydetails_ID] FOREIGN KEY([VocaDetailID]) REFERENCES [dbo].[ms_vocabularydetails] ([ID])
GO
ALTER TABLE [dbo].[ms_uservocabularies]  WITH CHECK ADD  CONSTRAINT [FK_msuservocabularies_UserID_msusers_ID] FOREIGN KEY([UserID]) REFERENCES [dbo].[ms_users] ([ID])
GO
ALTER TABLE [dbo].[ms_testresults]  WITH CHECK ADD  CONSTRAINT [FK_mstestresults_CategoryID_msvocacategories_ID] FOREIGN KEY([CategoryID]) REFERENCES [dbo].[ms_vocacategories] ([ID])
GO
ALTER TABLE [dbo].[ms_testresults]  WITH CHECK ADD  CONSTRAINT [FK_mstestresults_UserID_msusers_ID] FOREIGN KEY([UserID]) REFERENCES [dbo].[ms_users] ([ID])
GO
ALTER TABLE [dbo].[ms_registedvocasets]  WITH CHECK ADD  CONSTRAINT [FK_msregistedvocasets_VocaSetID_msvocasets_ID] FOREIGN KEY([VocaSetID]) REFERENCES [dbo].[ms_vocasets] ([ID])
GO
ALTER TABLE [dbo].[ms_registedvocasets]  WITH CHECK ADD  CONSTRAINT [FK_msregistedvocasets_UserID_msusers_ID] FOREIGN KEY([UserID]) REFERENCES [dbo].[ms_users] ([ID])
GO
ALTER TABLE [dbo].[ms_vouchers]  WITH CHECK ADD  CONSTRAINT [FK_msvouchers_VocaSetID_msvocasets_ID] FOREIGN KEY([VocaSetID]) REFERENCES [dbo].[ms_vocasets] ([ID])
GO
ALTER TABLE [dbo].[ms_paymenthistories]  WITH CHECK ADD  CONSTRAINT [FK_mspaymenthistories_VocaSetID_msvocasets_ID] FOREIGN KEY([VocaSetID]) REFERENCES [dbo].[ms_vocasets] ([ID])
GO
ALTER TABLE [dbo].[ms_paymenthistories]  WITH CHECK ADD  CONSTRAINT [FK_mspaymenthistories_UserID_msusers_ID] FOREIGN KEY([UserID]) REFERENCES [dbo].[ms_users] ([ID])
GO
ALTER TABLE [dbo].[ms_paymenthistories]  WITH CHECK ADD  CONSTRAINT [FK_mspaymenthistories_VoucherID_msvouchers_ID] FOREIGN KEY([VoucherID]) REFERENCES [dbo].[ms_vouchers] ([ID])
GO

