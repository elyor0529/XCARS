CREATE TABLE [dbo].[Auction] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [AutoID]          INT            NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [DateCreated]     DATETIME       NOT NULL,
    [StartPrice]      INT            NOT NULL,
    [CurrencyID]      INT            NOT NULL,
    [StateID]         INT            NOT NULL,
    [DateStopped]     DATETIME       NULL,
    [Deadline]        DATETIME       NOT NULL,
    [CompletionJobID] NVARCHAR (50)  NULL,
    [DeletionJobID]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Auction] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Auction_AuctionState] FOREIGN KEY ([StateID]) REFERENCES [dbo].[AuctionState] ([ID]),
    CONSTRAINT [FK_Auction_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_Auction_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([ID])
);

