CREATE TABLE [dbo].[AutoBodyType] (
    [ID]              INT            NOT NULL,
    [TransportTypeID] INT            NOT NULL,
    [Name]            NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_AutoBodyType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AutoBodyType_AutoTransportType] FOREIGN KEY ([TransportTypeID]) REFERENCES [dbo].[AutoTransportType] ([ID])
);

