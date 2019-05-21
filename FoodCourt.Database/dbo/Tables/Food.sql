CREATE TABLE [dbo].[Food] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [CreateTime] DATETIME      NULL,
    [Price]      FLOAT (53)    NULL,
    [Activated]  BIT           NULL,
    [StoreId]    INT           NULL,
    [CategoryId] INT           NULL,
    CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Food_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_Food_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Store] ([Id])
);



