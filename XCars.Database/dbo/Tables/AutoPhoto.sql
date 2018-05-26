CREATE TABLE [dbo].[AutoPhoto] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [AutoID] INT            NOT NULL,
    [Url]    NVARCHAR (512) NOT NULL,
    [IsMain] BIT            NOT NULL,
    CONSTRAINT [PK_AutoImg] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AutoPhoto_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID])
);

