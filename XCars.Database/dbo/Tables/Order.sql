CREATE TABLE [dbo].[Order] (
    [LMI_PAYMENT_NO]       NVARCHAR (64)   NOT NULL,
    [LMI_PREREQUEST]       INT             NULL,
    [LMI_MERCHANT_ID]      INT             NOT NULL,
    [LMI_PAYMENT_AMOUNT]   DECIMAL (18, 2) NOT NULL,
    [LMI_MODE]             INT             NOT NULL,
    [LMI_SYS_PAYMENT_DATE] DATETIME        NULL,
    [LMI_HASH]             NVARCHAR (511)  NULL,
    [UserID]               INT             NOT NULL,
    [DateCreated]          DATETIME        NOT NULL,
    [PurchaseTypeID]       INT             NULL,
    [ObjectID]             INT             NULL,
    [IsOpen]               BIT             NOT NULL,
    [UsedFromBalance]      DECIMAL (18, 2) CONSTRAINT [DF_Order_UsedFromBalance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([LMI_PAYMENT_NO] ASC),
    CONSTRAINT [FK_Order_PurchaseType] FOREIGN KEY ([PurchaseTypeID]) REFERENCES [dbo].[PurchaseType] ([ID]),
    CONSTRAINT [FK_Order_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

