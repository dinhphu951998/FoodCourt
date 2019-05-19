CREATE TABLE [dbo].[Users] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserName]           NVARCHAR (256) NULL,
    [NormalizedUserName] NVARCHAR (256) NULL,
    [Email]              NVARCHAR (256) NULL,
    [NormalizedEmail]    NVARCHAR (256) NULL,
    [PasswordHash]       NVARCHAR (MAX) NULL,
    [PhoneNumber]        NVARCHAR (MAX) NULL,
    [AccessFailedCount]  INT            NOT NULL,
    [FullName]           NVARCHAR (50)  NULL,
    [Address]            NVARCHAR (50)  NULL,
    [Activated]          BIT            NULL,
    [BirthDate]          DATETIME       NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Users_Username] UNIQUE NONCLUSTERED ([UserName] ASC)
);

