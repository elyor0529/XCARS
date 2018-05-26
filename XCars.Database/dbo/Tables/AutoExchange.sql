CREATE TABLE [dbo].[AutoExchange] (
    [ID]                 INT      IDENTITY (1, 1) NOT NULL,
    [TargetAutoID]       INT      NOT NULL,
    [OfferedAutoID]      INT      NOT NULL,
    [DateCreated]        DATETIME NOT NULL,
    [DiffPrice]          INT      NULL,
    [CurrencyID]         INT      NOT NULL,
    [DiffPriceDirection] INT      NULL,
    CONSTRAINT [PK_AutoExchange] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AutoExchange_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([ID]),
    CONSTRAINT [FK_AutoExchange_OfferedAuto] FOREIGN KEY ([OfferedAutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_AutoExchange_TargetAuto] FOREIGN KEY ([TargetAutoID]) REFERENCES [dbo].[Auto] ([ID])
);

