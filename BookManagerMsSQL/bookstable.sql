USE [MYDB1]
GO

/****** Object:  Table [dbo].[BookTable]    Script Date: 2020-07-09 오전 11:31:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BookTable](
	[Isbn] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Publisher] [nvarchar](50) NOT NULL,
	[Page] [int] NOT NULL,
	[BorrowedAt] [date] NULL,
	[IsBorrowed] [bit] NOT NULL,
	[UserId] [int] NULL,
	[UserName] [nvarchar](7) NULL,
 CONSTRAINT [PK_BookTable] PRIMARY KEY CLUSTERED 
(
	[Isbn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

