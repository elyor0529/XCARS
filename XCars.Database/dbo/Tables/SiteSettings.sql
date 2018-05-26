CREATE TABLE [dbo].[SiteSettings] (
    [ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Keyy]  NVARCHAR (256) NOT NULL,
    [Value] NVARCHAR (511) NOT NULL,
    CONSTRAINT [PK_SiteSettings] PRIMARY KEY CLUSTERED ([ID] ASC)
);

