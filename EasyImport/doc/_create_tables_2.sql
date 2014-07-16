USE [les_mig]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[data_nl_FCProductGroup](

           [ProductGroupCode] [int] NULL,

           [ProductGroupName] [nvarchar](100) NULL,

           [StringID] [int] NULL

) ON [PRIMARY]

GO

CREATE TABLE [dbo].[data_nl_ProductGroupListPrice](

           [ID] [int] IDENTITY(1,1) NOT NULL,

           [ServiceProviderID] [int] NULL,

           [DeliverySPID] [int] NULL,

           [EffectiveDateUTC] [datetime] NULL

) ON [PRIMARY]

GO

CREATE TABLE [dbo].[data_nl_ProductGroupListPriceEntry](

           [ID] [int] IDENTITY(1,1) NOT NULL,

           [ProductGroupListPriceID] [int] NULL,

           [ProductGroupCode] [int] NULL,

           [Price] [nvarchar](255) NULL

) ON [PRIMARY]

GO

CREATE TABLE [dbo].[data_nl_ServiceProvider](

           [ID] [int] IDENTITY(1,1) NOT NULL,

           [EntityClassID] [int] NULL,

           [Name] [nvarchar](50) NULL,

           [Retired] [smallint] NULL,

           [AccountID] [int] NULL,

           [TimeZoneID] [int] NULL,

           [XMLProperties] [ntext] NULL,

           [OwnerProviderID] [int] NULL,

           [TaxAreaCode] [nvarchar](3) NULL,

           [SPGroupID] [int] NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[data_nl_Site](

           [ID] [int] IDENTITY(1,1) NOT NULL,

           [Name] [nvarchar](50) NULL,

           [ParentSiteID] [int] NULL,

           [Level] [smallint] NULL,

           [ServiceProviderID] [int] NULL,

           [EntityClassID] [int] NULL,

           [XMLProperties] [ntext] NULL,

           [AccountID] [int] NULL,

           [UseLocalAccount] [bit] NULL,

           [LocalAccountID] [bit] NULL,

           [TimeZoneID] [int] NULL,

           [CountryID] [int] NULL,

           [TradeProductPoolID] [int] NULL,

           [TaxAreaCode] [nvarchar](3) NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[data_nl_Site] ADD  DEFAULT ((0)) FOR [UseLocalAccount]

GO

ALTER TABLE [dbo].[data_nl_Site] ADD  DEFAULT ((0)) FOR [LocalAccountID]

GO

CREATE TABLE [dbo].[data_nl_SiteGroup](

           [ID] [int] IDENTITY(1,1) NOT NULL,

           [ServiceProviderID] [int] NULL,

           [Description] [nvarchar](64) NULL,

           [IsCustomSiteGroup] [bit] NULL,

           [SiteGroupCapabilities] [smallint] NULL

) ON [PRIMARY]

GO

ALTER TABLE [dbo].[data_nl_SiteGroup] ADD  DEFAULT ((0)) FOR
[IsCustomSiteGroup]

GO

CREATE TABLE [dbo].[data_nl_SiteGroupMembership](

           [SiteGroupID] [int] NULL,

           [SiteID] [int] NULL,

           [IsBaseSite] [bit] NULL

) ON [PRIMARY]

GO

ALTER TABLE [dbo].[data_nl_SiteGroupMembership] ADD  DEFAULT ((0)) FOR
[IsBaseSite]

GO

CREATE TABLE [dbo].[data_nl_ThirdPartyLocationMappings](

           [DeviceID] [int] NULL,

           [SourceCardAcceptorTerminalID] [nvarchar](8) NULL,

           [SourceCardAcceptorIDCode] [nvarchar](15) NULL,

           [TargetCardAcceptorTerminalID] [nvarchar](8) NULL,

           [TargetCardAcceptorIDCode] [nvarchar](15) NULL,

           [ThirdPartyOrgID] [int] NULL

) ON [PRIMARY]

GO

CREATE TABLE [dbo].[data_nl_TradeProduct](

           [TradeProductID] [nvarchar](255) NULL,

           [ProductCode] [nvarchar](255) NULL,

           [ProductGroupCode] [int] NULL,

           [TradeProductPoolID] [int] NULL,

           [TARICCode] [nvarchar](14) NULL

) ON [PRIMARY]

GO