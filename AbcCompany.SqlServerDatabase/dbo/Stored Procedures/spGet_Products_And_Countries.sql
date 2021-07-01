CREATE PROCEDURE [dbo].[spGet_Products_And_Countries]	
AS

BEGIN
    SET NOCOUNT ON

    SELECT [ProductID],[ProductName],[Price]
    FROM [dbo].[Master_Product];

    SELECT [CountryCode],[CountryName]
	FROM [dbo].[Master_Country];
END
