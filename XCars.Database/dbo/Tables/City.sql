CREATE TABLE [dbo].[City] (
    [ID]       INT            NOT NULL,
    [RegionID] INT            NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_City_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID])
);

