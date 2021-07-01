CREATE TABLE [dbo].[Master_SalesRecord]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerName] NVARCHAR(100) NULL, 
    [CityCode] INT NOT NULL, 
    [DateOfSale] DATETIME2 NOT NULL, 
    [ProductID] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Total] NUMERIC(18, 2) NOT NULL, 
    CONSTRAINT [FK_Master_SalesRecord_Master_City] FOREIGN KEY ([CityCode]) REFERENCES [Master_City]([CityCode]), 
    CONSTRAINT [FK_Master_SalesRecord_Master_Product] FOREIGN KEY ([ProductID]) REFERENCES [Master_Product]([ProductID])
)
