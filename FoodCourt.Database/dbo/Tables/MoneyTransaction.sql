CREATE TABLE [dbo].[MoneyTransaction] (
    [WalletId]   INT           IDENTITY (1, 1) NOT NULL,
    [Money]      FLOAT (53)    NULL,
    [Type]       NVARCHAR (50) NULL,
    [CreateTime] DATETIME      NULL,
    [CreateBy]   INT           NOT NULL,
    CONSTRAINT [PK_MoneyTransaction] PRIMARY KEY CLUSTERED ([WalletId] ASC, [CreateBy] ASC),
    CONSTRAINT [FK_MoneyTransaction_Users] FOREIGN KEY ([CreateBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_MoneyTransaction_Wallet] FOREIGN KEY ([WalletId]) REFERENCES [dbo].[Wallet] ([Id])
);

