CREATE TABLE [dbo].[PurchasedTransaction] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [CreateTime] DATETIME   NULL,
    [Money]      FLOAT (53) NULL,
    [CreateBy]   INT        NOT NULL,
    CONSTRAINT [PK_PurchasedTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchasedTransaction_Users] FOREIGN KEY ([CreateBy]) REFERENCES [dbo].[Users] ([Id])
);

