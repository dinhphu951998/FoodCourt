CREATE TABLE [dbo].[Payment] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [CreateTime] DATETIME   NULL,
    [Money]      FLOAT (53) NULL,
    [CreateBy]   INT        NOT NULL,
    [AccountId]  INT        NOT NULL,
    [OrderId]    INT        NOT NULL,
    CONSTRAINT [PK_PurchasedTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payment_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_PurchasedTransaction_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [IX_Payment] UNIQUE NONCLUSTERED ([OrderId] ASC)
);

