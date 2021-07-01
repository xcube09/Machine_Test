CREATE TABLE [dbo].[Master_Product] (
    [ProductID]   INT             NOT NULL,
    [ProductName] NVARCHAR (255)  NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Master_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC)
);

