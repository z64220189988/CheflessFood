USE [master]
GO
/****** Object:  Database [chefless]    Script Date: 2024/2/14 上午 05:27:24 ******/
CREATE DATABASE [chefless]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'chefless', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\chefless.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'chefless_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\chefless_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[cart]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cartid] [int] IDENTITY(1,1) NOT NULL,
	[tobuypid] [int] NOT NULL,
	[tobuyquantity] [int] NOT NULL,
	[tobuycid] [int] NOT NULL,
	[tobuytime] [datetime] NOT NULL,
 CONSTRAINT [PK__cart__41663FC00101AC53] PRIMARY KEY CLUSTERED 
(
	[cartid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[duty]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[duty](
	[dutyid] [int] IDENTITY(1000,1) NOT NULL,
	[staffid] [int] NOT NULL,
	[checkin] [datetime] NULL,
	[checkout] [datetime] NULL,
	[權限] [int] NOT NULL,
 CONSTRAINT [PK__clerk__D837D05F4B7EE62A] PRIMARY KEY CLUSTERED 
(
	[dutyid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[expert]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[expert](
	[epid] [int] IDENTITY(1,1) NOT NULL,
	[eprigion] [nvarchar](50) NULL,
	[epseniority] [nvarchar](50) NULL,
	[epserviceway] [nvarchar](50) NULL,
	[epname] [nvarchar](50) NULL,
	[epskill] [nvarchar](100) NULL,
	[epcontact] [nvarchar](100) NULL,
	[epdesc] [nvarchar](500) NULL,
	[epimage] [nvarchar](50) NULL,
	[epbuyorsale] [nvarchar](50) NULL,
	[epcid] [int] NULL,
	[epdate] [date] NULL,
 CONSTRAINT [PK__expertsa__C408F6CA5A0B6537] PRIMARY KEY CLUSTERED 
(
	[epid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ordercontain]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ordercontain](
	[ocid] [int] IDENTITY(1,1) NOT NULL,
	[ocoid] [int] NOT NULL,
	[ocpid] [int] NOT NULL,
	[ocquantity] [int] NOT NULL,
 CONSTRAINT [PK_ordercontain] PRIMARY KEY CLUSTERED 
(
	[ocid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[oid] [int] IDENTITY(100,1) NOT NULL,
	[odate] [date] NOT NULL,
	[ototal] [int] NOT NULL,
	[ocustomerid] [int] NOT NULL,
	[ostatus] [nvarchar](20) NOT NULL,
	[orderpayway] [nchar](10) NOT NULL,
	[odesc] [nvarchar](500) NULL,
 CONSTRAINT [PK__orders__C2FFCF13DD2D4A26] PRIMARY KEY CLUSTERED 
(
	[oid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[persons]    Script Date: 2024/2/14 上午 05:27:24 ******/
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
/****** Object:  Table [dbo].[products]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[pid] [int] IDENTITY(10,1) NOT NULL,
	[pname] [nvarchar](50) NOT NULL,
	[pmode] [nvarchar](50) NOT NULL,
	[price] [int] NOT NULL,
	[pdesc] [nvarchar](1000) NOT NULL,
	[pimage] [nvarchar](100) NULL,
	[ptype] [nvarchar](50) NULL,
 CONSTRAINT [PK__products__DD37D91AA19C5817] PRIMARY KEY CLUSTERED 
(
	[pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[secondhand]    Script Date: 2024/2/14 上午 05:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[secondhand](
	[shid] [int] IDENTITY(1,1) NOT NULL,
	[shrigion] [nvarchar](50) NULL,
	[shnew] [nvarchar](50) NULL,
	[shtradeway] [nvarchar](50) NULL,
	[shnego] [nvarchar](50) NULL,
	[shname] [nvarchar](50) NULL,
	[shpname] [nvarchar](50) NULL,
	[shprice] [int] NULL,
	[shcontact] [nvarchar](100) NULL,
	[shdesc] [nvarchar](500) NULL,
	[shpimage] [nvarchar](50) NULL,
	[shbuyorsale] [nvarchar](50) NULL,
	[shcid] [int] NULL,
	[shdate] [date] NULL,
 CONSTRAINT [PK__shandsal__366F2DDCA0A2A89B] PRIMARY KEY CLUSTERED 
(
	[shid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[duty] ON 

INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1004, 9, CAST(N'2024-02-12T05:57:12.893' AS DateTime), CAST(N'2024-02-12T22:28:15.090' AS DateTime), 1)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1005, 9, CAST(N'2024-02-12T05:58:52.620' AS DateTime), CAST(N'2024-02-12T22:28:15.090' AS DateTime), 1)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1006, 9, CAST(N'2024-02-12T06:00:30.070' AS DateTime), CAST(N'2024-02-12T22:28:15.090' AS DateTime), 1)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1007, 9, CAST(N'2024-02-12T06:00:42.687' AS DateTime), CAST(N'2024-02-12T22:28:15.090' AS DateTime), 1)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1008, 9, CAST(N'2024-02-12T06:04:31.057' AS DateTime), CAST(N'2024-02-12T22:28:15.090' AS DateTime), 1)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1009, 4, CAST(N'2024-02-12T06:04:42.693' AS DateTime), CAST(N'2024-02-12T06:04:46.073' AS DateTime), 100)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1010, 9, CAST(N'2024-02-12T22:28:12.090' AS DateTime), CAST(N'2024-02-12T22:28:15.090' AS DateTime), 1)
INSERT [dbo].[duty] ([dutyid], [staffid], [checkin], [checkout], [權限]) VALUES (1011, 9, CAST(N'2024-02-13T05:11:44.940' AS DateTime), CAST(N'2024-02-13T05:11:48.790' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[duty] OFF
GO
SET IDENTITY_INSERT [dbo].[expert] ON 

INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (4, N'臺中市', N'11~15年', N'現場', N'Joyce Ma', N'帳務系統設計', N'Email : expert09@gmail.com', N'帳務系統設計是指設計一套系統來管理和追蹤組織的財務活動，包括記錄收入、支出、資產和負債等信息。這種系統通常包括記帳軟體和數據庫，用於收集、存儲和處理財務數據。設計一個有效的帳務系統需要考慮安全性、準確性、易用性和可擴展性等因素，以確保組織的財務信息得以準確記錄和管理。', N'專家9.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (5, N'花蓮縣', N'11~15年', N'線上', N'Mark Lai', N'法式醬汁實戰教學', N'電話 : 0966218966', N'法式醬汁實戰教學將教你如何製作出正宗的法式醬汁，如美味的貝爾尼斯醬、酥脆的白酒奶油醬等。這包括了選擇合適的材料，如奶油、蛋黃、蒜頭等，以及掌握正確的混合和加熱技巧。透過實際操作和步驟指導，你將能夠熟練地製作出各種口味豐富、質地細膩的法式醬汁，提升你的烹飪技能和料理水平。', N'專家1.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (6, N'高雄市', N'5年以下', N'現場', N'Jack Cheng', N'人事成本控管及面試技巧', N'電話 :09334562221', N'人事成本控管涉及管理企業在人力資源方面的花費，包括薪資、福利、培訓等。有效的成本控管需要妥善規劃、預算和監控人力資源的使用，以確保最佳的成本效益。面試技巧則是指在招聘過程中，應用有效的技巧和方法來評估應聘者的能力、素養和適配度。這包括問題設計、觀察技巧、溝通能力等，以確保企業能夠招募到最合適的人才。', N'專家2.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (7, N'高雄市', N'11~15年', N'線上', N'Max Wang', N'中式熱炒教學', N'電話 : 0933468715', N'中式熱炒是一種流行的烹飪方式，以快速高溫炒煮食材，保留食材的鮮嫩口感和營養價值。教學包括選擇新鮮食材，如蔬菜、肉類和海鮮，並準備切片、調味。在熱炒過程中，需要使用大火快速炒煮，加入調味料如蠔油、醬油等，並掌握火候和翻炒的技巧，以確保食材均勻受熱且保持爽脆。熱炒菜品的成功取決於熟練的技巧和對火候的掌握，因此透過實際操作和指導，能夠提高烹飪者的技能水平，並享受美味的中式家常菜。', N'專家4.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (8, N'桃園市', N'5年以下', N'線上', N'Yama Liao', N'歐式麵包膨化程度不足', N'電話 : 0933698159', N'歐式麵包膨化程度不足，徵求專家幫忙解決，報酬面議。', N'專家3.png', N'我要徵才', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (9, N'苗栗縣', N'5年以下', N'其他', N'Moiaces Chan', N'巧克力及翻糖專精', N'電話 : 0966841367', N'巧克力及翻糖專精是指專注於製作和裝飾巧克力甜點和翻糖蛋糕的技能和專業知識。這包括了準備高品質的巧克力、糖漿和翻糖材料，並掌握製作技巧，如溫度控制、混合、塗抹、雕刻等。在巧克力方面，重點在於創作各種口味和形狀的巧克力糖果、製作花裝飾和彩色塗抹。而在翻糖方面，則需要專注於製作和塗抹糖膏、模塑造型和進行細節修飾。透過不斷的練習和創新，巧克力及翻糖專精能夠創造出精緻美味的甜點藝術品，吸引顧客的眼球並滿足他們的味蕾。', N'專家8.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (10, N'新竹縣', N'11~15年', N'現場', N'Jessica Hou', N'宴席指尖茶點之設計與教學', N'電話 : 0966389713', N'宴席指尖茶點設計與教學旨在提供創意和精緻的茶點方案，以豐富宴會餐桌。這包括選擇優質食材、設計獨特造型、掌握裝飾技巧，如鮮花、巧克力、果凍等，使茶點美觀、色彩豐富、風味獨特。教學內容涵蓋茶點製作的基本步驟、工具選擇和技巧操作，並注重創意發揮和視覺效果。通過實踐操作和指導，學習者將能夠熟練製作各種風格和口味的茶點，提升宴席品質和賓客體驗。這種設計和教學能夠滿足宴席主辦者對品質和美感的需求，為宴會增添亮點和氛圍。', N'專家7.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (11, N'臺北市', N'5~10年', N'線上', N'Wamoca Yuan', N'正統義式披薩製作教學', N'電話 : 02-9876528', N'正統義式披薩製作教學教授如何製作地道的義式風味披薩，包括麵團、醬料和配料的製作。首先，學習者將學會如何調製鬆軟有彈性的披薩麵團，並掌握醱酵的技巧。接下來，教學將介紹正統的義式番茄醬配方，以及其他可能的醬料選擇。在配料部分，學習者將學會如何選擇和準備新鮮的食材，如薄切的意大利香腸、新鮮的蔬菜和優質的奶酪。最後，透過指導學習者將麵團擀平、塗抹醬料、撒上配料、烘烤等步驟，以確保製作出口感鬆軟、香味濃郁的正宗義式披薩。', N'專家5.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[expert] ([epid], [eprigion], [epseniority], [epserviceway], [epname], [epskill], [epcontact], [epdesc], [epimage], [epbuyorsale], [epcid], [epdate]) VALUES (13, N'臺中市', N'16~20年', N'現場', N'Alex Hong', N'戶外美式BBQ教學', N'電話 09879876629521133', N'戶外美式BBQ是一項享受陽光和烤肉的美好活動，結合了烹飪技巧和戶外生活的樂趣。首先，選擇新鮮的肉類，如牛肉、豬肉或雞肉，並根據需要切割成適當大小的塊狀。接著，調製BBQ醬，可以是自製的或商店購買的，然後在肉類表面均勻地塗抹醬料，再撒上鹽、胡椒等調味料。預熱燒烤爐或炭火，將調味好的肉類放在烤架上，定期翻動肉類以保持均勻烤熟。在烤的過程中，使用烤刷不斷地塗抹額外的BBQ醬，增添風味。同時，可以在烤架上放置一些木屑或果木片，製造煙燻效果，使肉類更加美味。使用溫度計測量肉類的內部溫度，確保達到安全的食用溫度。烤好後，讓肉類稍微休息一下，然後即可享用美味的戶外美式BBQ！這種烹飪方式不僅能讓你享受美食，還能享受與家人和朋友共度的愉快時光，是一個充滿歡樂和美好回憶的活動。', N'專家6.png', N'提供專長', 9, CAST(N'2024-02-13' AS Date))
SET IDENTITY_INSERT [dbo].[expert] OFF
GO
SET IDENTITY_INSERT [dbo].[ordercontain] ON 

INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (1, 102, 35, 300)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (2, 102, 39, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (3, 102, 40, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (4, 102, 52, 10)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (5, 102, 54, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (6, 102, 55, 50)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (7, 102, 56, 310)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (8, 103, 36, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (9, 103, 37, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (10, 103, 38, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (11, 103, 45, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (12, 103, 51, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (13, 103, 52, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (14, 103, 53, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (15, 103, 54, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (16, 103, 55, 501)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (17, 103, 56, 310)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (18, 104, 11, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (19, 104, 16, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (20, 104, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (21, 104, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (22, 104, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (23, 104, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (24, 104, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (25, 104, 31, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (26, 104, 32, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (27, 104, 36, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (28, 104, 37, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (29, 104, 38, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (30, 104, 40, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (31, 104, 47, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (32, 104, 48, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (33, 104, 50, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (34, 104, 52, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (35, 105, 11, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (36, 105, 16, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (37, 105, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (38, 105, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (39, 105, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (40, 105, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (41, 105, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (42, 105, 31, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (43, 105, 32, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (44, 105, 36, 500)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (45, 105, 37, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (46, 105, 38, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (47, 105, 40, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (48, 105, 47, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (49, 105, 48, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (50, 105, 50, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (51, 106, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (52, 106, 32, 60)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (53, 106, 37, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (54, 106, 47, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (55, 107, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (56, 107, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (57, 107, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (58, 107, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (59, 107, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (60, 107, 32, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (61, 107, 36, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (62, 107, 37, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (63, 107, 38, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (64, 107, 40, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (65, 107, 47, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (66, 107, 48, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (67, 107, 50, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (68, 108, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (69, 108, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (70, 108, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (71, 108, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (72, 108, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (73, 108, 32, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (74, 109, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (75, 109, 22, 15)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (76, 109, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (77, 109, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (78, 109, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (79, 110, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (80, 110, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (81, 110, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (82, 110, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (83, 110, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (84, 110, 32, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (85, 110, 36, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (86, 110, 37, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (87, 110, 38, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (88, 110, 40, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (89, 110, 47, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (90, 110, 48, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (91, 110, 50, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (92, 111, 19, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (93, 111, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (94, 111, 24, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (95, 111, 26, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (96, 111, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (97, 111, 32, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (98, 111, 40, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (99, 111, 47, 1)
GO
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (100, 111, 48, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (101, 111, 50, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (102, 112, 36, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (103, 113, 17, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (104, 113, 24, 6)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (105, 113, 26, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (106, 113, 36, 3)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (107, 113, 37, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (108, 113, 38, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (109, 113, 45, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (110, 113, 47, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (111, 113, 52, 2)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (112, 113, 53, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (113, 114, 11, 50)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (114, 114, 51, 222)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (115, 114, 55, 300)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (116, 114, 61, 500)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (117, 114, 68, 60)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (118, 114, 77, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (119, 114, 82, 7)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (120, 114, 83, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (121, 114, 84, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (122, 114, 85, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (123, 114, 86, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (124, 114, 93, 451)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (125, 114, 94, 8)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (126, 114, 99, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (127, 114, 100, 5)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (128, 114, 101, 10)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (129, 115, 22, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (130, 115, 32, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (131, 115, 33, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (132, 115, 54, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (133, 115, 77, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (134, 115, 93, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (135, 116, 28, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (136, 116, 201, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (137, 116, 205, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (138, 116, 209, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (139, 117, 126, 1)
INSERT [dbo].[ordercontain] ([ocid], [ocoid], [ocpid], [ocquantity]) VALUES (140, 118, 11, 1)
SET IDENTITY_INSERT [dbo].[ordercontain] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (100, CAST(N'2024-02-01' AS Date), 134000, 9, N'已取消', N'現金        ', N'請輸入備註')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (101, CAST(N'2024-02-02' AS Date), 36648000, 9, N'已完成', N'現金        ', N'請輸入備註')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (102, CAST(N'2024-02-03' AS Date), 36648000, 9, N'未出貨', N'現金        ', N'請輸入備註')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (103, CAST(N'2024-02-04' AS Date), 21658000, 9, N'未出貨', N'匯款        ', N'請輸入備註')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (104, CAST(N'2024-02-05' AS Date), 588000, 9, N'未出貨', N'匯款        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (105, CAST(N'2024-02-06' AS Date), 576000, 9, N'未出貨', N'現金        ', N'546666666666666666666666666666668')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (106, CAST(N'2024-02-05' AS Date), 324000, 9, N'已完成', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (107, CAST(N'2024-02-05' AS Date), 542000, 9, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (108, CAST(N'2024-02-05' AS Date), 362000, 9, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (109, CAST(N'2024-02-05' AS Date), 112000, 9, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (110, CAST(N'2024-02-05' AS Date), 542000, 9, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (111, CAST(N'2024-02-05' AS Date), 476000, 9, N'處理中', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (112, CAST(N'2024-02-05' AS Date), 4000, 9, N'處理中', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (113, CAST(N'2024-02-05' AS Date), 804000, 9, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (114, CAST(N'2024-02-09' AS Date), 16699000, 9, N'已取消', N'現金        ', N'請輸入備註，最多500字')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (115, CAST(N'2024-02-12' AS Date), 71500, 25, N'未出貨', N'匯款        ', N'請派生日是1994/04/32的輔導員')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (116, CAST(N'2024-02-13' AS Date), 21000, 31, N'已取消', N'匯款        ', N'我的威靈頓牛排要做素食')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (117, CAST(N'2024-02-13' AS Date), 3000, 31, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
INSERT [dbo].[orders] ([oid], [odate], [ototal], [ocustomerid], [ostatus], [orderpayway], [odesc]) VALUES (118, CAST(N'2024-02-13' AS Date), 3000, 31, N'未出貨', N'現金        ', N'請輸入備註，最多500字。')
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[persons] ON 

INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (1, N'1111@gmail.com', N'洩腦版', 1000, N'1111', N'1111111112')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (2, N'2222@gmail.com', N'蟑愚歌', 1000, N'2222', N'2222')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (3, N'3333@gmail.com', N'河棉寶寶', 10, N'3333', N'3333')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (4, N'4444@gmail.com', N'水箭龜', 100, N'4444', N'4444')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (5, N'5555@gmail.com', N'皮卡秋', 100, N'5555', N'5555')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (6, N'6666@gmail.com', N'皮卡乒', 10, N'6666', N'6666')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (8, N'7777@gmail.com', N'皮卡乓', 9, N'7777', N'7777')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (9, N'88', N'皮卡乒乓', 1, N'8888', N'88')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (20, N'487@gmail.com', N'皮卡乒乒乓乓', 1000, N'87', N'0987987987')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (23, N'9999', N'9999', 1000, N'9999', N'9999')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (24, N'qqqq', N'qqqq', 10, N'qqqq', N'qqqq')
INSERT [dbo].[persons] ([id], [email], [姓名], [權限], [密碼], [手機號碼]) VALUES (31, N'z64220189988@gmail.com', N'暴鯉大蛇丸', 1000, N'zmax20000', N'0987487987')
SET IDENTITY_INSERT [dbo].[persons] OFF
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (11, N'四川藤椒清燉牛肉麵', N'材料品項', 3000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛！', N'四川藤椒清燉牛肉麵.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (16, N'四川藤椒清燉牛肉麵', N'材料品項、製作步驟', 12000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛456654', N'四川藤椒清燉牛肉麵.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (17, N'四川藤椒清燉牛肉麵', N'材料品項、製作步驟、配方比例', 50000, N'四川藤椒清燉牛肉麵是一道風味獨特的中國川菜，以其辛辣的藤椒風味和鮮嫩的牛肉而聞名。這道菜將牛肉悶煮至極嫩，再加入四川特色的藤椒調味，燉煮成濃郁的湯汁，搭配彈牙的麵條，風味醇厚，香辣開胃。絕對是辛香愛好者的最愛！', N'四川藤椒清燉牛肉麵.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (18, N'夏里亞賓牛排', N'材料品項', 2000, N'夏里亞賓牛排的特徵是使用大量洋蔥作烹調，其肉質極為軟嫩，香氣馥郁。提供純正的牛肉風味。6666', N'夏里亞賓牛排.png', N'商用料理配方')
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
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (103, N'帳務製作', N'以堂計費', 3300, N'教育客戶如何利用POS機製作帳務報表，計算毛利率、周轉率、淨利率、各項成本所佔權重等...', N'帳務製作.png', N'技術轉移保證班')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (116, N'test1', N'test1', 87, N'66', N'水箭龜.png', N'test1')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (126, N'八寶芋泥', N'材料品項', 3000, N'
八寶芋泥是一道傳統的中式甜品，以紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生等八種寶貴食材製成，搭配芋頭熬煮而成。首先，將芋頭削皮切塊，加水煮至軟爛，然後壓成泥狀。接著，將紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生用清水浸泡，然後煮熟，並加入少許冰糖調味。最後，將已熟的八寶食材與芋泥混合均勻，再稍微加熱一下，即可完成。這道甜品口感綿密細膩，香甜可口，不僅富含營養，而且色澤美麗，深受人們喜愛。八寶芋泥不僅是一道美味的點心，也常見於節慶或家庭聚會上，象徵著幸福和美好的未來。', N'八寶芋泥.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (127, N'八寶芋泥', N'材料品項、製作步驟', 12000, N'
八寶芋泥是一道傳統的中式甜品，以紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生等八種寶貴食材製成，搭配芋頭熬煮而成。首先，將芋頭削皮切塊，加水煮至軟爛，然後壓成泥狀。接著，將紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生用清水浸泡，然後煮熟，並加入少許冰糖調味。最後，將已熟的八寶食材與芋泥混合均勻，再稍微加熱一下，即可完成。這道甜品口感綿密細膩，香甜可口，不僅富含營養，而且色澤美麗，深受人們喜愛。八寶芋泥不僅是一道美味的點心，也常見於節慶或家庭聚會上，象徵著幸福和美好的未來。', N'八寶芋泥.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (128, N'八寶芋泥', N'材料品項、製作步驟、配方比例', 50000, N'
八寶芋泥是一道傳統的中式甜品，以紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生等八種寶貴食材製成，搭配芋頭熬煮而成。首先，將芋頭削皮切塊，加水煮至軟爛，然後壓成泥狀。接著，將紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生用清水浸泡，然後煮熟，並加入少許冰糖調味。最後，將已熟的八寶食材與芋泥混合均勻，再稍微加熱一下，即可完成。這道甜品口感綿密細膩，香甜可口，不僅富含營養，而且色澤美麗，深受人們喜愛。八寶芋泥不僅是一道美味的點心，也常見於節慶或家庭聚會上，象徵著幸福和美好的未來。', N'八寶芋泥.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (168, N'八寶芋泥', N'500g/份，100份/件', 8000, N'
八寶芋泥是一道傳統的中式甜品，以紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生等八種寶貴食材製成，搭配芋頭熬煮而成。首先，將芋頭削皮切塊，加水煮至軟爛，然後壓成泥狀。接著，將紅藜、蓮子、紅棗、桂圓、葡萄乾、薏仁、杏仁和花生用清水浸泡，然後煮熟，並加入少許冰糖調味。最後，將已熟的八寶食材與芋泥混合均勻，再稍微加熱一下，即可完成。這道甜品口感綿密細膩，香甜可口，不僅富含營養，而且色澤美麗，深受人們喜愛。八寶芋泥不僅是一道美味的點心，也常見於節慶或家庭聚會上，象徵著幸福和美好的未來。', N'八寶芋泥.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (169, N'玉里柑橘櫻桃鴨胸', N'材料品項', 3000, N'玉里柑橘櫻桃鴨胸是一道精緻而美味的菜餚，融合了柑橘的清新香氣、櫻桃的甜美風味和鴨胸的鮮嫩口感。首先，將鴨胸切成適量大小的肉塊，然後用鹽和黑胡椒調味。接著，將柑橘和櫻桃分別搗成泥狀，並加入少許蜂蜜調味。然後，在熱平底鍋中，將鴨胸皮朝下煎至金黃色，然後翻轉，再將柑橘和櫻桃泥均勻塗抹在鴨胸上，繼續煎至鴨胸達到理想的熟度。最後，將煎好的鴨胸切片，擺盤，淋上柑橘櫻桃醬汁，並可以搭配新鮮的蔬菜或烤蔬菜作為配菜。這道菜色澤誘人，口感豐富，鴨胸的香氣與柑橘和櫻桃的酸甜交融，帶來一場美妙的味覺享受。', N'玉里柑橘櫻桃鴨胸.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (171, N'玉里柑橘櫻桃鴨胸', N'材料品項、製作步驟', 12000, N'玉里柑橘櫻桃鴨胸是一道精緻而美味的菜餚，融合了柑橘的清新香氣、櫻桃的甜美風味和鴨胸的鮮嫩口感。首先，將鴨胸切成適量大小的肉塊，然後用鹽和黑胡椒調味。接著，將柑橘和櫻桃分別搗成泥狀，並加入少許蜂蜜調味。然後，在熱平底鍋中，將鴨胸皮朝下煎至金黃色，然後翻轉，再將柑橘和櫻桃泥均勻塗抹在鴨胸上，繼續煎至鴨胸達到理想的熟度。最後，將煎好的鴨胸切片，擺盤，淋上柑橘櫻桃醬汁，並可以搭配新鮮的蔬菜或烤蔬菜作為配菜。這道菜色澤誘人，口感豐富，鴨胸的香氣與柑橘和櫻桃的酸甜交融，帶來一場美妙的味覺享受。', N'玉里柑橘櫻桃鴨胸.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (173, N'玉里柑橘櫻桃鴨胸', N'材料品項、製作步驟、配方比例', 50000, N'玉里柑橘櫻桃鴨胸是一道精緻而美味的菜餚，融合了柑橘的清新香氣、櫻桃的甜美風味和鴨胸的鮮嫩口感。首先，將鴨胸切成適量大小的肉塊，然後用鹽和黑胡椒調味。接著，將柑橘和櫻桃分別搗成泥狀，並加入少許蜂蜜調味。然後，在熱平底鍋中，將鴨胸皮朝下煎至金黃色，然後翻轉，再將柑橘和櫻桃泥均勻塗抹在鴨胸上，繼續煎至鴨胸達到理想的熟度。最後，將煎好的鴨胸切片，擺盤，淋上柑橘櫻桃醬汁，並可以搭配新鮮的蔬菜或烤蔬菜作為配菜。這道菜色澤誘人，口感豐富，鴨胸的香氣與柑橘和櫻桃的酸甜交融，帶來一場美妙的味覺享受。', N'玉里柑橘櫻桃鴨胸.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (175, N'玉里柑橘櫻桃鴨胸', N'500g/份，100份/件', 8000, N'玉里柑橘櫻桃鴨胸是一道精緻而美味的菜餚，融合了柑橘的清新香氣、櫻桃的甜美風味和鴨胸的鮮嫩口感。首先，將鴨胸切成適量大小的肉塊，然後用鹽和黑胡椒調味。接著，將柑橘和櫻桃分別搗成泥狀，並加入少許蜂蜜調味。然後，在熱平底鍋中，將鴨胸皮朝下煎至金黃色，然後翻轉，再將柑橘和櫻桃泥均勻塗抹在鴨胸上，繼續煎至鴨胸達到理想的熟度。最後，將煎好的鴨胸切片，擺盤，淋上柑橘櫻桃醬汁，並可以搭配新鮮的蔬菜或烤蔬菜作為配菜。這道菜色澤誘人，口感豐富，鴨胸的香氣與柑橘和櫻桃的酸甜交融，帶來一場美妙的味覺享受。', N'玉里柑橘櫻桃鴨胸.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (177, N'地中海式白酒炒海鮮', N'材料品項', 3000, N'地中海式白酒炒海鮮是一道充滿地中海風情的美味佳餚，結合了新鮮的海鮮和濃郁的白酒風味。首先，選擇各種新鮮的海鮮，如蝦、蚌、魷魚等，清洗乾淨備用。接著，在平底鍋中加熱橄欖油，加入切碎的大蒜和切片的洋蔥，炒香至金黃色。然後，將海鮮加入鍋中，繼續炒至海鮮變色。接著，倒入白酒，煮沸並稍微煮幹。最後，加入番茄醬、新鮮的番茄和香草，調味並繼續烹飪幾分鐘，直到海鮮完全熟透並吸收了酒的風味。這道菜色澤誘人，香氣撲鼻，口感豐富。白酒炒海鮮搭配著地中海風味的香草和番茄，給人一種清爽宜人的享受，適合作為正餐或節日盛宴的菜品。', N'地中海式白酒炒海鮮.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (178, N'地中海式白酒炒海鮮', N'材料品項、製作步驟', 12000, N'地中海式白酒炒海鮮是一道充滿地中海風情的美味佳餚，結合了新鮮的海鮮和濃郁的白酒風味。首先，選擇各種新鮮的海鮮，如蝦、蚌、魷魚等，清洗乾淨備用。接著，在平底鍋中加熱橄欖油，加入切碎的大蒜和切片的洋蔥，炒香至金黃色。然後，將海鮮加入鍋中，繼續炒至海鮮變色。接著，倒入白酒，煮沸並稍微煮幹。最後，加入番茄醬、新鮮的番茄和香草，調味並繼續烹飪幾分鐘，直到海鮮完全熟透並吸收了酒的風味。這道菜色澤誘人，香氣撲鼻，口感豐富。白酒炒海鮮搭配著地中海風味的香草和番茄，給人一種清爽宜人的享受，適合作為正餐或節日盛宴的菜品。', N'地中海式白酒炒海鮮.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (179, N'地中海式白酒炒海鮮', N'材料品項、製作步驟、配方比例', 50000, N'地中海式白酒炒海鮮是一道充滿地中海風情的美味佳餚，結合了新鮮的海鮮和濃郁的白酒風味。首先，選擇各種新鮮的海鮮，如蝦、蚌、魷魚等，清洗乾淨備用。接著，在平底鍋中加熱橄欖油，加入切碎的大蒜和切片的洋蔥，炒香至金黃色。然後，將海鮮加入鍋中，繼續炒至海鮮變色。接著，倒入白酒，煮沸並稍微煮幹。最後，加入番茄醬、新鮮的番茄和香草，調味並繼續烹飪幾分鐘，直到海鮮完全熟透並吸收了酒的風味。這道菜色澤誘人，香氣撲鼻，口感豐富。白酒炒海鮮搭配著地中海風味的香草和番茄，給人一種清爽宜人的享受，適合作為正餐或節日盛宴的菜品。', N'地中海式白酒炒海鮮.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (180, N'地中海式白酒炒海鮮', N'500g/份，100份/件', 8000, N'地中海式白酒炒海鮮是一道充滿地中海風情的美味佳餚，結合了新鮮的海鮮和濃郁的白酒風味。首先，選擇各種新鮮的海鮮，如蝦、蚌、魷魚等，清洗乾淨備用。接著，在平底鍋中加熱橄欖油，加入切碎的大蒜和切片的洋蔥，炒香至金黃色。然後，將海鮮加入鍋中，繼續炒至海鮮變色。接著，倒入白酒，煮沸並稍微煮幹。最後，加入番茄醬、新鮮的番茄和香草，調味並繼續烹飪幾分鐘，直到海鮮完全熟透並吸收了酒的風味。這道菜色澤誘人，香氣撲鼻，口感豐富。白酒炒海鮮搭配著地中海風味的香草和番茄，給人一種清爽宜人的享受，適合作為正餐或節日盛宴的菜品。', N'地中海式白酒炒海鮮.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (181, N'百花鑲豆腐', N'材料品項', 3000, N'百花鑲豆腐是一道精緻的中式菜品，以豆腐為主料，配以各種鮮蔬和菇類，再加上香料和調味料調製而成。首先，將豆腐切成適當大小的塊狀，然後將其煎至表面微黃，使豆腐更加香脆。接著，將各種鮮蔬和菇類切成細小的丁狀或片狀，如胡蘿蔔、青椒、香菇等，並與豆腐一起在平底鍋中翻炒至蔬菜熟軟。然後，調製一份鮮香的調味汁，包括蠔油、醬油、糖和水混合而成，並將其均勻地淋在炒好的豆腐和蔬菜上。最後，用火腿、鮮蝦或其他喜愛的食材將豆腐塊中間填充起來，裝飾成花朵狀，提升視覺效果。這道菜鮮嫩爽口，香氣四溢，蔬菜的營養與豆腐的蛋白質相輔相成，是一道美味又營養豐富的素食佳餚，適合家庭聚餐或節日盛宴享用。', N'百花鑲豆腐.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (182, N'百花鑲豆腐', N'材料品項、製作步驟', 12000, N'百花鑲豆腐是一道精緻的中式菜品，以豆腐為主料，配以各種鮮蔬和菇類，再加上香料和調味料調製而成。首先，將豆腐切成適當大小的塊狀，然後將其煎至表面微黃，使豆腐更加香脆。接著，將各種鮮蔬和菇類切成細小的丁狀或片狀，如胡蘿蔔、青椒、香菇等，並與豆腐一起在平底鍋中翻炒至蔬菜熟軟。然後，調製一份鮮香的調味汁，包括蠔油、醬油、糖和水混合而成，並將其均勻地淋在炒好的豆腐和蔬菜上。最後，用火腿、鮮蝦或其他喜愛的食材將豆腐塊中間填充起來，裝飾成花朵狀，提升視覺效果。這道菜鮮嫩爽口，香氣四溢，蔬菜的營養與豆腐的蛋白質相輔相成，是一道美味又營養豐富的素食佳餚，適合家庭聚餐或節日盛宴享用。', N'百花鑲豆腐.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (183, N'百花鑲豆腐', N'材料品項、製作步驟、配方比例', 50000, N'百花鑲豆腐是一道精緻的中式菜品，以豆腐為主料，配以各種鮮蔬和菇類，再加上香料和調味料調製而成。首先，將豆腐切成適當大小的塊狀，然後將其煎至表面微黃，使豆腐更加香脆。接著，將各種鮮蔬和菇類切成細小的丁狀或片狀，如胡蘿蔔、青椒、香菇等，並與豆腐一起在平底鍋中翻炒至蔬菜熟軟。然後，調製一份鮮香的調味汁，包括蠔油、醬油、糖和水混合而成，並將其均勻地淋在炒好的豆腐和蔬菜上。最後，用火腿、鮮蝦或其他喜愛的食材將豆腐塊中間填充起來，裝飾成花朵狀，提升視覺效果。這道菜鮮嫩爽口，香氣四溢，蔬菜的營養與豆腐的蛋白質相輔相成，是一道美味又營養豐富的素食佳餚，適合家庭聚餐或節日盛宴享用。', N'百花鑲豆腐.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (184, N'百花鑲豆腐', N'500g/份，100份/件', 8000, N'百花鑲豆腐是一道精緻的中式菜品，以豆腐為主料，配以各種鮮蔬和菇類，再加上香料和調味料調製而成。首先，將豆腐切成適當大小的塊狀，然後將其煎至表面微黃，使豆腐更加香脆。接著，將各種鮮蔬和菇類切成細小的丁狀或片狀，如胡蘿蔔、青椒、香菇等，並與豆腐一起在平底鍋中翻炒至蔬菜熟軟。然後，調製一份鮮香的調味汁，包括蠔油、醬油、糖和水混合而成，並將其均勻地淋在炒好的豆腐和蔬菜上。最後，用火腿、鮮蝦或其他喜愛的食材將豆腐塊中間填充起來，裝飾成花朵狀，提升視覺效果。這道菜鮮嫩爽口，香氣四溢，蔬菜的營養與豆腐的蛋白質相輔相成，是一道美味又營養豐富的素食佳餚，適合家庭聚餐或節日盛宴享用。', N'百花鑲豆腐.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (185, N'西班牙開胃四拼Tapas', N'材料品項', 3000, N'西班牙開胃四拼Tapas是一道結合了西班牙傳統風味和當地食材的精緻開胃菜，由四種小吃組成，每種小吃都有其獨特的口味和風情。首先，其中一款可能是鮮嫩多汁的西班牙火腿配橄欖，這組合將帶來濃郁的肉香和橄欖的鹹味。第二款可能是炸魚塊，外酥內嫩，搭配蒜泥蛋黃醬，口感鮮美。第三款可能是西班牙烤麵包，配以番茄、橄欖油和大蒜，味道清新爽口。最後一款可能是香煎馬鈴薯片，灑上海鹽和迷迭香，香脆可口。這四種小吃組合了不同的口味和質地，既可滿足食客對於不同風味的追求，又能讓他們體驗到西班牙獨特的美食文化和社交氛圍。這道Tapas不僅適合作為餐前小吃，也是西班牙餐廳和酒吧常見的招牌菜品之一，為用餐氛圍增添一份樂趣和情趣。', N'西班牙開胃四拼Tapas.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (186, N'西班牙開胃四拼Tapas', N'材料品項、製作步驟', 12000, N'西班牙開胃四拼Tapas是一道結合了西班牙傳統風味和當地食材的精緻開胃菜，由四種小吃組成，每種小吃都有其獨特的口味和風情。首先，其中一款可能是鮮嫩多汁的西班牙火腿配橄欖，這組合將帶來濃郁的肉香和橄欖的鹹味。第二款可能是炸魚塊，外酥內嫩，搭配蒜泥蛋黃醬，口感鮮美。第三款可能是西班牙烤麵包，配以番茄、橄欖油和大蒜，味道清新爽口。最後一款可能是香煎馬鈴薯片，灑上海鹽和迷迭香，香脆可口。這四種小吃組合了不同的口味和質地，既可滿足食客對於不同風味的追求，又能讓他們體驗到西班牙獨特的美食文化和社交氛圍。這道Tapas不僅適合作為餐前小吃，也是西班牙餐廳和酒吧常見的招牌菜品之一，為用餐氛圍增添一份樂趣和情趣。', N'西班牙開胃四拼Tapas.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (187, N'西班牙開胃四拼Tapas', N'材料品項、製作步驟、配方比例', 50000, N'西班牙開胃四拼Tapas是一道結合了西班牙傳統風味和當地食材的精緻開胃菜，由四種小吃組成，每種小吃都有其獨特的口味和風情。首先，其中一款可能是鮮嫩多汁的西班牙火腿配橄欖，這組合將帶來濃郁的肉香和橄欖的鹹味。第二款可能是炸魚塊，外酥內嫩，搭配蒜泥蛋黃醬，口感鮮美。第三款可能是西班牙烤麵包，配以番茄、橄欖油和大蒜，味道清新爽口。最後一款可能是香煎馬鈴薯片，灑上海鹽和迷迭香，香脆可口。這四種小吃組合了不同的口味和質地，既可滿足食客對於不同風味的追求，又能讓他們體驗到西班牙獨特的美食文化和社交氛圍。這道Tapas不僅適合作為餐前小吃，也是西班牙餐廳和酒吧常見的招牌菜品之一，為用餐氛圍增添一份樂趣和情趣。', N'西班牙開胃四拼Tapas.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (188, N'西班牙開胃四拼Tapas', N'500g/份，100份/件', 8000, N'西班牙開胃四拼Tapas是一道結合了西班牙傳統風味和當地食材的精緻開胃菜，由四種小吃組成，每種小吃都有其獨特的口味和風情。首先，其中一款可能是鮮嫩多汁的西班牙火腿配橄欖，這組合將帶來濃郁的肉香和橄欖的鹹味。第二款可能是炸魚塊，外酥內嫩，搭配蒜泥蛋黃醬，口感鮮美。第三款可能是西班牙烤麵包，配以番茄、橄欖油和大蒜，味道清新爽口。最後一款可能是香煎馬鈴薯片，灑上海鹽和迷迭香，香脆可口。這四種小吃組合了不同的口味和質地，既可滿足食客對於不同風味的追求，又能讓他們體驗到西班牙獨特的美食文化和社交氛圍。這道Tapas不僅適合作為餐前小吃，也是西班牙餐廳和酒吧常見的招牌菜品之一，為用餐氛圍增添一份樂趣和情趣。', N'西班牙開胃四拼Tapas.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (189, N'豆酥鱈魚', N'材料品項', 3000, N'豆酥鱈鱼是一道風味獨特且口感豐富的佳餚，主要將新鮮鱈魚搭配上酥脆的豆酥，通過巧妙的烹調技巧將兩者完美結合。鱈魚肉質細嫩、味道鮮美，搭配上豆酥的香脆，為這道菜品增添了多層次的口感和風味。

製作這道菜時，首先將鱈魚切成適口大小的塊狀，輕輕醃製以增強其風味。接著，將鱈魚塊裹上一層薄薄的麵粉或玉米澱粉，這一步驟不僅可以幫助豆酥更好地附著在魚肉上，還能在烹煮過程中形成一層酥脆的外殼，保留魚肉的鮮嫩。

烹調過程中，鱈魚塊會先在油鍋中炸至金黃酥脆，然後撒上預先炒香的豆酥，豆酥的製作通常包括黃豆或其他豆類的混合，經過烘烤或炸製至金黃色，釋放出濃郁的豆香。最後，根據個人口味可選擇加入蔥花、辣椒或其他調味料來調配，這樣一道既有海鮮的鮮美，又有豆酥的酥香，口感豐富多變的豆酥鱈魚便完成了。

豆酥鱈魚不僅美味，而且營養豐富，鱈魚富含高質量蛋白質和Omega-3脂肪酸，對健康有益。這道菜適合各種場合，無論是家庭聚餐還是節日慶典，都能成為桌上的一道亮點。', N'豆酥鱈魚.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (190, N'豆酥鱈魚', N'材料品項、製作步驟', 12000, N'豆酥鱈鱼是一道風味獨特且口感豐富的佳餚，主要將新鮮鱈魚搭配上酥脆的豆酥，通過巧妙的烹調技巧將兩者完美結合。鱈魚肉質細嫩、味道鮮美，搭配上豆酥的香脆，為這道菜品增添了多層次的口感和風味。

製作這道菜時，首先將鱈魚切成適口大小的塊狀，輕輕醃製以增強其風味。接著，將鱈魚塊裹上一層薄薄的麵粉或玉米澱粉，這一步驟不僅可以幫助豆酥更好地附著在魚肉上，還能在烹煮過程中形成一層酥脆的外殼，保留魚肉的鮮嫩。

烹調過程中，鱈魚塊會先在油鍋中炸至金黃酥脆，然後撒上預先炒香的豆酥，豆酥的製作通常包括黃豆或其他豆類的混合，經過烘烤或炸製至金黃色，釋放出濃郁的豆香。最後，根據個人口味可選擇加入蔥花、辣椒或其他調味料來調配，這樣一道既有海鮮的鮮美，又有豆酥的酥香，口感豐富多變的豆酥鱈魚便完成了。

豆酥鱈魚不僅美味，而且營養豐富，鱈魚富含高質量蛋白質和Omega-3脂肪酸，對健康有益。這道菜適合各種場合，無論是家庭聚餐還是節日慶典，都能成為桌上的一道亮點。', N'豆酥鱈魚.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (191, N'豆酥鱈魚', N'材料品項、製作步驟、配方比例', 50000, N'豆酥鱈鱼是一道風味獨特且口感豐富的佳餚，主要將新鮮鱈魚搭配上酥脆的豆酥，通過巧妙的烹調技巧將兩者完美結合。鱈魚肉質細嫩、味道鮮美，搭配上豆酥的香脆，為這道菜品增添了多層次的口感和風味。

製作這道菜時，首先將鱈魚切成適口大小的塊狀，輕輕醃製以增強其風味。接著，將鱈魚塊裹上一層薄薄的麵粉或玉米澱粉，這一步驟不僅可以幫助豆酥更好地附著在魚肉上，還能在烹煮過程中形成一層酥脆的外殼，保留魚肉的鮮嫩。

烹調過程中，鱈魚塊會先在油鍋中炸至金黃酥脆，然後撒上預先炒香的豆酥，豆酥的製作通常包括黃豆或其他豆類的混合，經過烘烤或炸製至金黃色，釋放出濃郁的豆香。最後，根據個人口味可選擇加入蔥花、辣椒或其他調味料來調配，這樣一道既有海鮮的鮮美，又有豆酥的酥香，口感豐富多變的豆酥鱈魚便完成了。

豆酥鱈魚不僅美味，而且營養豐富，鱈魚富含高質量蛋白質和Omega-3脂肪酸，對健康有益。這道菜適合各種場合，無論是家庭聚餐還是節日慶典，都能成為桌上的一道亮點。', N'豆酥鱈魚.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (192, N'豆酥鱈魚', N'500g/份，100份/件', 8000, N'豆酥鱈鱼是一道風味獨特且口感豐富的佳餚，主要將新鮮鱈魚搭配上酥脆的豆酥，通過巧妙的烹調技巧將兩者完美結合。鱈魚肉質細嫩、味道鮮美，搭配上豆酥的香脆，為這道菜品增添了多層次的口感和風味。

製作這道菜時，首先將鱈魚切成適口大小的塊狀，輕輕醃製以增強其風味。接著，將鱈魚塊裹上一層薄薄的麵粉或玉米澱粉，這一步驟不僅可以幫助豆酥更好地附著在魚肉上，還能在烹煮過程中形成一層酥脆的外殼，保留魚肉的鮮嫩。

烹調過程中，鱈魚塊會先在油鍋中炸至金黃酥脆，然後撒上預先炒香的豆酥，豆酥的製作通常包括黃豆或其他豆類的混合，經過烘烤或炸製至金黃色，釋放出濃郁的豆香。最後，根據個人口味可選擇加入蔥花、辣椒或其他調味料來調配，這樣一道既有海鮮的鮮美，又有豆酥的酥香，口感豐富多變的豆酥鱈魚便完成了。

豆酥鱈魚不僅美味，而且營養豐富，鱈魚富含高質量蛋白質和Omega-3脂肪酸，對健康有益。這道菜適合各種場合，無論是家庭聚餐還是節日慶典，都能成為桌上的一道亮點。', N'豆酥鱈魚.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (193, N'直火佛蘭肯式牛小排', N'材料品項', 3000, N'
直火佛蘭肯式牛小排是一種採用直火燒烤方式精心製作的牛肉美食，這種烹調方法能夠充分展現牛肉的原始風味和獨特質感。佛蘭肯式的烹調手法源於傳統歐洲料理，特別強調食材的自然風味和烹飪技巧的純熟，使得牛小排在直火的烤制下，表皮呈現出誘人的金黃色澤，帶有微微的焦香，而內部肉質依然保持多汁和嫩滑。

在製作過程中，牛小排會先經過仔細的挑選和處理，保證肉質的新鮮和優質。接著，將牛小排用特製的醃料醃制一段時間，這些醃料可能包含草本香料、大蒜、橄欖油等，旨在增強牛肉的風味。醃制過後的牛小排被放置在高溫的直火烤架上，經過專業的翻轉和控制，使每一面都均勻受熱，烤出完美的焦脆外皮和保持內部的嫩度。

此外，直火佛蘭肯式牛小排的獨到之處還在於其伴隨的醬汁和佐菜，常見的有紅酒醬汁、香草奶油醬或是簡單的海鹽和黑胡椒，這些配料不僅豐富了牛小排的層次，也讓整道菜的味道更加豐富多元。食用時，切片後的牛小排紋理清晰，肉汁豐富，每一口都能感受到牛肉的鮮美與烤制技巧的精妙，是一道適合在特殊場合享用的高級美食。', N'直火佛蘭肯式牛小排.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (194, N'直火佛蘭肯式牛小排', N'材料品項、製作步驟', 12000, N'
直火佛蘭肯式牛小排是一種採用直火燒烤方式精心製作的牛肉美食，這種烹調方法能夠充分展現牛肉的原始風味和獨特質感。佛蘭肯式的烹調手法源於傳統歐洲料理，特別強調食材的自然風味和烹飪技巧的純熟，使得牛小排在直火的烤制下，表皮呈現出誘人的金黃色澤，帶有微微的焦香，而內部肉質依然保持多汁和嫩滑。

在製作過程中，牛小排會先經過仔細的挑選和處理，保證肉質的新鮮和優質。接著，將牛小排用特製的醃料醃制一段時間，這些醃料可能包含草本香料、大蒜、橄欖油等，旨在增強牛肉的風味。醃制過後的牛小排被放置在高溫的直火烤架上，經過專業的翻轉和控制，使每一面都均勻受熱，烤出完美的焦脆外皮和保持內部的嫩度。

此外，直火佛蘭肯式牛小排的獨到之處還在於其伴隨的醬汁和佐菜，常見的有紅酒醬汁、香草奶油醬或是簡單的海鹽和黑胡椒，這些配料不僅豐富了牛小排的層次，也讓整道菜的味道更加豐富多元。食用時，切片後的牛小排紋理清晰，肉汁豐富，每一口都能感受到牛肉的鮮美與烤制技巧的精妙，是一道適合在特殊場合享用的高級美食。', N'直火佛蘭肯式牛小排.png', N'商用料理配方')
GO
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (195, N'直火佛蘭肯式牛小排', N'材料品項、製作步驟、配方比例', 50000, N'
直火佛蘭肯式牛小排是一種採用直火燒烤方式精心製作的牛肉美食，這種烹調方法能夠充分展現牛肉的原始風味和獨特質感。佛蘭肯式的烹調手法源於傳統歐洲料理，特別強調食材的自然風味和烹飪技巧的純熟，使得牛小排在直火的烤制下，表皮呈現出誘人的金黃色澤，帶有微微的焦香，而內部肉質依然保持多汁和嫩滑。

在製作過程中，牛小排會先經過仔細的挑選和處理，保證肉質的新鮮和優質。接著，將牛小排用特製的醃料醃制一段時間，這些醃料可能包含草本香料、大蒜、橄欖油等，旨在增強牛肉的風味。醃制過後的牛小排被放置在高溫的直火烤架上，經過專業的翻轉和控制，使每一面都均勻受熱，烤出完美的焦脆外皮和保持內部的嫩度。

此外，直火佛蘭肯式牛小排的獨到之處還在於其伴隨的醬汁和佐菜，常見的有紅酒醬汁、香草奶油醬或是簡單的海鹽和黑胡椒，這些配料不僅豐富了牛小排的層次，也讓整道菜的味道更加豐富多元。食用時，切片後的牛小排紋理清晰，肉汁豐富，每一口都能感受到牛肉的鮮美與烤制技巧的精妙，是一道適合在特殊場合享用的高級美食。', N'直火佛蘭肯式牛小排.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (196, N'直火佛蘭肯式牛小排', N'500g/份，100份/件', 8000, N'
直火佛蘭肯式牛小排是一種採用直火燒烤方式精心製作的牛肉美食，這種烹調方法能夠充分展現牛肉的原始風味和獨特質感。佛蘭肯式的烹調手法源於傳統歐洲料理，特別強調食材的自然風味和烹飪技巧的純熟，使得牛小排在直火的烤制下，表皮呈現出誘人的金黃色澤，帶有微微的焦香，而內部肉質依然保持多汁和嫩滑。

在製作過程中，牛小排會先經過仔細的挑選和處理，保證肉質的新鮮和優質。接著，將牛小排用特製的醃料醃制一段時間，這些醃料可能包含草本香料、大蒜、橄欖油等，旨在增強牛肉的風味。醃制過後的牛小排被放置在高溫的直火烤架上，經過專業的翻轉和控制，使每一面都均勻受熱，烤出完美的焦脆外皮和保持內部的嫩度。

此外，直火佛蘭肯式牛小排的獨到之處還在於其伴隨的醬汁和佐菜，常見的有紅酒醬汁、香草奶油醬或是簡單的海鹽和黑胡椒，這些配料不僅豐富了牛小排的層次，也讓整道菜的味道更加豐富多元。食用時，切片後的牛小排紋理清晰，肉汁豐富，每一口都能感受到牛肉的鮮美與烤制技巧的精妙，是一道適合在特殊場合享用的高級美食。', N'直火佛蘭肯式牛小排.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (197, N'勃根地紅酒燉牛肉', N'材料品項', 3000, N'
勃根地紅酒燉牛肉，又稱為“布爾喬尼燉牛肉”，是一道源自法國勃根地地區的經典菜餚。這道佳餚以其豐富的口感和層次深受人們喜愛，主要將優質牛肉與勃根地產的紅酒以及各式香料和蔬菜慢燉而成，經過長時間的低溫烹煮，牛肉變得極其鮮嫩，幾乎可以用叉子輕輕撥開，而紅酒則賦予了菜品獨特的酸甜和香氣，使得整道菜的風味層次豐富，香氣撲鼻。

製作勃根地紅酒燉牛肉時，首先需要將牛肉塊煎至表面金黃，然後將其與洋蔥、胡蘿蔔、芹菜等傳統法式香料蔬菜以及大蒜和香草等調味料一同放入鍋中，加入足量的勃根地紅酒和少許牛肉高湯。接著，在低溫下慢慢燉煮數小時，使得牛肉的肉質柔軟至極，蔬菜和香料的味道也完全滲透到肉中，形成了一道口味豐富、香氣四溢的經典法國菜餚。', N'勃根地紅酒燉牛肉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (198, N'勃根地紅酒燉牛肉', N'材料品項、製作步驟', 12000, N'
勃根地紅酒燉牛肉，又稱為“布爾喬尼燉牛肉”，是一道源自法國勃根地地區的經典菜餚。這道佳餚以其豐富的口感和層次深受人們喜愛，主要將優質牛肉與勃根地產的紅酒以及各式香料和蔬菜慢燉而成，經過長時間的低溫烹煮，牛肉變得極其鮮嫩，幾乎可以用叉子輕輕撥開，而紅酒則賦予了菜品獨特的酸甜和香氣，使得整道菜的風味層次豐富，香氣撲鼻。

製作勃根地紅酒燉牛肉時，首先需要將牛肉塊煎至表面金黃，然後將其與洋蔥、胡蘿蔔、芹菜等傳統法式香料蔬菜以及大蒜和香草等調味料一同放入鍋中，加入足量的勃根地紅酒和少許牛肉高湯。接著，在低溫下慢慢燉煮數小時，使得牛肉的肉質柔軟至極，蔬菜和香料的味道也完全滲透到肉中，形成了一道口味豐富、香氣四溢的經典法國菜餚。', N'勃根地紅酒燉牛肉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (199, N'勃根地紅酒燉牛肉', N'材料品項、製作步驟、配方比例', 50000, N'
勃根地紅酒燉牛肉，又稱為“布爾喬尼燉牛肉”，是一道源自法國勃根地地區的經典菜餚。這道佳餚以其豐富的口感和層次深受人們喜愛，主要將優質牛肉與勃根地產的紅酒以及各式香料和蔬菜慢燉而成，經過長時間的低溫烹煮，牛肉變得極其鮮嫩，幾乎可以用叉子輕輕撥開，而紅酒則賦予了菜品獨特的酸甜和香氣，使得整道菜的風味層次豐富，香氣撲鼻。

製作勃根地紅酒燉牛肉時，首先需要將牛肉塊煎至表面金黃，然後將其與洋蔥、胡蘿蔔、芹菜等傳統法式香料蔬菜以及大蒜和香草等調味料一同放入鍋中，加入足量的勃根地紅酒和少許牛肉高湯。接著，在低溫下慢慢燉煮數小時，使得牛肉的肉質柔軟至極，蔬菜和香料的味道也完全滲透到肉中，形成了一道口味豐富、香氣四溢的經典法國菜餚。', N'勃根地紅酒燉牛肉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (200, N'勃根地紅酒燉牛肉', N'500g/份，100份/件', 8000, N'
勃根地紅酒燉牛肉，又稱為“布爾喬尼燉牛肉”，是一道源自法國勃根地地區的經典菜餚。這道佳餚以其豐富的口感和層次深受人們喜愛，主要將優質牛肉與勃根地產的紅酒以及各式香料和蔬菜慢燉而成，經過長時間的低溫烹煮，牛肉變得極其鮮嫩，幾乎可以用叉子輕輕撥開，而紅酒則賦予了菜品獨特的酸甜和香氣，使得整道菜的風味層次豐富，香氣撲鼻。

製作勃根地紅酒燉牛肉時，首先需要將牛肉塊煎至表面金黃，然後將其與洋蔥、胡蘿蔔、芹菜等傳統法式香料蔬菜以及大蒜和香草等調味料一同放入鍋中，加入足量的勃根地紅酒和少許牛肉高湯。接著，在低溫下慢慢燉煮數小時，使得牛肉的肉質柔軟至極，蔬菜和香料的味道也完全滲透到肉中，形成了一道口味豐富、香氣四溢的經典法國菜餚。', N'勃根地紅酒燉牛肉.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (201, N'馬告風味英式慢烤牛肉', N'材料品項', 3000, N'馬告風味英式慢烤牛肉是一道將傳統英式烹調方式與台灣原住民馬告（胡椒）的特色香料結合的創新美食。這道菜品融合了英國的經典慢烤牛肉製法和馬告獨有的辛香味，創造出一種全新的口感和風味。馬告，又稱山胡椒，因其強烈的香氣和微辣味而被廣泛用於調味，為這道菜增添了獨特的台灣原住民料理風味。

製作這道菜時，首先將牛肉塊表面塗抹上馬告粉末和其他香料（如迷迭香、蒜末和海鹽），進行初步醃製，讓肉質吸收香料的精華。接著，在預熱的烤箱中以低溫慢烤數小時，使牛肉緩慢受熱，保持肉汁的豐富和肉質的嫩滑。慢烤過程中，馬告的香味逐漸滲透到牛肉每一寸肌肉中，與牛肉的自然鮮美完美融合。

這道菜的特色在於馬告帶來的獨特辛香和微辣，與傳統英式烤牛肉的醇厚肉香形成絕妙對比，既保留了英式烤肉的經典鮮美，又增添了台灣原住民料理的特色風味。馬告風味英式慢烤牛肉，不僅是一道味蕾的盛宴，也是一次文化的交融，適合在各種聚會和特殊場合中展示烹飪藝術和創意。', N'馬告風味英式慢烤牛肉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (202, N'馬告風味英式慢烤牛肉', N'材料品項、製作步驟', 12000, N'馬告風味英式慢烤牛肉是一道將傳統英式烹調方式與台灣原住民馬告（胡椒）的特色香料結合的創新美食。這道菜品融合了英國的經典慢烤牛肉製法和馬告獨有的辛香味，創造出一種全新的口感和風味。馬告，又稱山胡椒，因其強烈的香氣和微辣味而被廣泛用於調味，為這道菜增添了獨特的台灣原住民料理風味。

製作這道菜時，首先將牛肉塊表面塗抹上馬告粉末和其他香料（如迷迭香、蒜末和海鹽），進行初步醃製，讓肉質吸收香料的精華。接著，在預熱的烤箱中以低溫慢烤數小時，使牛肉緩慢受熱，保持肉汁的豐富和肉質的嫩滑。慢烤過程中，馬告的香味逐漸滲透到牛肉每一寸肌肉中，與牛肉的自然鮮美完美融合。

這道菜的特色在於馬告帶來的獨特辛香和微辣，與傳統英式烤牛肉的醇厚肉香形成絕妙對比，既保留了英式烤肉的經典鮮美，又增添了台灣原住民料理的特色風味。馬告風味英式慢烤牛肉，不僅是一道味蕾的盛宴，也是一次文化的交融，適合在各種聚會和特殊場合中展示烹飪藝術和創意。', N'馬告風味英式慢烤牛肉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (203, N'馬告風味英式慢烤牛肉', N'材料品項、製作步驟、配方比例', 50000, N'馬告風味英式慢烤牛肉是一道將傳統英式烹調方式與台灣原住民馬告（胡椒）的特色香料結合的創新美食。這道菜品融合了英國的經典慢烤牛肉製法和馬告獨有的辛香味，創造出一種全新的口感和風味。馬告，又稱山胡椒，因其強烈的香氣和微辣味而被廣泛用於調味，為這道菜增添了獨特的台灣原住民料理風味。

製作這道菜時，首先將牛肉塊表面塗抹上馬告粉末和其他香料（如迷迭香、蒜末和海鹽），進行初步醃製，讓肉質吸收香料的精華。接著，在預熱的烤箱中以低溫慢烤數小時，使牛肉緩慢受熱，保持肉汁的豐富和肉質的嫩滑。慢烤過程中，馬告的香味逐漸滲透到牛肉每一寸肌肉中，與牛肉的自然鮮美完美融合。

這道菜的特色在於馬告帶來的獨特辛香和微辣，與傳統英式烤牛肉的醇厚肉香形成絕妙對比，既保留了英式烤肉的經典鮮美，又增添了台灣原住民料理的特色風味。馬告風味英式慢烤牛肉，不僅是一道味蕾的盛宴，也是一次文化的交融，適合在各種聚會和特殊場合中展示烹飪藝術和創意。', N'馬告風味英式慢烤牛肉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (204, N'馬告風味英式慢烤牛肉', N'500g/份，100份/件', 8000, N'馬告風味英式慢烤牛肉是一道將傳統英式烹調方式與台灣原住民馬告（胡椒）的特色香料結合的創新美食。這道菜品融合了英國的經典慢烤牛肉製法和馬告獨有的辛香味，創造出一種全新的口感和風味。馬告，又稱山胡椒，因其強烈的香氣和微辣味而被廣泛用於調味，為這道菜增添了獨特的台灣原住民料理風味。

製作這道菜時，首先將牛肉塊表面塗抹上馬告粉末和其他香料（如迷迭香、蒜末和海鹽），進行初步醃製，讓肉質吸收香料的精華。接著，在預熱的烤箱中以低溫慢烤數小時，使牛肉緩慢受熱，保持肉汁的豐富和肉質的嫩滑。慢烤過程中，馬告的香味逐漸滲透到牛肉每一寸肌肉中，與牛肉的自然鮮美完美融合。

這道菜的特色在於馬告帶來的獨特辛香和微辣，與傳統英式烤牛肉的醇厚肉香形成絕妙對比，既保留了英式烤肉的經典鮮美，又增添了台灣原住民料理的特色風味。馬告風味英式慢烤牛肉，不僅是一道味蕾的盛宴，也是一次文化的交融，適合在各種聚會和特殊場合中展示烹飪藝術和創意。', N'馬告風味英式慢烤牛肉.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (205, N'麻婆豆腐', N'材料品項', 3000, N'麻婆豆腐是一道色香味俱全的四川傳統名菜，以其鮮辣麻味聞名於世。這道菜品的主要食材包括嫩豆腐、牛肉末（或豬肉末）、豆瓣醬和花椒等，通過精心烹製，將豆腐的柔嫩與肉末的鮮香完美結合，再加上特製的辣椒醬和花椒的麻香，形成了一道獨特的川菜經典。

製作麻婆豆腐時，首先需要將嫩豆腐切成小塊，輕輕煮過以保持其形狀和質地。接著，在鍋中加熱油，爆香蒜蓉、蔥段和豆瓣醬，加入肉末翻炒至變色，這一步是為了使肉末充分吸收調味料的風味。隨後，將預先煮好的豆腐塊加入鍋中，輕輕攪拌，使豆腐與肉末和調味料充分融合。最後，加入適量的水或高湯，小火燉煮，使湯汁濃稠，並在出鍋前撒上花椒粉和蔥花，增加麻香和視覺效果。', N'麻婆豆腐.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (206, N'麻婆豆腐', N'材料品項、製作步驟', 12000, N'麻婆豆腐是一道色香味俱全的四川傳統名菜，以其鮮辣麻味聞名於世。這道菜品的主要食材包括嫩豆腐、牛肉末（或豬肉末）、豆瓣醬和花椒等，通過精心烹製，將豆腐的柔嫩與肉末的鮮香完美結合，再加上特製的辣椒醬和花椒的麻香，形成了一道獨特的川菜經典。

製作麻婆豆腐時，首先需要將嫩豆腐切成小塊，輕輕煮過以保持其形狀和質地。接著，在鍋中加熱油，爆香蒜蓉、蔥段和豆瓣醬，加入肉末翻炒至變色，這一步是為了使肉末充分吸收調味料的風味。隨後，將預先煮好的豆腐塊加入鍋中，輕輕攪拌，使豆腐與肉末和調味料充分融合。最後，加入適量的水或高湯，小火燉煮，使湯汁濃稠，並在出鍋前撒上花椒粉和蔥花，增加麻香和視覺效果。', N'麻婆豆腐.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (207, N'麻婆豆腐', N'材料品項、製作步驟、配方比例', 50000, N'麻婆豆腐是一道色香味俱全的四川傳統名菜，以其鮮辣麻味聞名於世。這道菜品的主要食材包括嫩豆腐、牛肉末（或豬肉末）、豆瓣醬和花椒等，通過精心烹製，將豆腐的柔嫩與肉末的鮮香完美結合，再加上特製的辣椒醬和花椒的麻香，形成了一道獨特的川菜經典。

製作麻婆豆腐時，首先需要將嫩豆腐切成小塊，輕輕煮過以保持其形狀和質地。接著，在鍋中加熱油，爆香蒜蓉、蔥段和豆瓣醬，加入肉末翻炒至變色，這一步是為了使肉末充分吸收調味料的風味。隨後，將預先煮好的豆腐塊加入鍋中，輕輕攪拌，使豆腐與肉末和調味料充分融合。最後，加入適量的水或高湯，小火燉煮，使湯汁濃稠，並在出鍋前撒上花椒粉和蔥花，增加麻香和視覺效果。', N'麻婆豆腐.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (208, N'麻婆豆腐', N'500g/份，100份/件', 8000, N'麻婆豆腐是一道色香味俱全的四川傳統名菜，以其鮮辣麻味聞名於世。這道菜品的主要食材包括嫩豆腐、牛肉末（或豬肉末）、豆瓣醬和花椒等，通過精心烹製，將豆腐的柔嫩與肉末的鮮香完美結合，再加上特製的辣椒醬和花椒的麻香，形成了一道獨特的川菜經典。

製作麻婆豆腐時，首先需要將嫩豆腐切成小塊，輕輕煮過以保持其形狀和質地。接著，在鍋中加熱油，爆香蒜蓉、蔥段和豆瓣醬，加入肉末翻炒至變色，這一步是為了使肉末充分吸收調味料的風味。隨後，將預先煮好的豆腐塊加入鍋中，輕輕攪拌，使豆腐與肉末和調味料充分融合。最後，加入適量的水或高湯，小火燉煮，使湯汁濃稠，並在出鍋前撒上花椒粉和蔥花，增加麻香和視覺效果。', N'麻婆豆腐.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (209, N'凱薩沙拉', N'材料品項', 3000, N'
凱薩沙拉是一道源自墨西哥的經典沙拉，以新鮮的蔬菜、烤麵包塊和特製的凱撒醬調味而成。主要成分包括生菜、羅馬生菜、番茄、麵包塊、帕瑪森芝士和煙燻培根。首先，將生菜和羅馬生菜洗淨、瀝乾，撕成適口大小的片狀，然後將番茄切片。接著，將麵包切成小塊，撒上橄欖油和鹽，放入烤箱中烤至金黃酥脆。然後，將煙燻培根切成小塊，煎至酥脆。最後，將所有的食材放入一個大碗中，灑上帕瑪森芝士，加入特製的凱撒醬，輕輕拌勻即可。這道凱薩沙拉口感清新爽口，蔬菜的甜味與鹹香的培根和帕瑪森芝士相得益彰，而特製的凱撒醬則為整個沙拉增添了豐富的層次和風味。這道沙拉不僅可作為開胃菜或配菜，還可以搭配其他主菜一起享用，為餐桌增添一份美味和色彩。', N'凱薩沙拉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (211, N'凱薩沙拉', N'材料品項、製作步驟', 12000, N'
凱薩沙拉是一道源自墨西哥的經典沙拉，以新鮮的蔬菜、烤麵包塊和特製的凱撒醬調味而成。主要成分包括生菜、羅馬生菜、番茄、麵包塊、帕瑪森芝士和煙燻培根。首先，將生菜和羅馬生菜洗淨、瀝乾，撕成適口大小的片狀，然後將番茄切片。接著，將麵包切成小塊，撒上橄欖油和鹽，放入烤箱中烤至金黃酥脆。然後，將煙燻培根切成小塊，煎至酥脆。最後，將所有的食材放入一個大碗中，灑上帕瑪森芝士，加入特製的凱撒醬，輕輕拌勻即可。這道凱薩沙拉口感清新爽口，蔬菜的甜味與鹹香的培根和帕瑪森芝士相得益彰，而特製的凱撒醬則為整個沙拉增添了豐富的層次和風味。這道沙拉不僅可作為開胃菜或配菜，還可以搭配其他主菜一起享用，為餐桌增添一份美味和色彩。', N'凱薩沙拉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (213, N'凱薩沙拉', N'材料品項、製作步驟、配方比例', 50000, N'
凱薩沙拉是一道源自墨西哥的經典沙拉，以新鮮的蔬菜、烤麵包塊和特製的凱撒醬調味而成。主要成分包括生菜、羅馬生菜、番茄、麵包塊、帕瑪森芝士和煙燻培根。首先，將生菜和羅馬生菜洗淨、瀝乾，撕成適口大小的片狀，然後將番茄切片。接著，將麵包切成小塊，撒上橄欖油和鹽，放入烤箱中烤至金黃酥脆。然後，將煙燻培根切成小塊，煎至酥脆。最後，將所有的食材放入一個大碗中，灑上帕瑪森芝士，加入特製的凱撒醬，輕輕拌勻即可。這道凱薩沙拉口感清新爽口，蔬菜的甜味與鹹香的培根和帕瑪森芝士相得益彰，而特製的凱撒醬則為整個沙拉增添了豐富的層次和風味。這道沙拉不僅可作為開胃菜或配菜，還可以搭配其他主菜一起享用，為餐桌增添一份美味和色彩。', N'凱薩沙拉.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (215, N'凱薩沙拉', N'500g/份，100份/件', 8000, N'
凱薩沙拉是一道源自墨西哥的經典沙拉，以新鮮的蔬菜、烤麵包塊和特製的凱撒醬調味而成。主要成分包括生菜、羅馬生菜、番茄、麵包塊、帕瑪森芝士和煙燻培根。首先，將生菜和羅馬生菜洗淨、瀝乾，撕成適口大小的片狀，然後將番茄切片。接著，將麵包切成小塊，撒上橄欖油和鹽，放入烤箱中烤至金黃酥脆。然後，將煙燻培根切成小塊，煎至酥脆。最後，將所有的食材放入一個大碗中，灑上帕瑪森芝士，加入特製的凱撒醬，輕輕拌勻即可。這道凱薩沙拉口感清新爽口，蔬菜的甜味與鹹香的培根和帕瑪森芝士相得益彰，而特製的凱撒醬則為整個沙拉增添了豐富的層次和風味。這道沙拉不僅可作為開胃菜或配菜，還可以搭配其他主菜一起享用，為餐桌增添一份美味和色彩。', N'凱薩沙拉.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (296, N'塔香炒蛤蜊', N'材料品項', 3000, N'
塔香炒蛤蜊是一道具有濃郁香料風味的中式海鮮菜品，以新鮮的蛤蜊為主料，加入特製的香料和調味料調製而成。首先，將新鮮的蛤蜊洗淨，去除殼上的泥沙，備用。接著，準備好蔥、蒜、姜等香料，切成末。在熱鍋中加入適量的食用油，爆香蔥、蒜和姜末。然後，將蛤蜊加入鍋中翻炒片刻，待蛤蜊開口後加入料酒和水，繼續烹煮片刻至蛤蜊全部打開。最後，加入特製的塔香醬料，如豆瓣醬、辣椒醬等，調味均勻。炒勻後即可起鍋，撒上香菜點綴即可食用。這道菜品風味獨特，香辣鮮美，蛤蜊的鮮甜與香料的香氣完美融合，令人垂涎欲滴。塔香炒蛤蜊不僅可作為家常菜品，也是中式餐廳的招牌菜之一，深受廣大食客的喜愛。', N'塔香炒蛤蜊.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (297, N'塔香炒蛤蜊', N'材料品項、製作步驟', 12000, N'
塔香炒蛤蜊是一道具有濃郁香料風味的中式海鮮菜品，以新鮮的蛤蜊為主料，加入特製的香料和調味料調製而成。首先，將新鮮的蛤蜊洗淨，去除殼上的泥沙，備用。接著，準備好蔥、蒜、姜等香料，切成末。在熱鍋中加入適量的食用油，爆香蔥、蒜和姜末。然後，將蛤蜊加入鍋中翻炒片刻，待蛤蜊開口後加入料酒和水，繼續烹煮片刻至蛤蜊全部打開。最後，加入特製的塔香醬料，如豆瓣醬、辣椒醬等，調味均勻。炒勻後即可起鍋，撒上香菜點綴即可食用。這道菜品風味獨特，香辣鮮美，蛤蜊的鮮甜與香料的香氣完美融合，令人垂涎欲滴。塔香炒蛤蜊不僅可作為家常菜品，也是中式餐廳的招牌菜之一，深受廣大食客的喜愛。', N'塔香炒蛤蜊.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (298, N'塔香炒蛤蜊', N'材料品項、製作步驟、配方比例', 50000, N'
塔香炒蛤蜊是一道具有濃郁香料風味的中式海鮮菜品，以新鮮的蛤蜊為主料，加入特製的香料和調味料調製而成。首先，將新鮮的蛤蜊洗淨，去除殼上的泥沙，備用。接著，準備好蔥、蒜、姜等香料，切成末。在熱鍋中加入適量的食用油，爆香蔥、蒜和姜末。然後，將蛤蜊加入鍋中翻炒片刻，待蛤蜊開口後加入料酒和水，繼續烹煮片刻至蛤蜊全部打開。最後，加入特製的塔香醬料，如豆瓣醬、辣椒醬等，調味均勻。炒勻後即可起鍋，撒上香菜點綴即可食用。這道菜品風味獨特，香辣鮮美，蛤蜊的鮮甜與香料的香氣完美融合，令人垂涎欲滴。塔香炒蛤蜊不僅可作為家常菜品，也是中式餐廳的招牌菜之一，深受廣大食客的喜愛。', N'塔香炒蛤蜊.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (299, N'塔香炒蛤蜊', N'500g/份，100份/件', 8000, N'
塔香炒蛤蜊是一道具有濃郁香料風味的中式海鮮菜品，以新鮮的蛤蜊為主料，加入特製的香料和調味料調製而成。首先，將新鮮的蛤蜊洗淨，去除殼上的泥沙，備用。接著，準備好蔥、蒜、姜等香料，切成末。在熱鍋中加入適量的食用油，爆香蔥、蒜和姜末。然後，將蛤蜊加入鍋中翻炒片刻，待蛤蜊開口後加入料酒和水，繼續烹煮片刻至蛤蜊全部打開。最後，加入特製的塔香醬料，如豆瓣醬、辣椒醬等，調味均勻。炒勻後即可起鍋，撒上香菜點綴即可食用。這道菜品風味獨特，香辣鮮美，蛤蜊的鮮甜與香料的香氣完美融合，令人垂涎欲滴。塔香炒蛤蜊不僅可作為家常菜品，也是中式餐廳的招牌菜之一，深受廣大食客的喜愛。', N'塔香炒蛤蜊.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (300, N'遊牧民蒜奶湯', N'材料品項', 3000, N'遊牧民蒜奶湯是一道源自遊牧民族傳統的營養豐富的湯品，以大蒜和鮮奶為主要食材，結合了濃厚的蒜味和奶香。首先，將新鮮的大蒜去皮，切碎或切片備用。接著，在鍋中加入適量的水，放入切碎的大蒜，煮至蒜味釋放出來。然後，將鮮奶倒入鍋中，調至中小火，繼續煮至湯汁變得濃稠。在烹煮過程中可以根據個人口味添加適量的鹽和胡椒調味。最後，將湯倒入碗中，撒上一些香菜或蔥花點綴，即可食用。這道湯品口感滑嫩，湯汁濃郁，蒜味濃郁而不過於刺鼻，奶香濃厚而柔和。遊牧民蒜奶湯不僅口感豐富，而且營養豐富，含有豐富的蛋白質和維生素，有助於增強身體免疫力，是冬天暖身的絕佳選擇。', N'遊牧民蒜奶湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (301, N'遊牧民蒜奶湯', N'材料品項、製作步驟', 12000, N'遊牧民蒜奶湯是一道源自遊牧民族傳統的營養豐富的湯品，以大蒜和鮮奶為主要食材，結合了濃厚的蒜味和奶香。首先，將新鮮的大蒜去皮，切碎或切片備用。接著，在鍋中加入適量的水，放入切碎的大蒜，煮至蒜味釋放出來。然後，將鮮奶倒入鍋中，調至中小火，繼續煮至湯汁變得濃稠。在烹煮過程中可以根據個人口味添加適量的鹽和胡椒調味。最後，將湯倒入碗中，撒上一些香菜或蔥花點綴，即可食用。這道湯品口感滑嫩，湯汁濃郁，蒜味濃郁而不過於刺鼻，奶香濃厚而柔和。遊牧民蒜奶湯不僅口感豐富，而且營養豐富，含有豐富的蛋白質和維生素，有助於增強身體免疫力，是冬天暖身的絕佳選擇。', N'遊牧民蒜奶湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (302, N'遊牧民蒜奶湯', N'材料品項、製作步驟、配方比例', 50000, N'遊牧民蒜奶湯是一道源自遊牧民族傳統的營養豐富的湯品，以大蒜和鮮奶為主要食材，結合了濃厚的蒜味和奶香。首先，將新鮮的大蒜去皮，切碎或切片備用。接著，在鍋中加入適量的水，放入切碎的大蒜，煮至蒜味釋放出來。然後，將鮮奶倒入鍋中，調至中小火，繼續煮至湯汁變得濃稠。在烹煮過程中可以根據個人口味添加適量的鹽和胡椒調味。最後，將湯倒入碗中，撒上一些香菜或蔥花點綴，即可食用。這道湯品口感滑嫩，湯汁濃郁，蒜味濃郁而不過於刺鼻，奶香濃厚而柔和。遊牧民蒜奶湯不僅口感豐富，而且營養豐富，含有豐富的蛋白質和維生素，有助於增強身體免疫力，是冬天暖身的絕佳選擇。', N'遊牧民蒜奶湯.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (303, N'遊牧民蒜奶湯', N'500g/份，100份/件', 8000, N'遊牧民蒜奶湯是一道源自遊牧民族傳統的營養豐富的湯品，以大蒜和鮮奶為主要食材，結合了濃厚的蒜味和奶香。首先，將新鮮的大蒜去皮，切碎或切片備用。接著，在鍋中加入適量的水，放入切碎的大蒜，煮至蒜味釋放出來。然後，將鮮奶倒入鍋中，調至中小火，繼續煮至湯汁變得濃稠。在烹煮過程中可以根據個人口味添加適量的鹽和胡椒調味。最後，將湯倒入碗中，撒上一些香菜或蔥花點綴，即可食用。這道湯品口感滑嫩，湯汁濃郁，蒜味濃郁而不過於刺鼻，奶香濃厚而柔和。遊牧民蒜奶湯不僅口感豐富，而且營養豐富，含有豐富的蛋白質和維生素，有助於增強身體免疫力，是冬天暖身的絕佳選擇。', N'遊牧民蒜奶湯.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (304, N'蔗香燻雞', N'材料品項', 3000, N'蔗香燻雞是一道結合了蔗糖和燻製風味的美味家常菜品。首先，將新鮮的雞肉清洗乾淨，去除多餘的皮毛和內臟，備用。接著，準備蔗糖和香料，將蔗糖磨成粉末或切成片狀，並將其均勻地塗抹在雞肉表面。然後，將雞肉放入燻籠中，加入適量的木炭，燒至微煙狀態，進行燻製。燻製的時間可以根據雞肉的大小和厚度進行調整，通常需要約1至2個小時。在燻製過程中，木炭的煙燻會使得雞肉吸收到煙燻的香味，同時蔗糖的甜味也會融入雞肉中，增添了特殊的風味。最後，將燻好的雞肉取出，切片或撕成小塊，即可享用。這道菜品口感鮮嫩多汁，具有獨特的蔗糖和燻製的香氣，香甜可口。蔗香燻雞不僅適合作為主菜，還可以搭配米飯、麵食或沙拉一起食用，營養豐富，深受人們喜愛。', N'蔗香燻雞.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (305, N'蔗香燻雞', N'材料品項、製作步驟', 12000, N'蔗香燻雞是一道結合了蔗糖和燻製風味的美味家常菜品。首先，將新鮮的雞肉清洗乾淨，去除多餘的皮毛和內臟，備用。接著，準備蔗糖和香料，將蔗糖磨成粉末或切成片狀，並將其均勻地塗抹在雞肉表面。然後，將雞肉放入燻籠中，加入適量的木炭，燒至微煙狀態，進行燻製。燻製的時間可以根據雞肉的大小和厚度進行調整，通常需要約1至2個小時。在燻製過程中，木炭的煙燻會使得雞肉吸收到煙燻的香味，同時蔗糖的甜味也會融入雞肉中，增添了特殊的風味。最後，將燻好的雞肉取出，切片或撕成小塊，即可享用。這道菜品口感鮮嫩多汁，具有獨特的蔗糖和燻製的香氣，香甜可口。蔗香燻雞不僅適合作為主菜，還可以搭配米飯、麵食或沙拉一起食用，營養豐富，深受人們喜愛。', N'蔗香燻雞.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (306, N'蔗香燻雞', N'材料品項、製作步驟、配方比例', 50000, N'蔗香燻雞是一道結合了蔗糖和燻製風味的美味家常菜品。首先，將新鮮的雞肉清洗乾淨，去除多餘的皮毛和內臟，備用。接著，準備蔗糖和香料，將蔗糖磨成粉末或切成片狀，並將其均勻地塗抹在雞肉表面。然後，將雞肉放入燻籠中，加入適量的木炭，燒至微煙狀態，進行燻製。燻製的時間可以根據雞肉的大小和厚度進行調整，通常需要約1至2個小時。在燻製過程中，木炭的煙燻會使得雞肉吸收到煙燻的香味，同時蔗糖的甜味也會融入雞肉中，增添了特殊的風味。最後，將燻好的雞肉取出，切片或撕成小塊，即可享用。這道菜品口感鮮嫩多汁，具有獨特的蔗糖和燻製的香氣，香甜可口。蔗香燻雞不僅適合作為主菜，還可以搭配米飯、麵食或沙拉一起食用，營養豐富，深受人們喜愛。', N'蔗香燻雞.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (307, N'蔗香燻雞', N'500g/份，100份/件', 8000, N'蔗香燻雞是一道結合了蔗糖和燻製風味的美味家常菜品。首先，將新鮮的雞肉清洗乾淨，去除多餘的皮毛和內臟，備用。接著，準備蔗糖和香料，將蔗糖磨成粉末或切成片狀，並將其均勻地塗抹在雞肉表面。然後，將雞肉放入燻籠中，加入適量的木炭，燒至微煙狀態，進行燻製。燻製的時間可以根據雞肉的大小和厚度進行調整，通常需要約1至2個小時。在燻製過程中，木炭的煙燻會使得雞肉吸收到煙燻的香味，同時蔗糖的甜味也會融入雞肉中，增添了特殊的風味。最後，將燻好的雞肉取出，切片或撕成小塊，即可享用。這道菜品口感鮮嫩多汁，具有獨特的蔗糖和燻製的香氣，香甜可口。蔗香燻雞不僅適合作為主菜，還可以搭配米飯、麵食或沙拉一起食用，營養豐富，深受人們喜愛。', N'蔗香燻雞.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (308, N'橙汁排骨', N'材料品項', 3000, N'
橙汁排骨是一道美味的家常菜品，融合了鮮美的豬排骨和橙汁的酸甜風味，口感豐富，簡單易做。首先，將豬排骨洗淨，切成適當大小的塊狀，備用。接著，準備新鮮的橙子，擠取橙汁，去除籽，並加入一些橙皮屑以增添橙味。然後，在平底鍋中加熱適量的油，將豬排骨放入鍋中煎至金黃。煎熟後，倒入橙汁，加入適量的醬油、糖和生抽，調味均勻。接著，蓋上鍋蓋，小火燜煮約30分鐘至肉熟且入味。最後，根據個人口味加入鹽、胡椒粉調味，再煮至橙汁收濃稠即可。這道菜品口感酸甜爽口，肉質鮮嫩多汁，豬排骨吸收了橙汁的風味，香氣四溢。橙汁排骨不僅營養豐富，而且易於消化，是老少皆宜的美味佳餚。可搭配白飯或麵食食用，也可作為家庭聚餐或節日宴席的主菜，深受人們喜愛。', N'橙汁排骨.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (309, N'橙汁排骨', N'材料品項、製作步驟', 12000, N'
橙汁排骨是一道美味的家常菜品，融合了鮮美的豬排骨和橙汁的酸甜風味，口感豐富，簡單易做。首先，將豬排骨洗淨，切成適當大小的塊狀，備用。接著，準備新鮮的橙子，擠取橙汁，去除籽，並加入一些橙皮屑以增添橙味。然後，在平底鍋中加熱適量的油，將豬排骨放入鍋中煎至金黃。煎熟後，倒入橙汁，加入適量的醬油、糖和生抽，調味均勻。接著，蓋上鍋蓋，小火燜煮約30分鐘至肉熟且入味。最後，根據個人口味加入鹽、胡椒粉調味，再煮至橙汁收濃稠即可。這道菜品口感酸甜爽口，肉質鮮嫩多汁，豬排骨吸收了橙汁的風味，香氣四溢。橙汁排骨不僅營養豐富，而且易於消化，是老少皆宜的美味佳餚。可搭配白飯或麵食食用，也可作為家庭聚餐或節日宴席的主菜，深受人們喜愛。', N'橙汁排骨.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (310, N'橙汁排骨', N'材料品項、製作步驟、配方比例', 50000, N'
橙汁排骨是一道美味的家常菜品，融合了鮮美的豬排骨和橙汁的酸甜風味，口感豐富，簡單易做。首先，將豬排骨洗淨，切成適當大小的塊狀，備用。接著，準備新鮮的橙子，擠取橙汁，去除籽，並加入一些橙皮屑以增添橙味。然後，在平底鍋中加熱適量的油，將豬排骨放入鍋中煎至金黃。煎熟後，倒入橙汁，加入適量的醬油、糖和生抽，調味均勻。接著，蓋上鍋蓋，小火燜煮約30分鐘至肉熟且入味。最後，根據個人口味加入鹽、胡椒粉調味，再煮至橙汁收濃稠即可。這道菜品口感酸甜爽口，肉質鮮嫩多汁，豬排骨吸收了橙汁的風味，香氣四溢。橙汁排骨不僅營養豐富，而且易於消化，是老少皆宜的美味佳餚。可搭配白飯或麵食食用，也可作為家庭聚餐或節日宴席的主菜，深受人們喜愛。', N'橙汁排骨.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (311, N'橙汁排骨', N'500g/份，100份/件', 8000, N'
橙汁排骨是一道美味的家常菜品，融合了鮮美的豬排骨和橙汁的酸甜風味，口感豐富，簡單易做。首先，將豬排骨洗淨，切成適當大小的塊狀，備用。接著，準備新鮮的橙子，擠取橙汁，去除籽，並加入一些橙皮屑以增添橙味。然後，在平底鍋中加熱適量的油，將豬排骨放入鍋中煎至金黃。煎熟後，倒入橙汁，加入適量的醬油、糖和生抽，調味均勻。接著，蓋上鍋蓋，小火燜煮約30分鐘至肉熟且入味。最後，根據個人口味加入鹽、胡椒粉調味，再煮至橙汁收濃稠即可。這道菜品口感酸甜爽口，肉質鮮嫩多汁，豬排骨吸收了橙汁的風味，香氣四溢。橙汁排骨不僅營養豐富，而且易於消化，是老少皆宜的美味佳餚。可搭配白飯或麵食食用，也可作為家庭聚餐或節日宴席的主菜，深受人們喜愛。', N'橙汁排骨.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (312, N'鹽酥雞', N'材料品項', 3000, N'鹽酥雞是一道風靡台灣的美食，以其酥脆的外皮和嫩滑的內裡而聞名。首先，將雞肉切成適口大小的塊狀，並用鹽、胡椒、醬油和味精等調料醃製。接著，將醃好的雞肉裹上一層麵粉，再油炸至金黃酥脆。炸好的雞肉外酥內嫩，口感極佳。接著，將炸好的雞肉放入鍋中，加入炸過的九層塔和蒜末，炒至香氣四溢。最後，將炒好的雞肉撒上少許鹽和胡椒粉，均勻拌匀即可。這道菜品口感香脆，鹹香可口，外酥內嫩，深受人們的喜愛。鹽酥雞不僅可作為下酒菜，也是一道受歡迎的家常菜。無論是單吃，搭配啤酒或其他飲料，都能帶來美味的享受。這道美食不僅受到台灣人民的喜愛，也在國際上備受歡迎，成為了代表性的台灣美食之一。', N'鹽酥雞.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (313, N'鹽酥雞', N'材料品項、製作步驟', 12000, N'鹽酥雞是一道風靡台灣的美食，以其酥脆的外皮和嫩滑的內裡而聞名。首先，將雞肉切成適口大小的塊狀，並用鹽、胡椒、醬油和味精等調料醃製。接著，將醃好的雞肉裹上一層麵粉，再油炸至金黃酥脆。炸好的雞肉外酥內嫩，口感極佳。接著，將炸好的雞肉放入鍋中，加入炸過的九層塔和蒜末，炒至香氣四溢。最後，將炒好的雞肉撒上少許鹽和胡椒粉，均勻拌匀即可。這道菜品口感香脆，鹹香可口，外酥內嫩，深受人們的喜愛。鹽酥雞不僅可作為下酒菜，也是一道受歡迎的家常菜。無論是單吃，搭配啤酒或其他飲料，都能帶來美味的享受。這道美食不僅受到台灣人民的喜愛，也在國際上備受歡迎，成為了代表性的台灣美食之一。', N'鹽酥雞.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (314, N'鹽酥雞', N'材料品項、製作步驟、配方比例', 50000, N'鹽酥雞是一道風靡台灣的美食，以其酥脆的外皮和嫩滑的內裡而聞名。首先，將雞肉切成適口大小的塊狀，並用鹽、胡椒、醬油和味精等調料醃製。接著，將醃好的雞肉裹上一層麵粉，再油炸至金黃酥脆。炸好的雞肉外酥內嫩，口感極佳。接著，將炸好的雞肉放入鍋中，加入炸過的九層塔和蒜末，炒至香氣四溢。最後，將炒好的雞肉撒上少許鹽和胡椒粉，均勻拌匀即可。這道菜品口感香脆，鹹香可口，外酥內嫩，深受人們的喜愛。鹽酥雞不僅可作為下酒菜，也是一道受歡迎的家常菜。無論是單吃，搭配啤酒或其他飲料，都能帶來美味的享受。這道美食不僅受到台灣人民的喜愛，也在國際上備受歡迎，成為了代表性的台灣美食之一。', N'鹽酥雞.png', N'商用料理配方')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (315, N'鹽酥雞', N'500g/份，100份/件', 8000, N'鹽酥雞是一道風靡台灣的美食，以其酥脆的外皮和嫩滑的內裡而聞名。首先，將雞肉切成適口大小的塊狀，並用鹽、胡椒、醬油和味精等調料醃製。接著，將醃好的雞肉裹上一層麵粉，再油炸至金黃酥脆。炸好的雞肉外酥內嫩，口感極佳。接著，將炸好的雞肉放入鍋中，加入炸過的九層塔和蒜末，炒至香氣四溢。最後，將炒好的雞肉撒上少許鹽和胡椒粉，均勻拌匀即可。這道菜品口感香脆，鹹香可口，外酥內嫩，深受人們的喜愛。鹽酥雞不僅可作為下酒菜，也是一道受歡迎的家常菜。無論是單吃，搭配啤酒或其他飲料，都能帶來美味的享受。這道美食不僅受到台灣人民的喜愛，也在國際上備受歡迎，成為了代表性的台灣美食之一。', N'鹽酥雞.png', N'商用料理配料包')
INSERT [dbo].[products] ([pid], [pname], [pmode], [price], [pdesc], [pimage], [ptype]) VALUES (316, N'清蒸吉娃娃', N'大卸八塊', 87, N'仇恨值吸引器a吉娃娃', N'清蒸吉娃娃.png', N'廚餘')
SET IDENTITY_INSERT [dbo].[products] OFF
GO
SET IDENTITY_INSERT [dbo].[secondhand] ON 

INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (3, N'高雄市', N'九成新', N'面交', N'否', N'福榕商行', N'大理石流理臺', 5555, N'手機:0987487987', N'輕微使用痕跡，底下有洗碗機，大理石檯面已拋光', N'大理石流理臺.png', N'我要買', 9, CAST(N'2024-01-03' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (21, N'金門縣', N'近全新', N'面交', N'是', N'妙挖粽子', N'Rational蒸烤箱', 500000, N'手機號碼:0964656778', N'八字不合故售出', N'Rational蒸烤箱.png', N'我要賣', 9, CAST(N'2024-02-12' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (22, N'高雄市', N'六成新', N'超商店到店', N'可討論', N'八大壺', N'二口鼓風爐', 150000, N'電話0988966532', N'用到快故障了故售出', N'二口鼓風爐.png', N'我要賣', 9, CAST(N'2024-02-12' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (23, N'新竹縣', N'八成新', N'面交', N'否', N'超夢', N'上火烤箱', 15000, N'電話0987987666', N'功能皆正常，有正常使用痕跡。', N'上火烤箱.png', N'我要賣', 9, CAST(N'2024-02-12' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (24, N'臺北市', N'近全新', N'面交', N'是', N'胖釘', N'四口爐', 50000, N'Line : @456789abc', N'抽獎抽到，完全未使用', N'四口爐.png', N'我要賣', 9, CAST(N'2024-02-12' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (25, N'臺北市', N'近全新', N'面交', N'是', N'傘店鳥', N'恰克台', 12000, N'Email : zxc123@gmail.com', N'媽祖叫我賣掉', N'恰克台.png', N'我要賣', 9, CAST(N'2024-02-12' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (26, N'臺北市', N'七成新', N'面交', N'是', N'噴水龍', N'高湯爐', 12000, N'電話 : 0987987666', N'路邊撿到的用不到，功能皆正常', N'高湯爐.png', N'我要賣', 9, CAST(N'2024-02-12' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (27, N'高雄市', N'八成新', N'面交', N'是', N'螺旋丸', N'煎台', 5000, N'電話:0987555333', N'已清理完畢，功能皆正常', N'煎台.png', N'我要賣', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (28, N'臺中市', N'九成新', N'面交', N'否', N'大比鳥', N'落地式油炸機', 9999, N'電話:07-6422099', N'加熱速度快，蓄油量大', N'落地式油炸機.png', N'我要賣', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (29, N'金門縣', N'近全新', N'面交', N'是', N'可達鵝', N'蒸氣迴轉鍋', 45600, N'電話 : 0933285671', N'未提供', N'蒸氣迴轉鍋.png', N'我要賣', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (30, N'臺北市', N'近全新', N'面交', N'是', N'Hola Gee', N'二手神田鐵鍋', 1500, N'電話 0966512874', N'徵求二手神田鐵鍋，品項佳者優先', N'預設照片.png', N'我要買', 9, CAST(N'2024-02-13' AS Date))
INSERT [dbo].[secondhand] ([shid], [shrigion], [shnew], [shtradeway], [shnego], [shname], [shpname], [shprice], [shcontact], [shdesc], [shpimage], [shbuyorsale], [shcid], [shdate]) VALUES (31, N'臺南市', N'九成新', N'郵寄', N'否', N'暴鯉大蛇丸', N'微波爐', 99999, N'電話 0987987487', N'路邊撿到，沒有使用過', N'微波爐.png', N'我要賣', 9, CAST(N'2024-02-13' AS Date))
SET IDENTITY_INSERT [dbo].[secondhand] OFF
GO
ALTER TABLE [dbo].[persons] ADD  CONSTRAINT [DF_persons_權限]  DEFAULT ((1000)) FOR [權限]
GO
ALTER TABLE [dbo].[secondhand] ADD  CONSTRAINT [DF_secondhand_shpimage]  DEFAULT (N'預設照片.png') FOR [shpimage]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([tobuycid])
REFERENCES [dbo].[persons] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [FK__cart__tobuypid__4F7CD00D] FOREIGN KEY([tobuypid])
REFERENCES [dbo].[products] ([pid])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [FK__cart__tobuypid__4F7CD00D]
GO
ALTER TABLE [dbo].[expert]  WITH CHECK ADD  CONSTRAINT [FK_expert_persons] FOREIGN KEY([epcid])
REFERENCES [dbo].[persons] ([id])
GO
ALTER TABLE [dbo].[expert] CHECK CONSTRAINT [FK_expert_persons]
GO
ALTER TABLE [dbo].[ordercontain]  WITH CHECK ADD  CONSTRAINT [FK_ordercontain_orders] FOREIGN KEY([ocoid])
REFERENCES [dbo].[orders] ([oid])
GO
ALTER TABLE [dbo].[ordercontain] CHECK CONSTRAINT [FK_ordercontain_orders]
GO
ALTER TABLE [dbo].[ordercontain]  WITH CHECK ADD  CONSTRAINT [FK_ordercontain_products] FOREIGN KEY([ocpid])
REFERENCES [dbo].[products] ([pid])
GO
ALTER TABLE [dbo].[ordercontain] CHECK CONSTRAINT [FK_ordercontain_products]
GO
ALTER TABLE [dbo].[secondhand]  WITH CHECK ADD  CONSTRAINT [FK_secondhand_secondhand] FOREIGN KEY([shcid])
REFERENCES [dbo].[persons] ([id])
GO
ALTER TABLE [dbo].[secondhand] CHECK CONSTRAINT [FK_secondhand_secondhand]
GO
USE [master]
GO
ALTER DATABASE [chefless] SET  READ_WRITE 
GO
