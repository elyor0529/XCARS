CREATE TABLE [dbo].[AutoFavorite] (
    [ID]     INT IDENTITY (1, 1) NOT NULL,
    [AutoID] INT NOT NULL,
    [UserID] INT NOT NULL,
    CONSTRAINT [PK_AutoFavorite] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AutoFavorite_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoFavorite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

