CREATE TABLE [dbo].[Transaction] (
    [ID]                   INT             IDENTITY (1, 1) NOT NULL,
    [UserID]               INT             NOT NULL,
    [StateID]              INT             NOT NULL,
    [PurchaseTypeID]       INT             NULL,
    [Amount]               DECIMAL (18, 2) NOT NULL,
    [DateCreated]          DATETIME        NOT NULL,
    [Description]          NVARCHAR (MAX)  NULL,
    [ObjectID]             INT             NULL,
    [LMI_MODE]             INT             NOT NULL,
    [LMI_PAYMENT_SYSTEM]   NVARCHAR (256)  NULL,
    [LMI_PAYMENT_DESC]     NVARCHAR (MAX)  NULL,
    [LMI_SYS_PAYMENT_ID]   INT             NULL,
    [LMI_SYS_PAYMENT_DATE] DATETIME        NULL,
    [LMI_PAYER_IDENTIFIER] NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Popolnenie] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Transaction_PurchaseType] FOREIGN KEY ([PurchaseTypeID]) REFERENCES [dbo].[PurchaseType] ([ID]),
    CONSTRAINT [FK_Transaction_TransactionState] FOREIGN KEY ([StateID]) REFERENCES [dbo].[TransactionState] ([ID]),
    CONSTRAINT [FK_Transaction_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

