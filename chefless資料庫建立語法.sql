USE [master]
GO
/****** Object:  Database [chefless]    Script Date: 2024/2/3 下午 04:43:44 ******/
CREATE DATABASE [chefless]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'chefless', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\chefless.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'chefless_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\chefless_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [chefless] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [chefless].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [chefless] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [chefless] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [chefless] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [chefless] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [chefless] SET ARITHABORT OFF 
GO
ALTER DATABASE [chefless] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [chefless] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [chefless] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [chefless] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [chefless] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [chefless] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [chefless] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [chefless] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [chefless] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [chefless] SET  DISABLE_BROKER 
GO
ALTER DATABASE [chefless] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [chefless] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [chefless] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [chefless] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [chefless] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [chefless] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [chefless] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [chefless] SET RECOVERY FULL 
GO
ALTER DATABASE [chefless] SET  MULTI_USER 
GO
ALTER DATABASE [chefless] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [chefless] SET DB_CHAINING OFF 
GO
ALTER DATABASE [chefless] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [chefless] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [chefless] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [chefless] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'chefless', N'ON'
GO
ALTER DATABASE [chefless] SET QUERY_STORE = ON
GO
ALTER DATABASE [chefless] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [chefless]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 2024/2/3 下午 04:43:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cartid] [int] IDENTITY(1,1) NOT NULL,
	[tobuyid] [int] NOT NULL,
	[tobutcount] [int] NOT NULL,
	[userid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[cartid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clerk]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clerk](
	[cid] [int] IDENTITY(1000,1) NOT NULL,
	[staffid] [int] NOT NULL,
	[checkin] [datetime] NOT NULL,
	[checkout] [datetime] NOT NULL,
	[權限] [int] NOT NULL,
 CONSTRAINT [PK__clerk__D837D05F4B7EE62A] PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[expertbuy]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[expertbuy](
	[ebid] [int] IDENTITY(1,1) NOT NULL,
	[ebrigion] [nvarchar](50) NOT NULL,
	[ebseniority] [int] NOT NULL,
	[ebserviceway] [nvarchar](50) NOT NULL,
	[ebname] [nvarchar](50) NOT NULL,
	[ebwant] [nvarchar](100) NOT NULL,
	[ebbudget] [int] NOT NULL,
	[ebcontact] [nvarchar](100) NOT NULL,
	[ebdesc] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ebid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[expertsale]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[expertsale](
	[esid] [int] IDENTITY(1,1) NOT NULL,
	[esrigion] [nvarchar](50) NOT NULL,
	[esseniority] [int] NOT NULL,
	[esserviceway] [nvarchar](50) NOT NULL,
	[esname] [nvarchar](50) NOT NULL,
	[esskill] [nvarchar](100) NOT NULL,
	[escontact] [nvarchar](100) NOT NULL,
	[esdesc] [nvarchar](500) NOT NULL,
	[esimage] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[esid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[oid] [int] IDENTITY(100,1) NOT NULL,
	[odate] [date] NOT NULL,
	[ototal] [int] NOT NULL,
	[ocustomerid] [int] NOT NULL,
	[oproductid] [int] NOT NULL,
	[ostatus] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[oid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[persons]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[persons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[姓名] [nvarchar](50) NOT NULL,
	[權限] [int] NOT NULL,
	[密碼] [nvarchar](50) NOT NULL,
	[手機號碼] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[pid] [int] IDENTITY(10,1) NOT NULL,
	[pname] [nvarchar](50) NOT NULL,
	[pmode] [nvarchar](50) NOT NULL,
	[price] [int] NOT NULL,
	[pdesc] [nvarchar](500) NOT NULL,
	[pimage] [nvarchar](100) NULL,
	[ptype] [nvarchar](50) NULL,
 CONSTRAINT [PK__products__DD37D91AA19C5817] PRIMARY KEY CLUSTERED 
(
	[pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shandbuy]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shandbuy](
	[sbid] [int] IDENTITY(1,1) NOT NULL,
	[sbrigion] [nvarchar](50) NOT NULL,
	[sbnew] [nvarchar](50) NOT NULL,
	[sbtradeway] [nvarchar](50) NOT NULL,
	[sbnego] [nvarchar](50) NOT NULL,
	[sbname] [nvarchar](50) NOT NULL,
	[sbpname] [nvarchar](50) NOT NULL,
	[sbprice] [int] NOT NULL,
	[sbcontact] [nvarchar](100) NOT NULL,
	[sbdesc] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sbid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shandsale]    Script Date: 2024/2/3 下午 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shandsale](
	[ssid] [int] IDENTITY(1,1) NOT NULL,
	[ssrigion] [nvarchar](50) NOT NULL,
	[ssnew] [nvarchar](50) NOT NULL,
	[sstradeway] [nvarchar](50) NOT NULL,
	[snego] [nvarchar](50) NOT NULL,
	[ssname] [nvarchar](50) NOT NULL,
	[sspname] [nvarchar](50) NOT NULL,
	[ssprice] [int] NOT NULL,
	[sscontact] [nvarchar](100) NOT NULL,
	[ssdesc] [nvarchar](500) NOT NULL,
	[sspimage] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ssid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[persons] ON 

INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (1, N'1111@gmail.com', N'王洪文', 1000, N'1111', N'1111')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (2, N'2222@gmail.com', N'陳達祥', 1000, N'2222', N'2222')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (3, N'3333@gmail.com', N'陳昱瑋', 1000, N'3333', N'3333')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (4, N'4444@gmail.com', N'許正昀', 100, N'4444', N'4444')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (5, N'5555@gmail.com', N'陳韻竹', 100, N'5555', N'5555')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (6, N'6666@gmail.com', N'連宇喆', 10, N'6666', N'6666')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (8, N'7777@gmail.com', N'阿拉華瓜', 9, N'7777', N'7777')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (9, N'8888@gmail.com', N'王木白凱', 1, N'8888', N'8888')
SET IDENTITY_INSERT [dbo].[persons] OFF
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (11, N'四川藤椒清燉牛肉麵', N'材料品項', 2000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛！', N'四川藤椒清燉牛肉麵.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (16, N'四川藤椒清燉牛肉麵', N'材料品項、製作步驟', 12000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛！', N'四川藤椒清燉牛肉麵.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (17, N'四川藤椒清燉牛肉麵', N'材料品項、製作步驟、配方比例', 50000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛！', N'四川藤椒清燉牛肉麵.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (18, N'夏里亞賓牛排', N'材料品項', 2000, N'夏里亞賓牛排的特徵是使用大量洋蔥作烹調，其肉質極為軟嫩，香氣馥郁。提供純正的牛肉風味。', N'夏里亞賓牛排.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (19, N'夏里亞賓牛排', N'材料品項、製作步驟', 12000, N'夏里亞賓牛排的特徵是使用大量洋蔥作烹調，其肉質極為軟嫩，香氣馥郁。經過精心烹調，搭配獨特的調味，味道鮮美。', N'夏里亞賓牛排.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (20, N'夏里亞賓牛排', N'材料品項、製作步驟、配方比例', 50000, N'夏里亞賓牛排的特徵是使用大量洋蔥作烹調，其肉質極為軟嫩，香氣馥郁。經過精心烹調，搭配獨特的調味，味道鮮美。此食譜還包含製作步驟和配方比例，讓您輕鬆在家中享受美味。', N'夏里亞賓牛排.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (21, N'普羅旺斯馬賽魚湯', N'材料品項', 2000, N'普羅旺斯馬賽魚湯是一道經典的法國湯品，以新鮮的魚肉和香料製成，味道濃郁。提供純正的地中海風味。', N'普羅旺斯馬賽魚湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (22, N'普羅旺斯馬賽魚湯', N'材料品項、製作步驟', 12000, N'普羅旺斯馬賽魚湯是一道經典的法國湯品，以新鮮的魚肉和香料製成，味道濃郁。經過精心烹調，搭配獨特的調味，味道鮮美。', N'普羅旺斯馬賽魚湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (23, N'普羅旺斯馬賽魚湯', N'材料品項、製作步驟、配方比例', 50000, N'普羅旺斯馬賽魚湯是一道經典的法國湯品，以新鮮的魚肉和香料製成，味道濃郁。經過精心烹調，搭配獨特的調味，味道鮮美。此食譜還包含製作步驟和配方比例，讓您輕鬆在家中享受美味。', N'普羅旺斯馬賽魚湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (24, N'托斯卡尼奶油鮭魚', N'材料品項', 2000, N'托斯卡尼奶油鮭魚是一道美味的魚料理，以新鮮的鮭魚為主要材料，並加入了豐富的菠菜和番茄，提供豐富的鮮味。作法包括將鮭魚煎至金黃，煎熟後備用。', N'托斯卡尼奶油鮭魚.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (25, N'托斯卡尼奶油鮭魚', N'材料品項、製作步驟', 12000, N'托斯卡尼奶油鮭魚是一道美味的魚料理，以新鮮的鮭魚為主要材料，並加入了豐富的菠菜和番茄，提供豐富的鮮味。作法包括將鮭魚煎至金黃，煎熟後備用。接著爆香大蒜，加入菠菜炒熟。最後加入鮮奶油、鹽、黑胡椒，煨煮5~10分鐘將菠菜煮軟，然後將菠菜鋪底，放上煎熟的鮭魚，再依照喜好撒上帕馬森起司。', N'托斯卡尼奶油鮭魚.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (26, N'托斯卡尼奶油鮭魚', N'材料品項、製作步驟、配方比例', 50000, N'托斯卡尼奶油鮭魚是一道美味的魚料理，以新鮮的鮭魚為主要材料，並加入了豐富的菠菜和番茄，提供豐富的鮮味。作法包括將鮭魚煎至金黃，煎熟後備用。接著爆香大蒜，加入菠菜炒熟。最後加入鮮奶油、鹽、黑胡椒，煨煮5~10分鐘將菠菜煮軟，然後將菠菜鋪底，放上煎熟的鮭魚，再依照喜好撒上帕馬森起司。此食譜還包含詳細的製作步驟和配方比例。', N'托斯卡尼奶油鮭魚.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (27, N'威靈頓牛排佐巴西蘑菇醬', N'材料品項', 2000, N'威靈頓牛排佐巴西蘑菇醬是一道精緻的牛肉料理，主要材料包括優質的帕馬火腿和菲力牛肉，並以酥皮包覆。', N'威靈頓牛排佐巴西蘑菇醬.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (28, N'威靈頓牛排佐巴西蘑菇醬', N'材料品項、製作步驟', 12000, N'威靈頓牛排佐巴西蘑菇醬是一道精緻的牛肉料理，主要材料包括優質的帕馬火腿和菲力牛肉，並以酥皮包覆。製作步驟包括烹調蘑菇泥和包裹牛肉。', N'威靈頓牛排佐巴西蘑菇醬.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (29, N'威靈頓牛排佐巴西蘑菇醬', N'材料品項、製作步驟、配方比例', 50000, N'威靈頓牛排佐巴西蘑菇醬是一道精緻的牛肉料理，主要材料包括優質的帕馬火腿和菲力牛肉，並以酥皮包覆。製作步驟包括烹調蘑菇泥和包裹牛肉。此食譜還包含詳細的配方比例。', N'威靈頓牛排佐巴西蘑菇醬.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (30, N'揚州炒飯', N'材料品項', 2000, N'揚州炒飯是一道經典的中國炒飯料理，主要材料包括新鮮的鮮蝦、叉燒、干貝絲和青豆。', N'揚州炒飯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (31, N'揚州炒飯', N'材料品項、製作步驟', 12000, N'揚州炒飯是一道經典的中國炒飯料理，主要材料包括新鮮的鮮蝦、叉燒、干貝絲和青豆。製作步驟包括炒飯的技巧和步驟。', N'揚州炒飯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (32, N'揚州炒飯', N'材料品項、製作步驟、配方比例', 50000, N'揚州炒飯是一道經典的中國炒飯料理，主要材料包括新鮮的鮮蝦、叉燒、干貝絲和青豆。製作步驟包括炒飯的技巧和步驟。此食譜還包含詳細的配方比例。', N'揚州炒飯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (33, N'瑪格麗特披薩', N'材料品項', 2000, N'瑪格麗特披薩是一道經典的義大利披薩，主要材料包括新鮮的羅勒葉、番茄片、馬蘇里拉起司、酵母麵團和義式披薩紅醬。', N'瑪格麗特披薩.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (34, N'瑪格麗特披薩', N'材料品項、製作步驟', 12000, N'瑪格麗特披薩是一道經典的義大利披薩，主要材料包括新鮮的羅勒葉、番茄片、馬蘇里拉起司、酵母麵團和義式披薩紅醬。製作步驟包括醃製、烹調和烤製的技巧。', N'瑪格麗特披薩.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (35, N'瑪格麗特披薩', N'材料品項、製作步驟、配方比例', 50000, N'瑪格麗特披薩是一道經典的義大利披薩，主要材料包括新鮮的羅勒葉、番茄片、馬蘇里拉起司、酵母麵團和義式披薩紅醬。製作步驟包括醃製、烹調和烤製的技巧。此食譜還包含詳細的配方比例。', N'瑪格麗特披薩.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (36, N'何首烏骨雞湯', N'材料品項', 2000, N'主要材料包括烏骨雞肉、何首烏、紅棗等。何首烏有補肝腎、益精血、烏鬚髮和強筋健骨等功效。', N'何首烏骨雞湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (37, N'何首烏骨雞湯', N'材料品項、製作步驟', 12000, N'主要材料包括烏骨雞肉、何首烏、紅棗等。何首烏有補肝腎、益精血、烏鬚髮和強筋健骨等功效。製作步驟包括將藥材沖洗、汆燙烏骨雞肉、煮湯並調味。', N'何首烏骨雞湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (38, N'何首烏骨雞湯', N'材料品項、製作步驟、配方比例', 50000, N'主要材料包括烏骨雞肉、何首烏、紅棗等。何首烏有補肝腎、益精血、烏鬚髮和強筋健骨等功效。製作步驟包括將藥材沖洗、汆燙烏骨雞肉、煮湯並調味。配方比例可參考食譜。', N'何首烏骨雞湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (39, N'姬松茸淮山響螺湯', N'材料品項', 2000, N'主要材料包括姬松茸、枸杞、港式蜜棗、乾瑤柱、淮山、響螺片和瘦肉。', N'姬松茸淮山響螺湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (40, N'姬松茸淮山響螺湯', N'材料品項、製作步驟', 12000, N'主要材料包括姬松茸、枸杞、港式蜜棗、乾瑤柱、淮山、響螺片和瘦肉。製作步驟包括將材料清洗、燉煮湯品。', N'姬松茸淮山響螺湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (41, N'姬松茸淮山響螺湯', N'材料品項、製作步驟、配方比例', 50000, N'主要材料包括姬松茸、枸杞、港式蜜棗、乾瑤柱、淮山、響螺片和瘦肉。製作步驟包括將材料清洗、燉煮湯品。配方比例可參考食譜。', N'姬松茸淮山響螺湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (42, N'義式脆皮豬五花卷(porchetta)', N'材料品項', 2000, N'主要材料包括帶皮五花肉、火腿片、香料如鹽、黑胡椒、迷迭香等。', N'義式脆皮豬五花卷(porchetta).png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (43, N'義式脆皮豬五花卷(porchetta)', N'材料品項、製作步驟', 12000, N'主要材料包括帶皮五花肉、火腿片、香料如鹽、黑胡椒、迷迭香等。製作步驟包括材料準備、豬肉包裝香料、烤豬肉的過程等。', N'義式脆皮豬五花卷(porchetta).png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (44, N'義式脆皮豬五花卷(porchetta)', N'材料品項、製作步驟、配方比例', 50000, N'主要材料包括帶皮五花肉、火腿片、香料如鹽、黑胡椒、迷迭香等。製作步驟包括材料準備、豬肉包裝香料、烤豬肉的過程等。配方比例可參考食譜中的數據。', N'義式脆皮豬五花卷(porchetta).png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (45, N'南洋咖哩蟹', N'材料品項', 2000, N'主要材料包括香茅、洋蔥、蒜末、紅椒、芫荽粉等。', N'南洋咖哩蟹.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (46, N'南洋咖哩蟹', N'材料品項、製作步驟', 12000, N'主要材料包括香茅、洋蔥、蒜末、紅椒、芫荽粉等。製作步驟包括食材準備、香料調配、烹煮過程等。', N'南洋咖哩蟹.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (47, N'南洋咖哩蟹', N'材料品項、製作步驟、配方比例', 50000, N'主要材料包括香茅、洋蔥、蒜末、紅椒、芫荽粉等。製作步驟包括食材準備、香料調配、烹煮過程等。詳細的配方比例可參考食譜。', N'南洋咖哩蟹.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (48, N'馬太鞍樹豆燉豬腳', N'材料品項', 2000, N'主要材料包括豬腳和樹豆，豬腳需要切成2CM圈，樹豆需提前泡水約一天備用。', N'馬太鞍樹豆燉豬腳.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (49, N'馬太鞍樹豆燉豬腳', N'材料品項、製作步驟', 12000, N'主要材料包括豬腳和樹豆，豬腳需要切成2CM圈，樹豆需提前泡水約一天備用。製作步驟包括滷汁的製作和豬腳的川燙。', N'馬太鞍樹豆燉豬腳.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (50, N'馬太鞍樹豆燉豬腳', N'材料品項、製作步驟、配方比例', 50000, N'主要材料包括豬腳和樹豆，豬腳需要切成2CM圈，樹豆需提前泡水約一天備用。製作步驟包括滷汁的製作和豬腳的川燙。詳細的配方比例可參考食譜中的滷汁部分。', N'馬太鞍樹豆燉豬腳.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (51, N'四福鮮品佛跳牆', N'材料品項', 2000, N'主要材料包括鮑魚、刺參、干貝、花菇、魚肚、魚唇、冬圭、鳥蛋、蹄筋、海螺頭等多種食材。', N'四福鮮品佛跳牆.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (52, N'四福鮮品佛跳牆', N'材料品項、製作步驟', 12000, N'主要材料包括鮑魚、刺參、干貝、花菇、魚肚、魚唇、冬圭、鳥蛋、蹄筋、海螺頭等多種食材。製作步驟包括練湯、白湯、清湯、金湯和煨湯等多個步驟。', N'四福鮮品佛跳牆.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (53, N'四福鮮品佛跳牆', N'材料品項、製作步驟、配方比例', 50000, N'主要材料包括鮑魚、刺參、干貝、花菇、魚肚、魚唇、冬圭、鳥蛋、蹄筋、海螺頭等多種食材。製作步驟包括練湯、白湯、清湯、金湯和煨湯等多個步驟。詳細的配方比例可參考食譜中的相關部分。', N'四福鮮品佛跳牆.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (54, N'瀑布起司牧羊人派', N'材料品項', 2000, N'即溶土豆泥、牛奶、馬蘇里拉乳酪、肉醬、新鮮歐芹', N'瀑布起司牧羊人派.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (55, N'瀑布起司牧羊人派', N'材料品項、製作步驟', 12000, N'即溶土豆泥、牛奶、馬蘇里拉乳酪、肉醬、新鮮歐芹等材料。製作步驟包括在平底鍋中用中火加熱土豆泥和牛奶，攪拌至順滑，再加入馬蘇里拉乳酪，混合至光滑奶油狀。在烤盤中加入肉醬，均勻塗抹。將土豆和乳酪混合物倒在上面，均勻塗抹，最後撒上剩餘的乳酪。烘烤至金黃色，取出後用新鮮歐芹調味。', N'瀑布起司牧羊人派.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (56, N'瀑布起司牧羊人派', N'材料品項、製作步驟、配方比例', 50000, N'即溶土豆泥、牛奶、馬蘇里拉乳酪、肉醬、新鮮歐芹等材料。製作步驟包括在平底鍋中用中火加熱土豆泥和牛奶，攪拌至順滑，再加入馬蘇里拉乳酪，混合至光滑奶油狀。在烤盤中加入肉醬，均勻塗抹。將土豆和乳酪混合物倒在上面，均勻塗抹，最後撒上剩餘的乳酪。烘烤至金黃色，取出後用新鮮歐芹調味。', N'瀑布起司牧羊人派.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (61, N'四川藤椒清燉牛肉麵', N'500g/份，100份/件', 8000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛！', N'四川藤椒清燉牛肉麵.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (62, N'夏里亞賓牛排', N'200g/份，100份/件', 30000, N'夏里亞賓牛排的特徵是使用大量洋蔥作烹調，其肉質極為軟嫩，香氣馥郁。提供純正的牛肉風味。', N'夏里亞賓牛排.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (63, N'普羅旺斯馬賽魚湯', N'200g/份，100份/件', 20000, N'普羅旺斯馬賽魚湯是一道經典的法國湯品，以新鮮的魚肉和香料製成，味道濃郁。提供純正的地中海風味。', N'普羅旺斯馬賽魚湯.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (64, N'托斯卡尼奶油鮭魚', N'200g/份，100份/件', 5000, N'托斯卡尼奶油鮭魚是一道美味的魚料理，以新鮮的鮭魚為主要材料，並加入了豐富的菠菜和番茄，提供豐富的鮮味。', N'托斯卡尼奶油鮭魚.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (65, N'威靈頓牛排佐巴西蘑菇醬', N'200g/份，100份/件', 40000, N'威靈頓牛排佐巴西蘑菇醬是一道精緻的牛肉料理，主要材料包括優質的帕馬火腿和菲力牛肉，並以酥皮包覆。', N'威靈頓牛排佐巴西蘑菇醬.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (66, N'揚州炒飯', N'300g/份，100份/件', 5000, N'揚州炒飯是一道經典的中國炒飯料理，主要材料包括新鮮的鮮蝦、叉燒、干貝絲和青豆。', N'揚州炒飯.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (67, N'瑪格麗特披薩', N'200g/份，100份/件', 9000, N'瑪格麗特披薩是一道經典的義大利披薩，主要材料包括新鮮的羅勒葉、番茄片、馬蘇里拉起司、酵母麵團和義式披薩紅醬。', N'瑪格麗特披薩.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (68, N'何首烏骨雞湯', N'1200g/份，100份/件', 80000, N'主要材料包括烏骨雞肉、何首烏、紅棗等。何首烏有補肝腎、益精血、烏鬚髮和強筋健骨等功效。', N'何首烏骨雞湯.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (69, N'姬松茸淮山響螺湯', N'1200g/份，100份/件', 50000, N'主要材料包括姬松茸、枸杞、港式蜜棗、乾瑤柱、淮山、響螺片和瘦肉。', N'姬松茸淮山響螺湯.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (70, N'義式脆皮豬五花卷(porchetta)', N'800g/條，10份/件', 12000, N'主要材料包括帶皮五花肉、火腿片、香料如鹽、黑胡椒、迷迭香等。', N'義式脆皮豬五花卷(porchetta).png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (71, N'南洋咖哩蟹', N'200g/份，100份/件', 6000, N'主要材料包括香茅、洋蔥、蒜末、紅椒、芫荽粉等。', N'南洋咖哩蟹.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (72, N'馬太鞍樹豆燉豬腳', N'200g/份，100份/件', 5000, N'主要材料包括豬腳和樹豆，豬腳需要切成2CM圈，樹豆需提前泡水約一天備用。', N'馬太鞍樹豆燉豬腳.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (73, N'四福鮮品佛跳牆', N'1200g/份，100份/件', 88000, N'主要材料包括鮑魚、刺參、干貝、花菇、魚肚、魚唇、冬圭、鳥蛋、蹄筋、海螺頭等多種食材。', N'四福鮮品佛跳牆.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (74, N'瀑布起司牧羊人派', N'500g/份，100份/件', 25000, N'即溶土豆泥、牛奶、馬蘇里拉乳酪、肉醬、新鮮歐芹等材料。', N'瀑布起司牧羊人派.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (77, N'品質改進諮詢(線上)', N'以小時計費', 500, N'多平台線上服務，Discord、Zoom、Webex、Line、Skype', N'品質改進諮詢(線上).png', N'廚藝顧問出租')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (82, N'品質改進諮詢(現場)', N'以小時計費', 1000, N'輔導員會先瞭解作業流程，找出錯誤並指導正確做法', N'品質改進諮詢(現場).png', N'廚藝顧問出租')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (83, N'食譜標準化代書(線上)', N'以件計費', 500, N'將您提供的食譜數據，整理成有食材品項、重量配比、標準作業流程、食材採購廠商整合成excel表格，並加入函式，只要輸入需要的份數，即可轉換成對應的重量', N'食譜標準化代書(線上).png', N'廚藝顧問出租')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (84, N'食譜標準化代書 (現場DEMO)', N'以件計費', 2000, N'將您提供的食譜數據，整理成有食材品項、重量配比、標準作業流程、食材採購廠商整合成excel表格，並加入函式，只要輸入需要的份數，即可轉換成對應的重量，此外將會派輔導員在現場實作教學，材料需自備', N'食譜標準化代書(現場DEMO).png', N'廚藝顧問出租')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (85, N'原料廠商代尋(僅介紹)', N'以案計費', 1000, N'各式肉商、進口歐陸食材商、本地蔬果商、南北雜貨商', N'原料廠商代尋(僅介紹).png', N'廚藝顧問出租')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (86, N'原料廠商代尋(介紹並協助議價)', N'以案計費', 5000, N'各式肉商、進口歐陸食材商、本地蔬果商、南北雜貨商', N'原料廠商代尋(介紹並協助議價).png', N'廚藝顧問出租')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (89, N'餐飲設備購置諮詢(線上) ', N'以小時計費', 500, N'冷凍設備商、烘焙設備商、大型廚房設備進口商、餐廚用品行', N'餐飲設備購置諮詢(線上).png', N'商用廚房設備規劃')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (90, N'餐飲設備購置諮詢(現場) ', N'以小時計費', 1000, N'冷凍設備商、烘焙設備商、大型廚房設備進口商、餐廚用品行', N'餐飲設備購置諮詢(現場).png', N'商用廚房設備規劃')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (92, N'廠商代尋(僅介紹)', N'以案計費', 1000, N'冷凍設備商、烘焙設備商、大型廚房設備進口商、餐廚用品行', N'廠商代尋(僅介紹).png', N'商用廚房設備規劃')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (93, N'廠商代尋(介紹並協助議價)', N'以案計費', 5000, N'冷凍設備商、烘焙設備商、大型廚房設備進口商、餐廚用品行', N'廠商代尋(介紹並協助議價).png', N'商用廚房設備規劃')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (94, N'原料採購', N'以堂計費', 3000, N'派出輔導員帶您到各項原物料的源頭進行採購，會詳細說明各原物料正常行情價、波動容許值、競價潛規則、行業暗語、某些原料的專家等...', N'原料採購.png', N'技術轉移保證班')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (99, N'內外場設備規劃', N'以堂計費', 3000, N'教育客戶內外場設備的種類，並讓客戶有能力根據自身需求判斷如何規劃設備位置、管線位置、設備等級選擇、能源效率比較、容積產出率比較', N'內外場設備規劃.png', N'技術轉移保證班')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (100, N'POS系統', N'以堂計費', 3000, N'瞭解客戶的實作場景，介紹市面上各大POS系統的優劣及性價比', N'POS系統.png', N'技術轉移保證班')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (101, N'製作半成品及儲存', N'以堂計費', 3000, N'教育客戶那些製作流程可以用高效率、節能、節約成本的方式，先做成半成品，並教育安全食品儲存觀念', N'製作半成品及儲存.png', N'技術轉移保證班')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (102, N'高效出餐模式', N'以堂計費', 3000, N'教育客戶如何利用製作好的半成品，以穩定、快速、美味、節約人力的方式供應餐點', N'高效出餐模式.png', N'技術轉移保證班')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (103, N'帳務製作', N'以堂計費', 3000, N'教育客戶如何利用POS機製作帳務報表，計算毛利率、周轉率、淨利率、各項成本所佔權重等...', N'帳務製作.png', N'技術轉移保證班')
SET IDENTITY_INSERT [dbo].[products] OFF
GO
USE [master]
GO
ALTER DATABASE [chefless] SET  READ_WRITE 
GO
