CREATE TABLE [dbo].[Currency] (
    [ID]     INT           NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    [Symbol] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([ID] ASC)
);

