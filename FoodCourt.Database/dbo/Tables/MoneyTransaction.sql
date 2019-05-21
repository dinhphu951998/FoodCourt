CREATE TABLE [dbo].[MoneyTransaction] (
    [AccountId]  INT           IDENTITY (1, 1) NOT NULL,
    [Money]      FLOAT (53)    NULL,
    [Type]       NVARCHAR (50) NULL,
    [CreateTime] DATETIME      NULL,
    [CreateBy]   INT           NOT NULL,
    CONSTRAINT [PK_MoneyTransaction] PRIMARY KEY CLUSTERED ([AccountId] ASC, [CreateBy] ASC),
    CONSTRAINT [FK_MoneyTransaction_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_MoneyTransaction_Users] FOREIGN KEY ([CreateBy]) REFERENCES [dbo].[Users] ([Id])
);



