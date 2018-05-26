CREATE TABLE [dbo].[AuctionPhoto] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [AuctionID] INT            NOT NULL,
    [Url]       NVARCHAR (512) NOT NULL,
    [IsMain]    BIT            NOT NULL,
    CONSTRAINT [PK_AuctionPhoto] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AuctionPhoto_Auction] FOREIGN KEY ([AuctionID]) REFERENCES [dbo].[Auction] ([ID])
);

