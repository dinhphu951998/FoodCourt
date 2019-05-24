CREATE TABLE [dbo].[UserStore] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [UserId]  INT NULL,
    [StoreId] INT NULL,
    CONSTRAINT [PK_UserStore] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserStore_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Store] ([Id]),
    CONSTRAINT [FK_UserStore_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [IX_UserStore] UNIQUE NONCLUSTERED ([StoreId] ASC, [UserId] ASC)
);

