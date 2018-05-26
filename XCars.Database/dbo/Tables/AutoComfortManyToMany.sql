CREATE TABLE [dbo].[AutoComfortManyToMany] (
    [AutoID]        INT NOT NULL,
    [AutoComfortID] INT NOT NULL,
    CONSTRAINT [PK_AutoComfortManyToMany] PRIMARY KEY CLUSTERED ([AutoID] ASC, [AutoComfortID] ASC),
    CONSTRAINT [FK_AutoComfortManyToMany_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoComfortManyToMany_AutoComfort] FOREIGN KEY ([AutoComfortID]) REFERENCES [dbo].[AutoComfort] ([ID])
);

