CREATE TABLE [dbo].[Account] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [Type]    VARCHAR (50) NULL,
    [Money]   FLOAT (53)   NULL,
    [UserId]  INT          NULL,
    [StoreId] INT          NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Account_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Store] ([Id]),
    CONSTRAINT [FK_Account_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [IX_Account] UNIQUE NONCLUSTERED ([StoreId] ASC)
);

