CREATE TABLE [dbo].[Wallet] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [Money]       INT      NULL,
    [UpdatedTime] DATETIME NULL,
    CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Wallet_Users] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
);

