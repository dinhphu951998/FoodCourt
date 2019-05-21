CREATE TABLE [dbo].[Store] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NULL,
    [CreateTime] DATETIME       NULL,
    [Activated]  BIT            NULL,
    [Logo]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([Id] ASC)
);



