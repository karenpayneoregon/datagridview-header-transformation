USE [master]
GO
/****** 
IMPORTANT Do not run this script w/o changing the database path
******/
CREATE DATABASE [EF.BookCatalog1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EF.BookCatalog1', FILENAME = N'C:\Users\paynek\EF.BookCatalog1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EF.BookCatalog1_log', FILENAME = N'C:\Users\paynek\EF.BookCatalog1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EF.BookCatalog1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EF.BookCatalog1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EF.BookCatalog1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET ARITHABORT OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EF.BookCatalog1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EF.BookCatalog1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EF.BookCatalog1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EF.BookCatalog1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EF.BookCatalog1] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EF.BookCatalog1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EF.BookCatalog1] SET  MULTI_USER 
GO
ALTER DATABASE [EF.BookCatalog1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EF.BookCatalog1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EF.BookCatalog1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EF.BookCatalog1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EF.BookCatalog1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EF.BookCatalog1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EF.BookCatalog1] SET QUERY_STORE = OFF
GO
USE [EF.BookCatalog1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/10/2022 11:03:53 AM ******/
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
/****** Object:  Table [dbo].[Books]    Script Date: 9/10/2022 11:03:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 9/10/2022 11:03:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220910155215_FirstMigration', N'6.0.8')
GO
/****** Object:  Index [IX_Books_CategoryId]    Script Date: 9/10/2022 11:03:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Books_CategoryId] ON [dbo].[Books]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories_CategoryId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Book identifier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Books', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Book title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Books', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Price of book' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Books', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Category key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Books', @level2type=N'COLUMN',@level2name=N'CategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Category identifier' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Category description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'Description'
GO
USE [master]
GO
ALTER DATABASE [EF.BookCatalog1] SET  READ_WRITE 
GO
