USE [db_equip]
GO

/****** Object:  Table [dbo].[t_Equip]    Script Date: 03/08/2014 16:40:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_Equip](
	[EquipID] [varchar](40) NULL,
	[EquipName] [varchar](50) NULL,
	[EquipState] [varchar](20) NULL,
	[PosX] [numeric](18, 6) NULL,
	[PosY] [numeric](18, 6) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

