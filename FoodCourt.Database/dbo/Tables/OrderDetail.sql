CREATE TABLE [dbo].[OrderDetail] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [FoodId]      INT            NOT NULL,
    [OrderId]     INT            NOT NULL,
    [Price]       FLOAT (53)     NULL,
    [Quantity]    INT            NULL,
    [Description] NVARCHAR (MAX) NULL,
    [StoreId]     INT            NULL,
    CONSTRAINT [PK_OrderDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDetail_Food] FOREIGN KEY ([FoodId]) REFERENCES [dbo].[Food] ([Id]),
    CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_OrderDetail_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Store] ([Id]),
    CONSTRAINT [IX_OrderDetail] UNIQUE NONCLUSTERED ([FoodId] ASC, [OrderId] ASC)
);



