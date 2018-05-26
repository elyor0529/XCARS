CREATE TABLE [dbo].[AutoColor] (
    [ID]   INT            NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    [Hex]  NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_AutoColor] PRIMARY KEY CLUSTERED ([ID] ASC)
);

