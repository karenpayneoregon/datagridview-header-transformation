USE [master]
GO
/****** Object:  Database [ComputedSample2]    Script Date: 8/12/2023 5:37:15 AM ******/
CREATE DATABASE [ComputedSample2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ComputedSample2', FILENAME = N'C:\Users\paynek\ComputedSample2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ComputedSample2_log', FILENAME = N'C:\Users\paynek\ComputedSample2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ComputedSample2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ComputedSample2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ComputedSample2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ComputedSample2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ComputedSample2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ComputedSample2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ComputedSample2] SET ARITHABORT OFF 
GO
ALTER DATABASE [ComputedSample2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ComputedSample2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ComputedSample2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ComputedSample2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ComputedSample2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ComputedSample2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ComputedSample2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ComputedSample2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ComputedSample2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ComputedSample2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ComputedSample2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ComputedSample2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ComputedSample2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ComputedSample2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ComputedSample2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ComputedSample2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ComputedSample2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ComputedSample2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ComputedSample2] SET  MULTI_USER 
GO
ALTER DATABASE [ComputedSample2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ComputedSample2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ComputedSample2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ComputedSample2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ComputedSample2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ComputedSample2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ComputedSample2] SET QUERY_STORE = OFF
GO
USE [ComputedSample2]
GO
/****** Object:  Table [dbo].[ApplicationSettings]    Script Date: 8/12/2023 5:37:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppName] [nvarchar](max) NOT NULL,
	[VersionMajor] [int] NOT NULL,
	[VersionMinor] [int] NOT NULL,
	[VersionBuild] [int] NOT NULL,
	[VersionRevison] [int] NOT NULL,
	[Identifier] [int] NULL,
	[TheVersion]  AS ((((((replace(str([VersionMajor]),' ','')+'.')+replace(str([VersionMinor]),' ',''))+'.')+replace(str([VersionBuild]),' ',''))+'.')+replace(str([VersionRevison]),' ','')),
 CONSTRAINT [PK_WebPageSettings1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ApplicationSettings] ON 

INSERT [dbo].[ApplicationSettings] ([Id], [AppName], [VersionMajor], [VersionMinor], [VersionBuild], [VersionRevison], [Identifier]) VALUES (1, N'App1', 2, 9, 1, 4, 1)
INSERT [dbo].[ApplicationSettings] ([Id], [AppName], [VersionMajor], [VersionMinor], [VersionBuild], [VersionRevison], [Identifier]) VALUES (2, N'App2', 2, 0, 0, 2, 2)
INSERT [dbo].[ApplicationSettings] ([Id], [AppName], [VersionMajor], [VersionMinor], [VersionBuild], [VersionRevison], [Identifier]) VALUES (3, N'App3', 3, 2, 0, 22, 3)
SET IDENTITY_INSERT [dbo].[ApplicationSettings] OFF
GO
USE [master]
GO
ALTER DATABASE [ComputedSample2] SET  READ_WRITE 
GO
