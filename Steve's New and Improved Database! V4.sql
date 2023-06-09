USE [master]
GO
/****** Object:  Database [final_capstone]    Script Date: 4/16/2023 10:49:44 PM ******/
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
/****** Object:  Table [dbo].[Forum_Favorites]    Script Date: 4/16/2023 10:49:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_Favorites](
	[forum_id] [bigint] NOT NULL,
	[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forum_Mods]    Script Date: 4/16/2023 10:49:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_Mods](
	[forum_id] [bigint] NOT NULL,
	[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forum_Posts]    Script Date: 4/16/2023 10:49:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum_Posts](
	[header] [nvarchar](50) NOT NULL,
	[parent_post_id] [bigint] NULL,
	[post_content] [nvarchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[is_visible] [bit] NOT NULL,
	[forum_id] [bigint] NOT NULL,
	[post_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[image_url] [nvarchar](255) NULL,
 CONSTRAINT [PK_Forum_Posts_1] PRIMARY KEY CLUSTERED 
(
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forums]    Script Date: 4/16/2023 10:49:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forums](
	[description] [nvarchar](255) NULL,
	[title] [nvarchar](50) NULL,
	[topic] [nvarchar](50) NOT NULL,
	[user_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[is_visible] [bit] NOT NULL,
	[forum_id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Forums] PRIMARY KEY CLUSTERED 
(
	[forum_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post_Upvotes_Downvotes]    Script Date: 4/16/2023 10:49:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post_Upvotes_Downvotes](
	[forum_id] [bigint] NOT NULL,
	[post_id] [bigint] NOT NULL,
	[is_upvoted] [bit] NOT NULL,
	[is_downvoted] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Private_Messages]    Script Date: 4/16/2023 10:49:44 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 4/16/2023 10:49:44 PM ******/
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
INSERT [dbo].[Forum_Favorites] ([forum_id], [user_id]) VALUES (1, 1)
GO
INSERT [dbo].[Forum_Mods] ([forum_id], [user_id]) VALUES (1, 14)
GO
SET IDENTITY_INSERT [dbo].[Forum_Posts] ON 

INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'New to Skydiving', NULL, N'Hi everyone, I am new to skydiving and I am looking for some tips for beginners. Any advice would be appreciated!', CAST(N'2023-04-15T12:30:00.000' AS DateTime), 0, 1, 1, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Welcome to skydiving! I would recommend starting with a tandem jump to get a feel for it.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 2, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Make sure to take a beginner course and learn about safety procedures.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 3, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Skydiving is a thrilling experience! Just relax and enjoy the ride.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 4, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Get a good quality helmet and goggles to protect yourself during the jump.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 5, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Find a reputable skydiving center with experienced instructors.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 6, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'I found watching videos of experienced skydivers helped me learn some techniques.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 7, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Don''t forget to breathe and enjoy the view!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 8, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Always double-check your gear and ask questions if you are unsure about anything.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 9, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Remember to arch your back and keep your legs bent while in freefall.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 10, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 1, N'Have fun and always respect your fellow skydivers.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 11, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 8, N'I agree, tandem jumps are a great way to start. You will have an experienced instructor with you.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 12, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: New to Skydiving', 8, N'Tandem jumps also allow you to focus on the experience without worrying about controlling the parachute.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 13, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Favorite Skydiving Locations', NULL, N'Hey fellow skydivers, where are some of your favorite places to skydive? Looking for some new places to add to my list!', CAST(N'2023-04-15T12:40:00.000' AS DateTime), 1, 1, 14, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'I absolutely love skydiving in Dubai. The view of the city and the Palm Islands is incredible!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 15, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Switzerland has some breathtaking views, especially when jumping over the Alps.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 16, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Skydiving in New Zealand offers stunning landscapes and a unique experience.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 17, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Hawaii is an amazing place to skydive, with its beautiful beaches and volcanic landscapes.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 18, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'I had a fantastic time skydiving in Costa Rica. The view of the rainforest was incredible!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 19, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Australia offers amazing skydiving locations, like the Great Barrier Reef or the beautiful beaches of Sydney.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 20, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Skydiving in South Africa gives you a chance to see incredible wildlife from a unique perspective.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 21, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'The Grand Canyon in the USA is a breathtaking location for a skydive.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 22, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Norway has some fantastic locations, especially with the fjords and the Northern Lights!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 23, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Favorite Skydiving Locations', 14, N'Skydiving over the Great Blue Hole in Belize is an unforgettable experience.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 24, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Wingsuit Flying Experiences', NULL, N'Has anyone here tried wingsuit flying? I am considering giving it a go and would love to hear your experiences!', CAST(N'2023-04-15T12:50:00.000' AS DateTime), 1, 1, 25, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'Wingsuit flying is an incredible experience! Just make sure you have plenty of skydiving experience before attempting it.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 26, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'I tried wingsuit flying last year, and it was amazing. The feeling of flying through the air is unmatched.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 27, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'Make sure to train with an experienced wingsuit flyer before attempting it on your own.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 28, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'Wingsuit flying requires a high level of skill and experience. Make sure you have completed a significant number of skydives before trying it.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 29, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'It''s an incredible adrenaline rush but also very dangerous. Always prioritize safety.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 30, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'I found it to be a completely different experience compared to regular skydiving. The feeling of gliding through the air is exhilarating!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 31, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'I recommend watching videos and reading up on wingsuit flying techniques before attempting it.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 32, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'It''s important to choose the right wingsuit for your skill level and body type.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 33, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'I''ve been wingsuit flying for a few years now, and it''s by far my favorite way to experience the sky. Just be prepared for a steep learning curve.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 34, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Experiences', 25, N'Remember to always fly within your limits and never push yourself too hard. Safety should be your top priority.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 35, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Camera Recommendations for Skydiving', NULL, N'I want to record my skydives. Any recommendations for cameras that work well for skydiving?', CAST(N'2023-04-15T13:00:00.000' AS DateTime), 1, 1, 36, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'GoPro cameras are popular among skydivers due to their durability and ease of use.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 37, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'I use a Sony action camera, and the quality is great. Just make sure you have a secure mounting system.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 38, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'I recommend looking into the Insta360 One R. It offers amazing 360-degree footage!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 39, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'DJI Osmo Action is another great option for skydiving. It has excellent image stabilization.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 40, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Whichever camera you choose, make sure it can handle high wind speeds and has a reliable mounting system.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 41, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Consider getting a camera with a wrist remote or voice activation for easy control while skydiving.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 42, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'I use a GoPro Hero 9, and it works great. The built-in stabilization is impressive.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 43, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Make sure to choose a camera that can handle the extreme conditions of skydiving, like high altitude and low temperatures.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 44, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'If you''re looking for a budget-friendly option, the Akaso Brave 7 LE is a decent choice.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 45, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Remember to get extra batteries and memory cards, so you don''t run out of power or storage during your skydives.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 46, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Check with your dropzone if they have any specific camera requirements or restrictions before purchasing one.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 47, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Invest in a good quality protective case to keep your camera safe during your jumps.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 48, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'Experiment with different camera angles and mounting positions to find the best perspective for your skydiving videos.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 49, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Camera Recommendations for Skydiving', 36, N'It''s a good idea to use a tether or safety line to secure your camera in case the mount fails during the jump.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 50, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'First Time Skydiving Tips', NULL, N'What advice would you give someone going skydiving for the first time? I am super excited but also a bit nervous.', CAST(N'2023-04-15T13:10:00.000' AS DateTime), 1, 1, 51, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Try to relax and enjoy the experience. The instructors are highly trained and will ensure your safety.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 52, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Listen carefully to your instructor and follow their guidance. They will help you have a safe and enjoyable jump.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 53, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Remember to breathe! It''s common for first-timers to hold their breath, but it''s important to breathe normally during the jump.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 54, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Wear comfortable, weather-appropriate clothing and closed-toe shoes. The temperature can be much colder at altitude.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 55, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Don''t be afraid to ask questions or express your concerns to your instructor. They are there to help you.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 56, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Consider booking an early morning jump to avoid potential weather-related delays later in the day.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 57, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Trust your instructor and the equipment. Skydiving is a very safe sport, and accidents are extremely rare.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 58, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Capture your experience with a video or photos. Most dropzones offer this service, and it''s a great way to remember your first jump.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 59, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Try not to overthink it. The anticipation can be scarier than the actual jump.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 60, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Skydiving Tips', 51, N'Remember that it''s normal to feel nervous. Embrace the excitement and enjoy the once-in-a-lifetime experience!', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 61, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Best Skydiving Locations', NULL, N'What are some of the most scenic and exciting places to skydive around the world?', CAST(N'2023-04-15T13:20:00.000' AS DateTime), 1, 1, 62, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Skydiving over the Great Barrier Reef in Australia is an unforgettable experience with stunning views of the ocean and coral reefs.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 63, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Dubai offers incredible skydiving experiences with views of the city, the desert, and the Palm Jumeirah.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 64, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Interlaken, Switzerland offers breathtaking views of the Swiss Alps, lakes, and valleys.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 65, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Jumping over Queenstown, New Zealand, provides amazing views of the mountains, lakes, and fjords.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 66, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Hawaii is a fantastic skydiving destination, with incredible views of the Pacific Ocean, waterfalls, and lush green mountains.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 67, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Skydiving over Victoria Falls in Zambia and Zimbabwe offers a unique view of one of the world''s most spectacular waterfalls.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 68, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Skydiving in Moab, Utah, provides stunning views of the red rock formations and desert landscape.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 69, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Jumping over the Namib Desert in Namibia offers incredible views of the vast desert landscape and massive sand dunes.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 70, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Fox Glacier, New Zealand, is another fantastic location with views of glaciers, mountains, rainforests, and the ocean.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 71, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Best Skydiving Locations', 62, N'Skydiving in Costa Brava, Spain, offers stunning views of the Mediterranean Sea, picturesque coastal towns, and beautiful landscapes.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 72, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Canopy Control Tips', NULL, N'I am looking for tips to improve my canopy control skills. Any suggestions?', CAST(N'2023-04-15T13:30:00.000' AS DateTime), 0, 1, 73, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Practice makes perfect. Spend as much time as possible under canopy to improve your skills.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 74, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Take a canopy control course to learn advanced techniques and receive personalized coaching.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 75, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Learn to use your harness effectively. It will give you more control over your canopy.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 76, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Understand how different wind conditions affect your canopy and how to adjust your flight accordingly.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 77, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Practice high-performance landings in a safe environment, such as a swoop pond.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 78, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Learn from experienced skydivers and watch videos of their canopy flights to gain insights.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 79, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Always have a plan for your canopy flight, including your landing pattern and potential outs.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 80, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Stay aware of other skydivers under canopy, and communicate your intentions through hand signals or radio.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 81, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Canopy Control Tips', 73, N'Practice different canopy maneuvers, such as turns, stalls, and flares, to build muscle memory and improve your control.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 82, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Wingsuit Flying Advice', NULL, N'I am interested in getting into wingsuit flying. Any advice for someone starting out?', CAST(N'2023-04-15T13:40:00.000' AS DateTime), 1, 1, 83, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Gain plenty of skydiving experience before attempting wingsuit flying. Most dropzones require a minimum of 200 jumps.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 84, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Take a wingsuit training course from a certified instructor to learn proper techniques and safety procedures.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 85, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Start with a smaller, more forgiving wingsuit and gradually work your way up to more advanced suits.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 86, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Learn to fly with a group to develop skills such as formations, proximity flying, and safety.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 87, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Always have an exit plan and be prepared for emergencies, such as malfunctions and off-landing situations.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 88, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Invest in quality gear and maintain it properly to ensure safety and optimal performance.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 89, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Practice tracking jumps to improve your body position, altitude awareness, and navigation skills before transitioning to wingsuit flying.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 90, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Stay current with skydiving and wingsuit flying by participating in events, workshops, and online communities.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 91, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Wingsuit Flying Advice', 83, N'Be patient and progress at your own pace. Wingsuit flying is a high-risk sport, and it is crucial to build a solid foundation of skills and experience.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 92, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Skydiving Insurance', NULL, N'Do you have any recommendations for skydiving insurance providers or tips on what to look for in a policy?', CAST(N'2023-04-15T13:50:00.000' AS DateTime), 1, 1, 93, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'USPA membership includes third-party liability insurance, which is a good start for skydivers in the United States.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 94, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Check with your dropzone for any specific insurance requirements they may have.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 95, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Consider adding supplemental insurance to cover medical expenses, disability, and life insurance in case of an accident.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 96, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Research different providers and compare their coverage, premiums, and exclusions before making a decision.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 97, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Ensure that the insurance policy covers all aspects of skydiving, such as training, competitions, and other related activities.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 98, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Some travel insurance policies include coverage for skydiving, so check if your existing policy offers that option.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 99, 8, NULL)
GO
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Make sure to read the fine print and understand any limitations or exclusions in your policy.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 100, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Consult with other skydivers or skydiving forums for recommendations and reviews of insurance providers.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 101, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: Skydiving Insurance', 93, N'Remember that insurance is meant to provide peace of mind, so choose a policy and provider that best suits your needs and offers adequate coverage.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 102, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'First Time Tandem Experience', NULL, N'I just booked my first tandem skydive! Any advice for a first-timer?', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 103, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Wear comfortable, weather-appropriate clothing and athletic shoes. Avoid loose or baggy clothes.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 104, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Listen carefully to the instructions given by your tandem instructor and ask questions if you are unsure about anything.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 105, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Try to relax and enjoy the experience. Remember that tandem instructors are highly experienced professionals.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 0, 1, 106, 1, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Consider purchasing a video or photo package to capture your first skydive experience.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 107, 2, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Make sure to eat a light meal before your jump to maintain your energy levels without feeling too full.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 108, 8, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Arrive early at the dropzone to check-in, complete paperwork, and attend the safety briefing without feeling rushed.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 109, 9, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Bring a support person, such as a friend or family member, to share the experience and help ease any nerves.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 110, 10, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'Reply: First Time Tandem Experience', 103, N'Remember that skydiving is weather-dependent, so be prepared for the possibility of delays or rescheduling.', CAST(N'2023-04-15T14:00:00.000' AS DateTime), 1, 1, 111, 11, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'asdasd', NULL, N'asdad', CAST(N'1904-10-18T00:00:00.000' AS DateTime), 0, 2, 112, 13, NULL)
INSERT [dbo].[Forum_Posts] ([header], [parent_post_id], [post_content], [create_date], [is_visible], [forum_id], [post_id], [user_id], [image_url]) VALUES (N'string', 1, N'string', CAST(N'2023-04-16T16:17:18.747' AS DateTime), 0, 1, 113, 12, N'string')
SET IDENTITY_INSERT [dbo].[Forum_Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Forums] ON 

INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Join our skydiving community and discuss tips and tricks, share experiences, and discover new locations to jump!', N'Skydiving Enthusiasts', N'Skydiving', 1, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 1)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Share your programming experiences, ask questions, and collaborate on projects.', N'Programming Hub', N'Programming', 2, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 2)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Welcome to the bookworms forum, where you can discuss your favorite books, authors, and genres.', N'Bookworms United', N'Literature', 8, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 3)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Discuss the latest movies and TV shows, share recommendations, and debate on popular series.', N'Movie and TV Buffs', N'Movies and TV', 9, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 4)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'A place to share your travel experiences, ask questions, and plan your next adventure.', N'World Travelers', N'Travel', 10, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 5)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Exchange ideas, discuss techniques, and showcase your latest artworks.', N'Artists'' Corner', N'Art', 11, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 6)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Discuss the latest video games, consoles, and industry news.', N'Gamers'' Den', N'Gaming', 1, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 7)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Talk about your favorite music, artists, and genres, and discover new tunes.', N'Music Lovers', N'Music', 2, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 8)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Exchange healthy recipes, workout routines, and tips to maintain a healthy lifestyle.', N'Health and Fitness', N'Health', 8, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 9)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'Discuss the latest fashion trends, share styling tips, and find inspiration for your wardrobe.', N'Fashion Forward', N'Fashion', 9, CAST(N'2023-04-15T15:43:41.943' AS DateTime), 1, 10)
INSERT [dbo].[Forums] ([description], [title], [topic], [user_id], [create_date], [is_visible], [forum_id]) VALUES (N'asdasd', N'asdasd', N'asdasd', 13, CAST(N'1904-10-18T00:00:00.000' AS DateTime), 0, 11)
SET IDENTITY_INSERT [dbo].[Forums] OFF
GO
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [is_upvoted], [is_downvoted], [create_date], [user_id]) VALUES (1, 1, 1, 0, CAST(N'2023-04-15T18:16:29.103' AS DateTime), 1)
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [is_upvoted], [is_downvoted], [create_date], [user_id]) VALUES (1, 1, 1, 0, CAST(N'2023-04-15T18:16:29.103' AS DateTime), 2)
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [is_upvoted], [is_downvoted], [create_date], [user_id]) VALUES (1, 1, 1, 0, CAST(N'2023-04-15T18:16:29.103' AS DateTime), 8)
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [is_upvoted], [is_downvoted], [create_date], [user_id]) VALUES (1, 1, 1, 0, CAST(N'2023-04-15T18:16:29.103' AS DateTime), 9)
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [is_upvoted], [is_downvoted], [create_date], [user_id]) VALUES (1, 1, 0, 1, CAST(N'2023-04-15T18:16:29.103' AS DateTime), 10)
INSERT [dbo].[Post_Upvotes_Downvotes] ([forum_id], [post_id], [is_upvoted], [is_downvoted], [create_date], [user_id]) VALUES (1, 1, 0, 1, CAST(N'2023-04-15T18:16:29.107' AS DateTime), 11)
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

INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (1, N'user', N'Jg45HuwT7PZkfuKTz6IB90CtWY4=', N'LHxP4Xh7bN0=', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2012-04-23T18:25:43.510' AS DateTime), 0)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (2, N'admin', N'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', N'Ar/aB2thQTI=', N'admin', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), NULL, 0)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (8, N'user2', N'N/A', N'N/A', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'9999-04-23T18:25:43.510' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (9, N'user3', N'N/A', N'N/A', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (10, N'user4', N'N/A', N'N/A', N'user', CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'2023-04-10T11:32:17.230' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (11, N'user5', N'l5cRpggjGzPlIFt+BJjxCXOT5nY=', N'qZVrJlMgXJM=', N'member', CAST(N'2023-04-12T19:35:55.080' AS DateTime), CAST(N'2023-04-12T19:35:55.080' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (12, N'AdminTester', N'fVfZ/1+MARAGHC3LpAv1gXlMmmo=', N'Zf1X9PH5ExE=', N'admin', CAST(N'2023-04-16T11:17:15.087' AS DateTime), CAST(N'2023-04-16T11:17:15.087' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (13, N'UserAB', N'WnF9npzKWosCbGqurgmxpgY87pY=', N'0CbIACiTF1A=', N'member', CAST(N'2023-04-16T12:10:36.250' AS DateTime), CAST(N'2023-04-16T12:10:36.250' AS DateTime), CAST(N'9999-04-23T18:25:43.510' AS DateTime), 1)
INSERT [dbo].[Users] ([user_id], [username], [password_hash], [salt], [user_role], [create_date], [last_login], [restore_ban_time], [is_active]) VALUES (14, N'User6', N'ckrrYLNNisoQl9aGRVismTru/EM=', N'IvvHXaGxmTo=', N'admin', CAST(N'2023-04-16T17:40:19.613' AS DateTime), CAST(N'2023-04-16T17:40:19.613' AS DateTime), CAST(N'1904-10-18T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_UserName]    Script Date: 4/16/2023 10:49:44 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_UserName] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Forum_Posts] ADD  CONSTRAINT [DF_Forum_Posts_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Forums] ADD  CONSTRAINT [DF_Forum_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[Forums] ADD  CONSTRAINT [DF_Forum_is_visible]  DEFAULT ((1)) FOR [is_visible]
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
ALTER TABLE [dbo].[Forum_Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Favorites_Forums] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forums] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_Favorites] CHECK CONSTRAINT [FK_Forum_Favorites_Forums]
GO
ALTER TABLE [dbo].[Forum_Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Favorites_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Forum_Favorites] CHECK CONSTRAINT [FK_Forum_Favorites_Users]
GO
ALTER TABLE [dbo].[Forum_Mods]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Mods_Forums] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forums] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_Mods] CHECK CONSTRAINT [FK_Forum_Mods_Forums]
GO
ALTER TABLE [dbo].[Forum_Mods]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Mods_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Forum_Mods] CHECK CONSTRAINT [FK_Forum_Mods_Users]
GO
ALTER TABLE [dbo].[Forum_Posts]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Posts_Forums] FOREIGN KEY([forum_id])
REFERENCES [dbo].[Forums] ([forum_id])
GO
ALTER TABLE [dbo].[Forum_Posts] CHECK CONSTRAINT [FK_Forum_Posts_Forums]
GO
ALTER TABLE [dbo].[Forum_Posts]  WITH CHECK ADD  CONSTRAINT [FK_Forum_Posts_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Forum_Posts] CHECK CONSTRAINT [FK_Forum_Posts_Users]
GO
ALTER TABLE [dbo].[Forums]  WITH CHECK ADD  CONSTRAINT [FK_Forum_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Forums] CHECK CONSTRAINT [FK_Forum_users]
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes]  WITH CHECK ADD  CONSTRAINT [FK_Post_Upvotes_Downvotes_Users] FOREIGN KEY([post_id])
REFERENCES [dbo].[Forum_Posts] ([post_id])
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes] CHECK CONSTRAINT [FK_Post_Upvotes_Downvotes_Users]
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes]  WITH CHECK ADD  CONSTRAINT [FK_Post_Upvotes_Downvotes_Users1] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Post_Upvotes_Downvotes] CHECK CONSTRAINT [FK_Post_Upvotes_Downvotes_Users1]
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
