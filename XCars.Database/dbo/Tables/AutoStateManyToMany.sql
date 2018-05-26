CREATE TABLE [dbo].[AutoStateManyToMany] (
    [AutoID]      INT NOT NULL,
    [AutoStateID] INT NOT NULL,
    CONSTRAINT [PK_AutoStateManyToMany] PRIMARY KEY CLUSTERED ([AutoID] ASC, [AutoStateID] ASC),
    CONSTRAINT [FK_AutoStateManyToMany_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoStateManyToMany_AutoState] FOREIGN KEY ([AutoStateID]) REFERENCES [dbo].[AutoState] ([ID])
);

