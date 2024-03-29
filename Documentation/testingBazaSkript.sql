USE [master]
GO
/****** Object:  Database [testingsystem]    Script Date: 21/08/2021 13:13:05 ******/
CREATE DATABASE [testingsystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'testingsystem', FILENAME = N'/var/opt/mssql/data/testingsystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'testingsystem_log', FILENAME = N'/var/opt/mssql/data/testingsystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [testingsystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [testingsystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [testingsystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [testingsystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [testingsystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [testingsystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [testingsystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [testingsystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [testingsystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [testingsystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [testingsystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [testingsystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [testingsystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [testingsystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [testingsystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [testingsystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [testingsystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [testingsystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [testingsystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [testingsystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [testingsystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [testingsystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [testingsystem] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [testingsystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [testingsystem] SET RECOVERY FULL 
GO
ALTER DATABASE [testingsystem] SET  MULTI_USER 
GO
ALTER DATABASE [testingsystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [testingsystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [testingsystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [testingsystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [testingsystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [testingsystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'testingsystem', N'ON'
GO
ALTER DATABASE [testingsystem] SET QUERY_STORE = OFF
GO
USE [testingsystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerOptions]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OptionText] [nvarchar](256) NULL,
	[IsCorrect] [bit] NOT NULL,
	[QuestionId] [int] NOT NULL,
 CONSTRAINT [PK_AnswerOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examiners]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examiners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[ExternalId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Examiners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ExaminerId] [int] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionText] [nvarchar](1000) NULL,
	[TestId] [smallint] NOT NULL,
	[QuestionScore] [tinyint] NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[GroupId] [smallint] NULL,
	[ExternalId] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTestQuestionResponses]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTestQuestionResponses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Response] [nvarchar](256) NULL,
	[ResponseScore] [int] NOT NULL,
	[StudentTestQuestionId] [int] NOT NULL,
 CONSTRAINT [PK_StudentTestQuestionResponses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTestQuestions]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTestQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[StudentTestStudentId] [int] NULL,
	[StudentTestTestId] [smallint] NULL,
 CONSTRAINT [PK_StudentTestQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTests]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTests](
	[StudentId] [int] NOT NULL,
	[TestId] [smallint] NOT NULL,
	[Score] [int] NOT NULL,
	[IsTestPassed] [bit] NOT NULL,
	[StudentGroupId] [smallint] NULL,
 CONSTRAINT [PK_StudentTests] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ExaminerId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TestScore] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestStatistics]    Script Date: 21/08/2021 13:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestStatistics](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TestId] [smallint] NOT NULL,
	[TestTitle] [nvarchar](max) NULL,
	[ExaminerId] [int] NOT NULL,
	[PercentageOfStudentsWhoPassedTheTest] [int] NOT NULL,
	[NumberOfStudentsWhoTookTheTest] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TestStatistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210412164839_Initial_Create', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210415132123_Added_StudentTest_manyToMany_Relation_table', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210415143828_Updated_StudentTestQuestion', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210420150435_Created_table_TestStatistics', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210420203133_Updated_StudentTest_with_StudentGroupId', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210517164354_Removed_EndDate_from_Test', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210606105146_Updated_Student_with_Email', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210611114900_Updated_Question_with_Type', N'5.0.4')
GO
SET IDENTITY_INSERT [dbo].[AnswerOptions] ON 

INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (1, N'Evropa', 0, 1)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (2, N'Azija', 1, 1)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (3, N'Evropa', 1, 2)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (4, N'Azija', 0, 2)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (5, N'Severna Amerika', 0, 2)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (6, N'Juzna Amerika', 0, 2)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (7, N'Andora', 0, 3)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (8, N'Rusija', 1, 3)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (9, N'San Marino', 0, 3)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (10, N'Kazahstan', 1, 3)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (11, N'Severna Makedonija', 0, 3)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (12, N'Austrija', 1, 4)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (13, N'Argentina', 0, 4)
INSERT [dbo].[AnswerOptions] ([Id], [OptionText], [IsCorrect], [QuestionId]) VALUES (14, N'', 1, 5)
SET IDENTITY_INSERT [dbo].[AnswerOptions] OFF
GO
SET IDENTITY_INSERT [dbo].[Examiners] ON 

INSERT [dbo].[Examiners] ([Id], [FirstName], [LastName], [ExternalId]) VALUES (2, N'Boris', N'Karanovic', N'b8f4e784-8d39-44ef-8d9c-c3dc91ae5f10')
INSERT [dbo].[Examiners] ([Id], [FirstName], [LastName], [ExternalId]) VALUES (3, N'Aleksandra', N'Dotlic', N'43e84b33-5b28-4221-942e-b53fa1d90c13')
INSERT [dbo].[Examiners] ([Id], [FirstName], [LastName], [ExternalId]) VALUES (4, N'Profesor', N'Profesor 1', N'aa317c55-b3b5-4201-928d-93778329834c')
SET IDENTITY_INSERT [dbo].[Examiners] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([Id], [Title], [ExaminerId]) VALUES (1, N'Geografija 1 - Boris', 2)
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([Id], [QuestionText], [TestId], [QuestionScore], [Type]) VALUES (1, N'Da li je veca Evropa ili Azija ?', 1, 1, 1)
INSERT [dbo].[Questions] ([Id], [QuestionText], [TestId], [QuestionScore], [Type]) VALUES (2, N'Najmanji kontinent je? ', 1, 1, 3)
INSERT [dbo].[Questions] ([Id], [QuestionText], [TestId], [QuestionScore], [Type]) VALUES (3, N'Selektuj dve najvece zemlje', 1, 2, 3)
INSERT [dbo].[Questions] ([Id], [QuestionText], [TestId], [QuestionScore], [Type]) VALUES (4, N'Selektuj planinsku zemlju', 1, 1, 1)
INSERT [dbo].[Questions] ([Id], [QuestionText], [TestId], [QuestionScore], [Type]) VALUES (5, N'Navedi pet zemalja koje pocinju na slovo S ', 1, 1, 2)
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (1, N'Ana', N'Ilic', 1, N'f206b127-9245-4f36-9ed4-4b76cb35d875', N'anailic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (2, N'Marko', N'Rakic', 1, N'9ee11b84-3662-4d9b-82b5-a0d66e8d11b1', N'markorakic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (3, N'Bojana', N'Pavlovic', 1, N'3b962598-5653-449f-bf13-6ed73ec70d1b', N'bojanapavlovic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (4, N'Jovana', N'Milic', 1, N'71a9e170-152b-47df-af6c-83c0b36b48ec', N'jovanamilic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (5, N'Filip', N'Filipovic', 1, N'568dd332-c625-4cb5-98ef-129b7d64ad09', N'filipfilipovic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (6, N'Nenad', N'Pljakic', 1, N'00b8a9a7-d0e7-4c6e-9a54-a7e116205e50', N'jovanam505@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (7, N'Marija', N'Mihajlovic', 1, N'faabadf4-83d8-4f62-9228-322d8315ce49', N'marijamihajlovic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (8, N'Petar', N'Ilic', NULL, N'1b147494-7bef-453f-b437-4b07beaf1da0', N'petarilic@mejl.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (9, N'Sladjana', N'Stefanovic', NULL, N'e1f971b3-f8d7-463a-9a28-97e6e4eea783', N'sladjanastefanovic@mejl.rs')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (10, N'Luka', N'Petrovic', NULL, N'fe7ceb03-5270-4c18-a9dc-c5b3bf7549f0', N'lukapetrovic@mejl.co.rs')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (11, N'Nevena', N'Peric', NULL, N'cd36aa8c-9537-427d-983c-14f279ea24d7', N'nevenaperic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (12, N'Jana', N'Jovanovic', NULL, N'c03f03fd-9d92-41d2-905b-3bfe0e0c3bf0', N'janajovanovic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (13, N'Natasa', N'Markovic', NULL, N'd4ee2ee4-a132-45f8-a816-2b4a51b1850d', N'natasamarkovic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (14, N'Vera', N'Matovic', NULL, N'7e740c4d-5e85-4c07-b241-cbcbe49a7bd4', N'veramatovic@gmail.com')
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [GroupId], [ExternalId], [Email]) VALUES (15, N'Mitar', N'Miric', NULL, N'680a7d79-7c3c-441d-8e90-7b156c701124', N'doberman@mitar.miric')
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentTestQuestionResponses] ON 

INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (1, N'Azija', 1, 1)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (2, N'Azija', 0, 2)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (3, N'Kazahstan', 1, 3)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (4, N'Rusija', 1, 3)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (5, N'Austrija', 1, 4)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (6, N'Tekst', 1, 5)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (7, N'Azija', 1, 6)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (8, N'Evropa', 1, 7)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (9, N'Rusija', 1, 8)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (10, N'Kazahstan', 1, 8)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (11, N'Austrija', 1, 9)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (12, N'Tekst', 1, 10)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (13, N'Evropa', 0, 11)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (14, N'Azija', 0, 12)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (15, N'Andora', 0, 13)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (16, N'Severna Makedonija', 0, 13)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (17, N'Argentina', 0, 14)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (18, N'Azija', 1, 15)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (19, N'Evropa', 1, 16)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (20, N'Rusija', 1, 17)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (21, N'Kazahstan', 1, 17)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (22, N'Argentina', 0, 18)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (23, N'Azija', 1, 19)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (24, N'Evropa', 1, 20)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (25, N'Severna Makedonija', 0, 21)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (26, N'San Marino', 0, 21)
INSERT [dbo].[StudentTestQuestionResponses] ([Id], [Response], [ResponseScore], [StudentTestQuestionId]) VALUES (27, N'Argentina', 0, 22)
SET IDENTITY_INSERT [dbo].[StudentTestQuestionResponses] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentTestQuestions] ON 

INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (1, 1, 1, 1, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (2, 2, 0, 1, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (3, 3, 2, 1, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (4, 4, 1, 1, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (5, 5, 1, 1, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (6, 1, 1, 2, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (7, 2, 1, 2, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (8, 3, 2, 2, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (9, 4, 1, 2, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (10, 5, 1, 2, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (11, 1, 0, 3, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (12, 2, 0, 3, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (13, 3, 0, 3, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (14, 4, 0, 3, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (15, 1, 1, 4, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (16, 2, 1, 4, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (17, 3, 2, 4, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (18, 4, 0, 4, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (19, 1, 1, 5, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (20, 2, 1, 5, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (21, 3, 0, 5, 1)
INSERT [dbo].[StudentTestQuestions] ([Id], [QuestionId], [Score], [StudentTestStudentId], [StudentTestTestId]) VALUES (22, 4, 0, 5, 1)
SET IDENTITY_INSERT [dbo].[StudentTestQuestions] OFF
GO
INSERT [dbo].[StudentTests] ([StudentId], [TestId], [Score], [IsTestPassed], [StudentGroupId]) VALUES (1, 1, 5, 1, 1)
INSERT [dbo].[StudentTests] ([StudentId], [TestId], [Score], [IsTestPassed], [StudentGroupId]) VALUES (2, 1, 6, 1, 1)
INSERT [dbo].[StudentTests] ([StudentId], [TestId], [Score], [IsTestPassed], [StudentGroupId]) VALUES (3, 1, 0, 0, 1)
INSERT [dbo].[StudentTests] ([StudentId], [TestId], [Score], [IsTestPassed], [StudentGroupId]) VALUES (4, 1, 4, 1, 1)
INSERT [dbo].[StudentTests] ([StudentId], [TestId], [Score], [IsTestPassed], [StudentGroupId]) VALUES (5, 1, 2, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([Id], [Title], [ExaminerId], [IsActive], [TestScore], [StartDate]) VALUES (1, N'Geografija 1', 2, 1, 6, CAST(N'2021-08-21T11:30:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[TestStatistics] ON 

INSERT [dbo].[TestStatistics] ([Id], [TestId], [TestTitle], [ExaminerId], [PercentageOfStudentsWhoPassedTheTest], [NumberOfStudentsWhoTookTheTest], [Date]) VALUES (1, 1, N'Geografija 1', 2, 60, 5, CAST(N'2021-08-21T12:41:15.1552234' AS DateTime2))
SET IDENTITY_INSERT [dbo].[TestStatistics] OFF
GO
/****** Object:  Index [IX_AnswerOptions_QuestionId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_AnswerOptions_QuestionId] ON [dbo].[AnswerOptions]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Groups_ExaminerId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_Groups_ExaminerId] ON [dbo].[Groups]
(
	[ExaminerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_TestId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_Questions_TestId] ON [dbo].[Questions]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Students_GroupId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_Students_GroupId] ON [dbo].[Students]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StudentTestQuestionResponses_StudentTestQuestionId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_StudentTestQuestionResponses_StudentTestQuestionId] ON [dbo].[StudentTestQuestionResponses]
(
	[StudentTestQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StudentTestQuestions_StudentTestStudentId_StudentTestTestId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_StudentTestQuestions_StudentTestStudentId_StudentTestTestId] ON [dbo].[StudentTestQuestions]
(
	[StudentTestStudentId] ASC,
	[StudentTestTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StudentTests_TestId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_StudentTests_TestId] ON [dbo].[StudentTests]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tests_ExaminerId]    Script Date: 21/08/2021 13:13:06 ******/
CREATE NONCLUSTERED INDEX [IX_Tests_ExaminerId] ON [dbo].[Tests]
(
	[ExaminerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT (CONVERT([tinyint],(0))) FOR [Type]
GO
ALTER TABLE [dbo].[StudentTests] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsTestPassed]
GO
ALTER TABLE [dbo].[AnswerOptions]  WITH CHECK ADD  CONSTRAINT [FK_AnswerOptions_Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnswerOptions] CHECK CONSTRAINT [FK_AnswerOptions_Questions_QuestionId]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Examiners_ExaminerId] FOREIGN KEY([ExaminerId])
REFERENCES [dbo].[Examiners] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Examiners_ExaminerId]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Tests_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Tests_TestId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Groups_GroupId]
GO
ALTER TABLE [dbo].[StudentTestQuestionResponses]  WITH CHECK ADD  CONSTRAINT [FK_StudentTestQuestionResponses_StudentTestQuestions_StudentTestQuestionId] FOREIGN KEY([StudentTestQuestionId])
REFERENCES [dbo].[StudentTestQuestions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentTestQuestionResponses] CHECK CONSTRAINT [FK_StudentTestQuestionResponses_StudentTestQuestions_StudentTestQuestionId]
GO
ALTER TABLE [dbo].[StudentTestQuestions]  WITH CHECK ADD  CONSTRAINT [FK_StudentTestQuestions_StudentTests_StudentTestStudentId_StudentTestTestId] FOREIGN KEY([StudentTestStudentId], [StudentTestTestId])
REFERENCES [dbo].[StudentTests] ([StudentId], [TestId])
GO
ALTER TABLE [dbo].[StudentTestQuestions] CHECK CONSTRAINT [FK_StudentTestQuestions_StudentTests_StudentTestStudentId_StudentTestTestId]
GO
ALTER TABLE [dbo].[StudentTests]  WITH CHECK ADD  CONSTRAINT [FK_StudentTests_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentTests] CHECK CONSTRAINT [FK_StudentTests_Students_StudentId]
GO
ALTER TABLE [dbo].[StudentTests]  WITH CHECK ADD  CONSTRAINT [FK_StudentTests_Tests_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentTests] CHECK CONSTRAINT [FK_StudentTests_Tests_TestId]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Examiners_ExaminerId] FOREIGN KEY([ExaminerId])
REFERENCES [dbo].[Examiners] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Examiners_ExaminerId]
GO
USE [master]
GO
ALTER DATABASE [testingsystem] SET  READ_WRITE 
GO
