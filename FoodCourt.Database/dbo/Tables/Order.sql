CREATE TABLE [dbo].[Order] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Total]       FLOAT (53)   NULL,
    [CreateTime]  DATETIME     NULL,
    [ReceiveTime] DATETIME     NULL,
    [UserId]      INT          NULL,
    [Status]      VARCHAR (50) NULL,
    [StoreId]     INT          NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Store] ([Id]),
    CONSTRAINT [FK_Order_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);





