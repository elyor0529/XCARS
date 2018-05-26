CREATE TABLE [dbo].[AuctionConnection] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [AuctionID]  INT           NOT NULL,
    [UserID]     INT           NULL,
    [Connection] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuctionConnection] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AuctionConnection_Auction] FOREIGN KEY ([AuctionID]) REFERENCES [dbo].[Auction] ([ID]),
    CONSTRAINT [FK_AuctionConnection_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

