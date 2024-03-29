USE [master]
GO
/****** Object:  Database [authentication]    Script Date: 21/08/2021 13:11:58 ******/
CREATE DATABASE [authentication]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'authentication', FILENAME = N'/var/opt/mssql/data/authentication.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'authentication_log', FILENAME = N'/var/opt/mssql/data/authentication_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [authentication] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [authentication].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [authentication] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [authentication] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [authentication] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [authentication] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [authentication] SET ARITHABORT OFF 
GO
ALTER DATABASE [authentication] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [authentication] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [authentication] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [authentication] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [authentication] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [authentication] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [authentication] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [authentication] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [authentication] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [authentication] SET  ENABLE_BROKER 
GO
ALTER DATABASE [authentication] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [authentication] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [authentication] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [authentication] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [authentication] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [authentication] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [authentication] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [authentication] SET RECOVERY FULL 
GO
ALTER DATABASE [authentication] SET  MULTI_USER 
GO
ALTER DATABASE [authentication] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [authentication] SET DB_CHAINING OFF 
GO
ALTER DATABASE [authentication] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [authentication] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [authentication] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [authentication] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'authentication', N'ON'
GO
ALTER DATABASE [authentication] SET QUERY_STORE = OFF
GO
USE [authentication]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21/08/2021 13:11:59 ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 21/08/2021 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210331145809_Initial', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210331155254_Removed_AccountId', N'5.0.4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'28204c39-9dcf-4ddf-b998-e364d352a5b1', N'Student', N'STUDENT', N'4890b0a1-a16c-4c1c-bf8b-280fe9500a73')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e4125167-c9a3-4893-8fa3-d072acfee4a7', N'Examiner', N'EXAMINER', N'cd36bfef-07c3-43d9-a0e5-1f515e7b9b4c')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'00b8a9a7-d0e7-4c6e-9a54-a7e116205e50', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1b147494-7bef-453f-b437-4b07beaf1da0', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3b962598-5653-449f-bf13-6ed73ec70d1b', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'568dd332-c625-4cb5-98ef-129b7d64ad09', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'680a7d79-7c3c-441d-8e90-7b156c701124', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'71a9e170-152b-47df-af6c-83c0b36b48ec', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7e740c4d-5e85-4c07-b241-cbcbe49a7bd4', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9ee11b84-3662-4d9b-82b5-a0d66e8d11b1', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c03f03fd-9d92-41d2-905b-3bfe0e0c3bf0', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cd36aa8c-9537-427d-983c-14f279ea24d7', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd4ee2ee4-a132-45f8-a816-2b4a51b1850d', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e1f971b3-f8d7-463a-9a28-97e6e4eea783', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f206b127-9245-4f36-9ed4-4b76cb35d875', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'faabadf4-83d8-4f62-9228-322d8315ce49', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fe7ceb03-5270-4c18-a9dc-c5b3bf7549f0', N'28204c39-9dcf-4ddf-b998-e364d352a5b1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'43e84b33-5b28-4221-942e-b53fa1d90c13', N'e4125167-c9a3-4893-8fa3-d072acfee4a7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'aa317c55-b3b5-4201-928d-93778329834c', N'e4125167-c9a3-4893-8fa3-d072acfee4a7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b8f4e784-8d39-44ef-8d9c-c3dc91ae5f10', N'e4125167-c9a3-4893-8fa3-d072acfee4a7')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'00b8a9a7-d0e7-4c6e-9a54-a7e116205e50', N'jovanam505@gmail.com', N'JOVANAM505@GMAIL.COM', N'jovanam505@gmail.com', N'JOVANAM505@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEAB9EKkoJgxUkfZQRHXq0WbYxr0WtrrrH6oH/OyMhgPWU9/TM9XlpvyM+0SlIzjj6Q==', N'7JLEZAS4KQARMDEF7CGOVC2M74SWRZWH', N'ccb70918-7eea-4fdf-b1eb-2f5f7ead05db', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1b147494-7bef-453f-b437-4b07beaf1da0', N'petarilic@mejl.com', N'PETARILIC@MEJL.COM', N'petarilic@mejl.com', N'PETARILIC@MEJL.COM', 0, N'AQAAAAEAACcQAAAAEMyB6UGTDgD8DUKH6ANdqETQnExvoO48CBw25JpyuA8uYyLk6nDgBzgYRAY2EMbZCQ==', N'CDQP4MJAYRUXNG3HY3YERORBGZOPAWHZ', N'cdf2bdda-604b-4371-b430-3faf92f077cb', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3b962598-5653-449f-bf13-6ed73ec70d1b', N'bojanapavlovic@gmail.com', N'BOJANAPAVLOVIC@GMAIL.COM', N'bojanapavlovic@gmail.com', N'BOJANAPAVLOVIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEPHSITIGX1xzB+C6XKao/Y1qOCKlRk7GerPGOdDH2Zi1BZvEFahWCqSO5kDjH2m1tw==', N'KMVJB3P6NS5WYD5GNJNUHSOCTI4BYICQ', N'd167627f-0f4a-4475-8b90-2f6ceb0f57ad', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'43e84b33-5b28-4221-942e-b53fa1d90c13', N'alexandra.nerandzic@gmail.com', N'ALEXANDRA.NERANDZIC@GMAIL.COM', N'alexandra.nerandzic@gmail.com', N'ALEXANDRA.NERANDZIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEAI0wlFD8hCycF7GJovBPUxQvkqxwP6H3l0hDJa7pfdAQ8fg08XjS8BNUWSV12p+9g==', N'MO6T6TDM424EGCDHOA7WZP3TSZLQ3JPG', N'52b0fc27-b68d-44f9-b55c-6c8dc1bf53bc', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'568dd332-c625-4cb5-98ef-129b7d64ad09', N'filipfilipovic@gmail.com', N'FILIPFILIPOVIC@GMAIL.COM', N'filipfilipovic@gmail.com', N'FILIPFILIPOVIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEC6QtPpQtwe7Ddyrg6RogQVAWRBCB3+28da19VQdvfsVGishZF1dOuCjgqr/J95m9A==', N'7TTQDJYSQ6NBYVJ5HA3MZVZVTPCGFXUR', N'2ca9a272-98f3-40e7-83c2-6847d75b9390', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'680a7d79-7c3c-441d-8e90-7b156c701124', N'doberman@mitar.miric', N'DOBERMAN@MITAR.MIRIC', N'doberman@mitar.miric', N'DOBERMAN@MITAR.MIRIC', 0, N'AQAAAAEAACcQAAAAEGSvQhbedWHsJEdmKJKkvLMFXkCP8wjfQip81FnEXxWVA3BXs3+cAoC2uc4ddpeD2A==', N'R46QUPFRZAUMZSON7VAKAFE4AHMYAFPB', N'e9d44f40-3a20-4855-a5b2-6cb84f8a2d00', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'71a9e170-152b-47df-af6c-83c0b36b48ec', N'jovanamilic@gmail.com', N'JOVANAMILIC@GMAIL.COM', N'jovanamilic@gmail.com', N'JOVANAMILIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEIYF/eCMsNyaGhHnWJk/5uvprXccEHYY0B8TnTEtLSc2vCIQdPcWBFWFZ/kpM83lvw==', N'5PVCO46FZQ23JJSRXRBPIFZNU5KNDOFM', N'1c6aa1db-4153-4ead-9f72-47431bec4867', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7e740c4d-5e85-4c07-b241-cbcbe49a7bd4', N'veramatovic@gmail.com', N'VERAMATOVIC@GMAIL.COM', N'veramatovic@gmail.com', N'VERAMATOVIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECAeHtJBQvxwMXA0fsqb+m6yU75ZlK/SdJ0BexRbumncJEFafcdXibqd3Va9y3yAjg==', N'OT7K5SST5DNEAD6EQOY3I7OXMQPSIGGZ', N'2717b1a7-15a3-43bb-84bf-1a705615d7aa', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9ee11b84-3662-4d9b-82b5-a0d66e8d11b1', N'markorakic@gmail.com', N'MARKORAKIC@GMAIL.COM', N'markorakic@gmail.com', N'MARKORAKIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAED4GJ/ZdyChV2t3RHIaMlVGjLAxf+Ijb91Z/f32wt7RLDhYFXv16dE4OvMTmsWN40g==', N'HBEUT2ILSZMO57OD4F4PTA2NOL3J3DWR', N'1c879263-ac2d-482f-afda-68c712a1dfa1', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'aa317c55-b3b5-4201-928d-93778329834c', N'profesor1@gmail.com', N'PROFESOR1@GMAIL.COM', N'profesor1@gmail.com', N'PROFESOR1@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAENIfSqVQY54QPJXTa9LmL3SVImaB5S1ezM61GsOQ3u8FG8m3rr3s78JQcqEvzDehqQ==', N'KQZ7ZWLG7TSDJ3ZAENMQ5GEUTKOQ4E74', N'9b28c4eb-972e-44d3-9a88-9dc6695e9f39', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b8f4e784-8d39-44ef-8d9c-c3dc91ae5f10', N'boskonet@gmail.com', N'BOSKONET@GMAIL.COM', N'boskonet@gmail.com', N'BOSKONET@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEPYnFEgQ9EPpuvX12qAjZ0FOB1fXOJQLrPG1ULnpeG8klf/DYJfm5kGAkel/gCZzJg==', N'FJ43IIXMR2EOTVUXXSC3ZRP6YHQHN6JP', N'c58a965f-85e5-449d-a58e-df7e518e304c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c03f03fd-9d92-41d2-905b-3bfe0e0c3bf0', N'janajovanovic@gmail.com', N'JANAJOVANOVIC@GMAIL.COM', N'janajovanovic@gmail.com', N'JANAJOVANOVIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKyWwqWbVUiZdfaLiQaismoDzteB5sVoVU8atZBmcgs63uG+SR3/ZKzR1XzaC7Zghg==', N'WBKLTTRYAD6AVIUPLGDKTGVYCCMF5ZIN', N'3f885eaf-a11d-4b99-bdcb-cb2e6fec6540', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'cd36aa8c-9537-427d-983c-14f279ea24d7', N'nevenaperic@gmail.com', N'NEVENAPERIC@GMAIL.COM', N'nevenaperic@gmail.com', N'NEVENAPERIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAENqn85YOd9zXQ4uHm/YUrpUw3eAeVtyX7uoEV/49dl2GmvL2RP/uNpny7EK7U2C7cA==', N'G4V7RSD5HKSS7MKDQGIHZS4WD3AUOXJ5', N'54c6940f-1afc-4248-99e4-d8c7de45b1aa', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd4ee2ee4-a132-45f8-a816-2b4a51b1850d', N'natasamarkovic@gmail.com', N'NATASAMARKOVIC@GMAIL.COM', N'natasamarkovic@gmail.com', N'NATASAMARKOVIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAENVwWRRjWjcJs6wwUeGxt1IIksDGYgQFmwBE1zsuc4AMhXBGlxM69rYlyi8kp99XfA==', N'C3ZDDSW5UBM7TYZ46QTS2DCKBBLHHOPN', N'c35bdb84-9057-49c6-803c-e99314099d6e', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e1f971b3-f8d7-463a-9a28-97e6e4eea783', N'sladjanastefanovic@mejl.rs', N'SLADJANASTEFANOVIC@MEJL.RS', N'sladjanastefanovic@mejl.rs', N'SLADJANASTEFANOVIC@MEJL.RS', 0, N'AQAAAAEAACcQAAAAEKBKOrbFvSLe5Evx35SIaAoGNbgsSAtSAhKK1zQ7uVWaH/WK73geKNW+sn8U3b3Yaw==', N'AXTR27ZTPKA4HI4GCIFA2BOEKEY6MNCU', N'50274f6f-ed94-4d8d-95f9-fe104ffb6981', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f206b127-9245-4f36-9ed4-4b76cb35d875', N'anailic@gmail.com', N'ANAILIC@GMAIL.COM', N'anailic@gmail.com', N'ANAILIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEAtyV75fDFKIAJG81yZPiJ04wWFUY3zZ2vVhBqtrVouE25P+SMGFwJyh0IV2f5OYXQ==', N'2ERCJFSW7B7PXK3SNMEGVFOEDH3KOI7Y', N'40569bd0-c82e-4186-b5f6-a075ffd054cb', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'faabadf4-83d8-4f62-9228-322d8315ce49', N'marijamihajlovic@gmail.com', N'MARIJAMIHAJLOVIC@GMAIL.COM', N'marijamihajlovic@gmail.com', N'MARIJAMIHAJLOVIC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKSUORZgr0ljbGA1cejnfTve5vkLx1I9xuB5X4kxVJnuOM9cowwpJlhL7uN0mBC/kQ==', N'5XXLZKXV6WSFUOW3NVIN4JPJZ3SRXZB4', N'08f39f15-2e49-411c-8f2b-7066a0bd03f7', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fe7ceb03-5270-4c18-a9dc-c5b3bf7549f0', N'lukapetrovic@mejl.co.rs', N'LUKAPETROVIC@MEJL.CO.RS', N'lukapetrovic@mejl.co.rs', N'LUKAPETROVIC@MEJL.CO.RS', 0, N'AQAAAAEAACcQAAAAEJ/8p1AdMVHpY0CFSJzC5+iWWH48DIKcurI2RxtAnVZNTrbba4Ei7c/BdT91fER8BQ==', N'JQDDSFGRVGGYPSZFUAQTZE6RE3LV5O6W', N'3d606ee8-a9e5-453c-958e-032798c6d542', NULL, 0, 0, NULL, 1, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 21/08/2021 13:11:59 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 21/08/2021 13:11:59 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 21/08/2021 13:11:59 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 21/08/2021 13:11:59 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 21/08/2021 13:11:59 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 21/08/2021 13:11:59 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 21/08/2021 13:11:59 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [authentication] SET  READ_WRITE 
GO
