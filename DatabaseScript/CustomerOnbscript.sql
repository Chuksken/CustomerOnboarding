USE [CustomerOnboardDB]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 3/28/2022 1:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[Password] [nvarchar](500) NULL,
	[LgaID] [int] NOT NULL,
	[StateID] [int] NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LGA]    Script Date: 3/28/2022 1:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LGA](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[StateID] [int] NOT NULL,
 CONSTRAINT [PK_LGA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 3/28/2022 1:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenTable]    Script Date: 3/28/2022 1:15:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[IsUsed] [bit] NULL,
	[TokenGeneratedTime] [datetime] NULL,
	[TokenExpiration] [datetime] NULL,
 CONSTRAINT [PK_TokenTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [Email], [PhoneNo], [CreatedDate], [Password], [LgaID], [StateID], [IsActive]) VALUES (1, N'chuks@gmail.com', N'07031122227', CAST(N'2022-03-27T23:11:58.743' AS DateTime), N'V02zy9YLAaJxk3au/LqxGw==', 1, 1, NULL)
INSERT [dbo].[Customer] ([Id], [Email], [PhoneNo], [CreatedDate], [Password], [LgaID], [StateID], [IsActive]) VALUES (2, N'Tomi', N'08131111111', CAST(N'2022-03-28T07:19:09.680' AS DateTime), N'V02zy9YLAaJxk3au/LqxGw==', 2, 2, 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[LGA] ON 

INSERT [dbo].[LGA] ([Id], [Name], [Created], [StateID]) VALUES (1, N'Nsukka', CAST(N'2022-03-27T23:10:18.143' AS DateTime), 1)
INSERT [dbo].[LGA] ([Id], [Name], [Created], [StateID]) VALUES (2, N'MainLand', CAST(N'2022-03-28T07:18:01.550' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[LGA] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 

INSERT [dbo].[State] ([Id], [Name], [CreatedDate]) VALUES (1, N'Enugu', CAST(N'2022-03-27T23:06:13.710' AS DateTime))
INSERT [dbo].[State] ([Id], [Name], [CreatedDate]) VALUES (2, N'Lagos', CAST(N'2022-03-28T07:16:22.507' AS DateTime))
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[TokenTable] ON 

INSERT [dbo].[TokenTable] ([Id], [Token], [PhoneNo], [IsUsed], [TokenGeneratedTime], [TokenExpiration]) VALUES (1, N'778075', N'08131111111', 0, CAST(N'2022-03-28T07:19:09.763' AS DateTime), CAST(N'2022-03-28T07:49:09.763' AS DateTime))
SET IDENTITY_INSERT [dbo].[TokenTable] OFF
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_LGA] FOREIGN KEY([LgaID])
REFERENCES [dbo].[LGA] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_LGA]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_State]
GO
ALTER TABLE [dbo].[LGA]  WITH CHECK ADD  CONSTRAINT [FK_LGA_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[LGA] CHECK CONSTRAINT [FK_LGA_State]
GO
