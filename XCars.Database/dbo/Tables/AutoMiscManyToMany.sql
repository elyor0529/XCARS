CREATE TABLE [dbo].[AutoMiscManyToMany] (
    [AutoID]     INT NOT NULL,
    [AutoMiscID] INT NOT NULL,
    CONSTRAINT [PK_AutoMiscManyToMany] PRIMARY KEY CLUSTERED ([AutoID] ASC, [AutoMiscID] ASC),
    CONSTRAINT [FK_AutoMiscManyToMany_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoMiscManyToMany_AutoMisc] FOREIGN KEY ([AutoMiscID]) REFERENCES [dbo].[AutoMisc] ([ID])
);

