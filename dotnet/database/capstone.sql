USE [master]
GO
/****** Object:  Database [final_capstone]    Script Date: 4/10/2023 2:03:34 PM ******/
DROP DATABASE [final_capstone]
CREATE DATABASE [final_capstone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'final_capstone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\final_capstone.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'final_capstone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\final_capstone_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [final_capstone] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [final_capstone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [final_capstone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [final_capstone] SET ARITHABORT OFF 
GO
ALTER DATABASE [final_capstone] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [final_capstone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [final_capstone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [final_capstone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [final_capstone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [final_capstone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [final_capstone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [final_capstone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [final_capstone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [final_capstone] SET  ENABLE_BROKER 
GO
ALTER DATABASE [final_capstone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [final_capstone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [final_capstone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [final_capstone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [final_capstone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [final_capstone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [final_capstone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [final_capstone] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [final_capstone] SET  MULTI_USER 
GO
ALTER DATABASE [final_capstone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [final_capstone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [final_capstone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [final_capstone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [final_capstone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [final_capstone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [final_capstone] SET QUERY_STORE = OFF
GO
USE [final_capstone]
GO
/****** Object:  Table [dbo].[Forum]    Script Date: 4/10/2023 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum](
	[forum_id] [bigint] IDENTITY(1,1) NOT NULL,
	[topic] [nvarchar](50) NOT NULL,
	[user_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[is_visible] [bit] NOT NULL,
 CONSTRAINT [PK_Forum] PRIMARY KEY CLUSTERED 
(
	[forum_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forum_mods]    Script Date: 4/10/2023 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_mods](
	[user_id] [int] NOT NULL,
	[forum_id] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 4/10/2023 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[posts_id] [bigint] IDENTITY(1,1) NOT NULL,
	[post_content] [nvarchar](255) NULL,
	[create_date] [datetime] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[thumbs_up] [bigint] NOT NULL,
	[thumbs_down] [bigint] NOT NULL,
	[visible] [bit] NOT NULL,
	[path] [nvarchar](max) NOT NULL,
	[forum_id] [bigint] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[posts_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Private_Message]    Script Date: 4/10/2023 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Private_Message](
	[message_id] [int] IDENTITY(1,1) NOT NULL,
	[from_user] [int] NOT NULL,
	[to_user] [int] NOT NULL,
	[message] [nvarchar](max) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Private_Message] PRIMARY KEY CLUSTERED 
(
	[message_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 4/10/2023 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
	[user_role] [varchar](50) NOT NULL,
	[Create_date] [datetime] NOT NULL,
	[Last_login] [datetime] NOT NULL,
	[restore_ban_time] [datetime] NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [Create_date], [Last_login], [restore_ban_time], [is_active]) VALUES (1, N'user', N'Jg45HuwT7PZkfuKTz6IB90CtWY4=', N'LHxP4Xh7bN0=', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [Create_date], [Last_login], [restore_ban_time], [is_active]) VALUES (2, N'admin', N'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', N'Ar/aB2thQTI=', N'admin', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_UserName]    Script Date: 4/10/2023 2:03:34 PM ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [UQ_UserName] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Forum] ADD  CONSTRAINT [DF_Forum_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Forum] ADD  CONSTRAINT [DF_Forum_is_visible]  DEFAULT ((1)) FOR [is_visible]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF_Posts_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF_Posts_thumbs_up]  DEFAULT ((0)) FOR [thumbs_up]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF_Posts_thumbs_down]  DEFAULT ((0)) FOR [thumbs_down]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF_Posts_visible]  DEFAULT ((1)) FOR [visible]
GO
ALTER TABLE [dbo].[Private_Message] ADD  CONSTRAINT [DF_Private_Message_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_Create_date]  DEFAULT (getdate()) FOR [Create_date]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_Last_login]  DEFAULT (getdate()) FOR [Last_login]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_restore_ban_time]  DEFAULT (((1753)-(1))-(1)) FOR [restore_ban_time]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Forum]  WITH CHECK ADD  CONSTRAINT [FK_Forum_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[Forum] CHECK CONSTRAINT [FK_Forum_users]
GO
ALTER TABLE [dbo].[Forum_mods]  WITH CHECK ADD  CONSTRAINT [FK_Forum_mods_Forum1] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forum] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_mods] CHECK CONSTRAINT [FK_Forum_mods_Forum1]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Forum] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forum] ([forum_id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Forum]
GO
ALTER TABLE [dbo].[Private_Message]  WITH CHECK ADD  CONSTRAINT [FK_Private_Message_users] FOREIGN KEY([from_user])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[Private_Message] CHECK CONSTRAINT [FK_Private_Message_users]
GO
ALTER TABLE [dbo].[Private_Message]  WITH CHECK ADD  CONSTRAINT [FK_Private_Message_users1] FOREIGN KEY([to_user])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[Private_Message] CHECK CONSTRAINT [FK_Private_Message_users1]
GO
USE [master]
GO
ALTER DATABASE [final_capstone] SET  READ_WRITE 
GO
