CREATE TABLE [dbo].[ScheduledEmail] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [DateScheduled] DATETIME       NOT NULL,
    [DateDue]       DATETIME       NOT NULL,
    [StatusID]      INT            NOT NULL,
    [To]            NVARCHAR (256) NOT NULL,
    [Subject]       NVARCHAR (511) NOT NULL,
    [Body]          NVARCHAR (MAX) NOT NULL,
    [ObjectTypeID]  INT            CONSTRAINT [DF_ScheduledEmail_ObjectTypeID] DEFAULT ((1)) NOT NULL,
    [ObjectID]      INT            NULL,
    [DateEligible]  DATETIME       NOT NULL,
    CONSTRAINT [PK_ScheduledEmail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ScheduledEmail_ScheduledEmailObjectType] FOREIGN KEY ([ObjectTypeID]) REFERENCES [dbo].[ScheduledEmailObjectType] ([ID]),
    CONSTRAINT [FK_ScheduledEmail_ScheduledEmailStatus] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[ScheduledEmailStatus] ([ID])
);

