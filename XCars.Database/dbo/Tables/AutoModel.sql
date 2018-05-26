CREATE TABLE [dbo].[AutoModel] (
    [ID]      INT            NOT NULL,
    [MakeID]  INT            NOT NULL,
    [Name]    NVARCHAR (128) NOT NULL,
    [Name_ru] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_AutoModel] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AutoModel_AutoMake] FOREIGN KEY ([MakeID]) REFERENCES [dbo].[AutoMake] ([ID])
);

