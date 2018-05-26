CREATE TABLE [dbo].[AutoMultimediaManyToMany] (
    [AutoID]           INT NOT NULL,
    [AutoMultimediaID] INT NOT NULL,
    CONSTRAINT [PK_AutoMultimediaManyToMany] PRIMARY KEY CLUSTERED ([AutoID] ASC, [AutoMultimediaID] ASC),
    CONSTRAINT [FK_AutoMultimediaManyToMany_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoMultimediaManyToMany_AutoMultimedia] FOREIGN KEY ([AutoMultimediaID]) REFERENCES [dbo].[AutoMultimedia] ([ID])
);

