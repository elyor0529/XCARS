CREATE TABLE [dbo].[ContactsRequest] (
    [ID]     INT IDENTITY (1, 1) NOT NULL,
    [UserID] INT NOT NULL,
    [AutoID] INT NULL,
    CONSTRAINT [PK_ContactsRequest] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ContactsRequest_Auto] FOREIGN KEY ([AutoID]) REFERENCES [dbo].[Auto] ([ID]),
    CONSTRAINT [FK_ContactsRequest_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

