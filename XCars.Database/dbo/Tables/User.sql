CREATE TABLE [dbo].[User] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [AspNetUserID]     NVARCHAR (128)  NOT NULL,
    [Email]            NVARCHAR (256)  NOT NULL,
    [DateRegistered]   DATETIME        NOT NULL,
    [Balance]          DECIMAL (18, 2) CONSTRAINT [DF_User_Balance] DEFAULT ((0)) NOT NULL,
    [AuctionAgreement] BIT             CONSTRAINT [DF_User_AuctionAgreement] DEFAULT ((0)) NOT NULL,
    [LastSeen]         DATETIME        NULL,
    [PhoneNumber]      NVARCHAR (50)   NULL,
    [Name]             NVARCHAR (128)  NULL,
    [RegionID]         INT             NULL,
    [CityID]           INT             NULL,
    [PhotoUrl]         NVARCHAR (512)  NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]),
    CONSTRAINT [FK_User_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID])
);

