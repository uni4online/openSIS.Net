USE [master]
GO
/****** Object:  Database [OpensisV2]    Script Date: 08/09/2020 1:06:49 AM ******/
CREATE DATABASE [OpensisV2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OpensisV2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\OpensisV2.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OpensisV2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\OpensisV2_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OpensisV2] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OpensisV2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OpensisV2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OpensisV2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OpensisV2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OpensisV2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OpensisV2] SET ARITHABORT OFF 
GO
ALTER DATABASE [OpensisV2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OpensisV2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OpensisV2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OpensisV2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OpensisV2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OpensisV2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OpensisV2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OpensisV2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OpensisV2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OpensisV2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OpensisV2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OpensisV2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OpensisV2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OpensisV2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OpensisV2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OpensisV2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OpensisV2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OpensisV2] SET RECOVERY FULL 
GO
ALTER DATABASE [OpensisV2] SET  MULTI_USER 
GO
ALTER DATABASE [OpensisV2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OpensisV2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OpensisV2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OpensisV2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [OpensisV2] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OpensisV2', N'ON'
GO
USE [OpensisV2]
GO
/****** Object:  Table [dbo].[Table_membership]    Script Date: 08/09/2020 1:06:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Table_membership](
	[Tenant_id] [uniqueidentifier] NOT NULL,
	[School_id] [int] NOT NULL,
	[id] [int] NOT NULL,
	[user_id] [int] NULL,
	[access] [varchar](max) NULL,
	[weekly_update] [bit] NULL,
 CONSTRAINT [PK_Table_membership] PRIMARY KEY CLUSTERED 
(
	[Tenant_id] ASC,
	[School_id] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Table_Plans]    Script Date: 08/09/2020 1:06:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Table_Plans](
	[Tenant_id] [uniqueidentifier] NOT NULL,
	[School_id] [int] NOT NULL,
	[id] [int] NOT NULL,
	[name] [varchar](100) NULL,
	[max_api_checks] [int] NULL,
	[features] [varbinary](max) NULL,
 CONSTRAINT [PK_Table_Plans] PRIMARY KEY CLUSTERED 
(
	[Tenant_id] ASC,
	[School_id] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Table_School_Detail]    Script Date: 08/09/2020 1:06:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_School_Detail](
	[Tenant_Id] [uniqueidentifier] NULL,
	[School_Id] [int] NULL,
	[Affiliation] [nchar](100) NULL,
	[Associations] [nchar](100) NULL,
	[Locale] [nchar](100) NULL,
	[Lowest_Grade_Level] [nchar](100) NULL,
	[Highest_Grade_Level] [nchar](100) NULL,
	[Date_School_Opened] [date] NULL,
	[Date_School_Closed] [date] NULL,
	[Status] [bit] NULL,
	[Gender] [nchar](6) NULL,
	[Internet] [bit] NULL,
	[Electricity] [bit] NULL,
	[Telephone] [nchar](20) NULL,
	[Fax] [nchar](20) NULL,
	[Website] [nchar](150) NULL,
	[Email] [nchar](100) NULL,
	[Twitter] [nchar](100) NULL,
	[Facebook] [nchar](100) NULL,
	[Instagram] [nchar](100) NULL,
	[Youtube] [nchar](100) NULL,
	[LinkedIn] [nchar](100) NULL,
	[Name_of_Principal] [nchar](100) NULL,
	[Name_of_Assistant_Principal] [nchar](100) NULL,
	[School_Logo] [image] NULL,
	[Running_Water] [bit] NULL,
	[Main_Source_of_Drinking_Water] [nchar](100) NULL,
	[Currently_Available] [bit] NULL,
	[Female_Toilet_Type] [nchar](50) NULL,
	[Total_Female_Toilets] [smallint] NULL,
	[Total_Female_Toilets_Usable] [smallint] NULL,
	[Female_Toilet_Accessibility] [nchar](50) NULL,
	[Male_Toilet_Type] [nchar](50) NULL,
	[Total_Male_Toilets] [smallint] NULL,
	[Total_Male_Toilets_Usable] [smallint] NULL,
	[Male_Toilet_Accessibility] [nchar](50) NULL,
	[Comon_Toilet_Type] [nchar](50) NULL,
	[Total_Common_Toilets] [smallint] NULL,
	[Total_Common_Toilets_Usable] [smallint] NULL,
	[Common_Toilet_Accessibility] [nchar](50) NULL,
	[Handwashing_Available] [bit] NULL,
	[Soap_and_Water_Available] [bit] NULL,
	[Hygene_Education] [nchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Table_School_Master]    Script Date: 08/09/2020 1:06:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Table_School_Master](
	[Tenant_Id] [uniqueidentifier] NOT NULL,
	[School_Id] [int] NOT NULL,
	[School_Alt_Id] [nchar](10) NULL,
	[School_State_Id] [nchar](10) NULL,
	[School_District_Id] [nchar](50) NULL,
	[School_Level] [nchar](50) NULL,
	[School_Classification] [nchar](50) NULL,
	[School_Name] [nvarchar](100) NULL,
	[Alternate_Name] [nvarchar](100) NULL,
	[Street_Address_1] [nvarchar](150) NULL,
	[Street_Address_2] [nvarchar](150) NULL,
	[City] [char](50) NULL,
	[County] [char](50) NULL,
	[Division] [char](50) NULL,
	[State] [char](50) NULL,
	[District] [char](50) NULL,
	[Zip] [nchar](10) NULL,
	[Country] [char](50) NULL,
	[GeoPosition] [geography] NULL,
	[Current_Period_ends] [datetime] NULL,
	[Max_api_checks] [int] NULL,
	[Features] [varchar](max) NULL,
	[Plan_id] [int] NULL,
	[Created_By] [char](50) NULL,
	[Date_Created] [datetime] NULL,
	[Modified_By] [char](50) NULL,
	[Date_Modifed] [datetime] NULL,
 CONSTRAINT [PK_Table_School_Master] PRIMARY KEY CLUSTERED 
(
	[Tenant_Id] ASC,
	[School_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Table_User_Master]    Script Date: 08/09/2020 1:06:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Table_User_Master](
	[Tenant_Id] [uniqueidentifier] NOT NULL,
	[School_id] [int] NOT NULL,
	[User_id] [int] NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[EmailAddress] [varchar](150) NOT NULL,
	[PasswordHash] [binary](32) NOT NULL,
 CONSTRAINT [PK_Table_User_Master_1] PRIMARY KEY CLUSTERED 
(
	[Tenant_Id] ASC,
	[School_id] ASC,
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Table_membership]  WITH CHECK ADD  CONSTRAINT [FK_Table_membership_Table_School_Master] FOREIGN KEY([Tenant_id], [School_id])
REFERENCES [dbo].[Table_School_Master] ([Tenant_Id], [School_Id])
GO
ALTER TABLE [dbo].[Table_membership] CHECK CONSTRAINT [FK_Table_membership_Table_School_Master]
GO
ALTER TABLE [dbo].[Table_membership]  WITH CHECK ADD  CONSTRAINT [FK_Table_membership_Table_User_Master] FOREIGN KEY([Tenant_id], [School_id], [user_id])
REFERENCES [dbo].[Table_User_Master] ([Tenant_Id], [School_id], [User_id])
GO
ALTER TABLE [dbo].[Table_membership] CHECK CONSTRAINT [FK_Table_membership_Table_User_Master]
GO
ALTER TABLE [dbo].[Table_School_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Table_School_Detail_Table_School_Master] FOREIGN KEY([Tenant_Id], [School_Id])
REFERENCES [dbo].[Table_School_Master] ([Tenant_Id], [School_Id])
GO
ALTER TABLE [dbo].[Table_School_Detail] CHECK CONSTRAINT [FK_Table_School_Detail_Table_School_Master]
GO
ALTER TABLE [dbo].[Table_School_Master]  WITH CHECK ADD  CONSTRAINT [FK_Table_School_Master_Table_Plans] FOREIGN KEY([Tenant_Id], [School_Id], [Plan_id])
REFERENCES [dbo].[Table_Plans] ([Tenant_id], [School_id], [id])
GO
ALTER TABLE [dbo].[Table_School_Master] CHECK CONSTRAINT [FK_Table_School_Master_Table_Plans]
GO
USE [master]
GO
ALTER DATABASE [OpensisV2] SET  READ_WRITE 
GO
