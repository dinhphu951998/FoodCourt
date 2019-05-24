CREATE TABLE [dbo].[Transaction] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [AccountId]  INT           NOT NULL,
    [CreateBy]   INT           NOT NULL,
    [Money]      FLOAT (53)    NULL,
    [Type]       NVARCHAR (50) NULL,
    [CreateTime] DATETIME      NULL,
    CONSTRAINT [PK_MoneyTransaction_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MoneyTransaction_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_MoneyTransaction_Users] FOREIGN KEY ([CreateBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [IX_MoneyTransaction] UNIQUE NONCLUSTERED ([AccountId] ASC, [CreateBy] ASC)
);

