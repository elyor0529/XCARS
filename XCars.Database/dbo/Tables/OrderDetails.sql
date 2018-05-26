CREATE TABLE [dbo].[OrderDetails] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [OrderID] NVARCHAR (64)  NOT NULL,
    [Name]    NVARCHAR (50)  NOT NULL,
    [Value]   NVARCHAR (511) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([LMI_PAYMENT_NO])
);

