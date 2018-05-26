CREATE TABLE [dbo].[PersonalData] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (128) NOT NULL,
    [RegionID]    INT            NOT NULL,
    [CityID]      INT            NOT NULL,
    [PhoneNumber] NVARCHAR (50)  NOT NULL,
    [PhotoUrl]    NVARCHAR (512) CONSTRAINT [DF_PersonalData_Photo] DEFAULT (N'nophoto.jpg') NOT NULL,
    CONSTRAINT [PK_PersonalData] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PersonalData_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]),
    CONSTRAINT [FK_PersonalData_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID])
);

