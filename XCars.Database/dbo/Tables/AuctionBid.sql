CREATE TABLE [dbo].[AuctionBid] (
    [ID]          INT      IDENTITY (1, 1) NOT NULL,
    [AuctionID]   INT      NOT NULL,
    [UserID]      INT      NOT NULL,
    [DateCreated] DATETIME NOT NULL,
    [Amount]      INT      NOT NULL,
    CONSTRAINT [PK_AuctionBid] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AuctionBid_Auction] FOREIGN KEY ([AuctionID]) REFERENCES [dbo].[Auction] ([ID]),
    CONSTRAINT [FK_AuctionBid_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

