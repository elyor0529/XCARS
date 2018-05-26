CREATE TABLE [dbo].[UserConnection] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [UserID]     INT           NOT NULL,
    [Connection] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserConnection] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserConnection_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

