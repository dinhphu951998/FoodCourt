CREATE TABLE [dbo].[Order] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Total]       FLOAT (53) NULL,
    [CreateTime]  DATETIME   NULL,
    [Activated]   BIT        NULL,
    [ReceiveTime] DATETIME   NULL,
    [UserId]      INT        NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

