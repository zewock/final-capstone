USE [master]
GO
/****** Object:  Database [final_capstone]    Script Date: 4/14/2023 10:28:26 AM ******/
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
/****** Object:  Table [dbo].[Forum_Favorites]    Script Date: 4/14/2023 10:28:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_Favorites](
	[user_id] [bigint] NOT NULL,
	[forum_id] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forum_Mods]    Script Date: 4/14/2023 10:28:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_Mods](
	[user_id] [int] NOT NULL,
	[forum_id] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forum_Posts]    Script Date: 4/14/2023 10:28:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_Posts](
	[header] [nvarchar](50) NOT NULL,
	[parent_post_id] [bigint] NULL,
	[posts_id] [bigint] IDENTITY(1,1) NOT NULL,
	[post_content] [nvarchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[is_visible] [bit] NOT NULL,
	[forum_id] [bigint] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[posts_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forums]    Script Date: 4/14/2023 10:28:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forums](
	[description] [nvarchar](255) NULL,
	[title] [nvarchar](50) NULL,
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
/****** Object:  Table [dbo].[Post_Upvotes_Downvotes]    Script Date: 4/14/2023 10:28:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post_Upvotes_Downvotes](
	[forum_id] [bigint] NOT NULL,
	[post_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[is_upvoted] [bit] NOT NULL,
	[is_downvoted] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Private_Messages]    Script Date: 4/14/2023 10:28:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Private_Messages](
	[message_id] [int] IDENTITY(1,1) NOT NULL,
	[from_user] [int] NOT NULL,
	[to_user] [int] NOT NULL,
	[message] [nvarchar](max) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[Is_visable] [bit] NOT NULL,
 CONSTRAINT [PK_Private_Message] PRIMARY KEY CLUSTERED 
(
	[message_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/14/2023 10:28:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
	[user_role] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[last_login] [datetime] NOT NULL,
	[restore_ban_time] [datetime] NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Forum_Favorites] ([user_id], [forum_id]) VALUES (1, 1)
INSERT [dbo].[Forum_Favorites] ([user_id], [forum_id]) VALUES (2, 1)
INSERT [dbo].[Forum_Favorites] ([user_id], [forum_id]) VALUES (1, 4)
INSERT [dbo].[Forum_Favorites] ([user_id], [forum_id]) VALUES (2, 3)
INSERT [dbo].[Forum_Favorites] ([user_id], [forum_id]) VALUES (8, 1)
INSERT [dbo].[Forum_Favorites] ([user_id], [forum_id]) VALUES (11, 1)
GO
INSERT [dbo].[Forum_Mods] ([user_id], [forum_id]) VALUES (1, 2)
INSERT [dbo].[Forum_Mods] ([user_id], [forum_id]) VALUES (2, 1)
INSERT [dbo].[Forum_Mods] ([user_id], [forum_id]) VALUES (1, 3)
INSERT [dbo].[Forum_Mods] ([user_id], [forum_id]) VALUES (8, 4)
INSERT [dbo].[Forum_Mods] ([user_id], [forum_id]) VALUES (10, 4)
INSERT [dbo].[Forum_Mods] ([user_id], [forum_id]) VALUES (11, 1)
GO
SET IDENTITY_INSERT [dbo].[Forum_Posts] ON 

INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [posts_id], [post_content], [create_date], [user_id], [is_visible], [forum_id]) VALUES (N'Tomato smashing is the best!', NULL, 4, N'Yabba Dabba Do', CAST(N'2023-04-14T08:52:45.057' AS DateTime), 1, 1, 1)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [posts_id], [post_content], [create_date], [user_id], [is_visible], [forum_id]) VALUES (N'Magnet Fishing Badassery', NULL, 5, N'12345 ', CAST(N'2023-04-14T08:53:36.947' AS DateTime), 2, 1, 1)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [posts_id], [post_content], [create_date], [user_id], [is_visible], [forum_id]) VALUES (N'I love Hippos', NULL, 6, N'blargggggg', CAST(N'2023-04-14T08:55:00.700' AS DateTime), 3, 1, 1)
SET IDENTITY_INSERT [dbo].[Forum_Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Forums] ON 

INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 1, N'Aligators', 2, CAST(N'2023-04-11T15:46:56.723' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 2, N'turtles', 1, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 3, N'bluejays', 1, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 4, N'jumanji', 2, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 5, N'surfing', 1, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 6, N'constipation', 2, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 7, N'george', 2, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 8, N'cellphones', 1, CAST(N'2023-04-12T11:19:15.400' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 9, N'turtles', 1, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 10, N'bluejays', 1, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 11, N'jumanji', 2, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 12, N'surfing', 1, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 13, N'constipation', 2, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 14, N'george', 2, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
INSERT [dbo].[Forums] ([description], [title], [forum_id], [topic], [user_id], [create_date], [is_visible]) VALUES (NULL, NULL, 15, N'cellphones', 1, CAST(N'2023-04-12T11:19:17.027' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Forums] OFF
GO
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [user_id], [is_upvoted], [is_downvoted], [create_date]) VALUES (1, 5, 1, 1, 0, CAST(N'2023-04-10T11:32:20.230' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Private_Messages] ON 

INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (1, 1, 2, N'Yo', CAST(N'2023-04-10T11:32:17.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (2, 2, 1, N'Hello', CAST(N'2023-04-10T11:32:18.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (3, 1, 2, N'You Suck', CAST(N'2023-04-10T11:32:19.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (4, 2, 1, N'My Dad Can Beat Up Your Dad', CAST(N'2023-04-10T11:32:20.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (5, 8, 9, N'Howdy', CAST(N'2023-04-10T11:32:21.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (6, 8, 9, N'Are You There', CAST(N'2023-04-10T11:32:22.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (7, 9, 8, N'You Should Not Be Able to See This', CAST(N'2023-04-10T11:32:23.230' AS DateTime), 0)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (8, 9, 8, N'Dab', CAST(N'2023-04-10T11:32:24.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (9, 11, 2, N'Yo', CAST(N'2023-04-10T11:32:24.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (10, 2, 11, N'YoYo', CAST(N'2023-04-10T11:32:24.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (11, 11, 1, N'Grr', CAST(N'2023-04-10T11:32:24.230' AS DateTime), 1)
INSERT [dbo].[Private_Messages] ([message_id], [from_user], [to_user], [message], [create_date], [Is_visable]) VALUES (12, 1, 11, N'GRRRRR', CAST(N'2023-04-10T11:32:24.230' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Private_Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (1, N'user', N'Jg45HuwT7PZkfuKTz6IB90CtWY4=', N'LHxP4Xh7bN0=', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), NULL, 0)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (2, N'admin', N'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', N'Ar/aB2thQTI=', N'admin', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), NULL, 0)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (8, N'user2', N'N/A', N'N/A', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (9, N'user3', N'N/A', N'N/A', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (10, N'user4', N'N/A', N'N/A', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (11, N'user5', N'l5cRpggjGzPlIFt+BJjxCXOT5nY=', N'qZVrJlMgXJM=', N'user', CAST(N'2023-04-12T19:35:55.080' AS DateTime), CAST(N'2023-04-12T19:35:55.080' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_UserName]    Script Date: 4/14/2023 10:28:27 AM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_UserName] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Forum_Posts] ADD  CONSTRAINT [DF_Posts_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Forum_Posts] ADD  CONSTRAINT [DF_Posts_visible]  DEFAULT ((1)) FOR [is_visible]
GO
ALTER TABLE [dbo].[Forums] ADD  CONSTRAINT [DF_Forum_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Forums] ADD  CONSTRAINT [DF_Forum_is_visible]  DEFAULT ((1)) FOR [is_visible]
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes] ADD  CONSTRAINT [DF_Post_Upvotes_Downvotes_isUpvoted]  DEFAULT ((0)) FOR [is_upvoted]
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes] ADD  CONSTRAINT [DF_Post_Upvotes_Downvotes_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Private_Messages] ADD  CONSTRAINT [DF_Private_Message_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Private_Messages] ADD  CONSTRAINT [DF_Private_Message_IsVisable]  DEFAULT ((1)) FOR [Is_visable]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_users_Create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_users_Last_login]  DEFAULT (getdate()) FOR [last_login]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_users_restore_ban_time]  DEFAULT (((1753)-(1))-(1)) FOR [restore_ban_time]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_users_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Forum_Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Favorites2_Forum] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forums] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_Favorites] CHECK CONSTRAINT [FK_Forum_Favorites2_Forum]
GO
ALTER TABLE [dbo].[Forum_Mods]  WITH CHECK ADD  CONSTRAINT [FK_Forum_mods_Forum1] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forums] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_Mods] CHECK CONSTRAINT [FK_Forum_mods_Forum1]
GO
ALTER TABLE [dbo].[Forum_Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Forum] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forums] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_Posts] CHECK CONSTRAINT [FK_Posts_Forum]
GO
ALTER TABLE [dbo].[Forums]  WITH CHECK ADD  CONSTRAINT [FK_Forum_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Forums] CHECK CONSTRAINT [FK_Forum_users]
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes]  WITH CHECK ADD  CONSTRAINT [FK_Post_Upvotes_Downvotes_Posts] FOREIGN KEY([post_id])
REFERENCES [dbo].[Forum_Posts] ([posts_id])
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes] CHECK CONSTRAINT [FK_Post_Upvotes_Downvotes_Posts]
GO
ALTER TABLE [dbo].[Private_Messages]  WITH CHECK ADD  CONSTRAINT [FK_Private_Message_users] FOREIGN KEY([from_user])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Private_Messages] CHECK CONSTRAINT [FK_Private_Message_users]
GO
ALTER TABLE [dbo].[Private_Messages]  WITH CHECK ADD  CONSTRAINT [FK_Private_Message_users1] FOREIGN KEY([to_user])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Private_Messages] CHECK CONSTRAINT [FK_Private_Message_users1]
GO
USE [master]
GO
ALTER DATABASE [final_capstone] SET  READ_WRITE 
GO
