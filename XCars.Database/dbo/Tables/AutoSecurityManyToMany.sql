CREATE TABLE [dbo].[AutoSecurityManyToMany] (
    [AutoID]         INT NOT NULL,
    [AutoSecurityID] INT NOT NULL,
    CONSTRAINT [PK_AutoSecurityManyToMany] PRIMARY KEY CLUSTERED ([AutoID] ASC, [AutoSecurityID] ASC),
    CONSTRAINT [FK_AutoSecurityManyToMany_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoSecurityManyToMany_AutoSecurity] FOREIGN KEY ([AutoSecurityID]) REFERENCES [dbo].[AutoSecurity] ([ID])
);

