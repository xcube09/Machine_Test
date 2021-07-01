CREATE PROCEDURE [dbo].[spSearch_Sales_Records]
	@offset int,
    @pageSize int,
    @countryCode char(3),
    @regionCode char(3),
    @cityCode int,
    @dateOfSale datetime2(7)
AS

BEGIN
        SET NOCOUNT ON

        BEGIN TRY

        SELECT s.Id, s.CustomerName, s.DateOfSale, s.Quantity, s.Total, p.ProductName, c.CityName, r.RegionName, co.CountryName
        FROM [dbo].[Master_SalesRecord] s
        INNER JOIN [dbo].[Master_Product] p ON s.ProductID = p.ProductID
        INNER JOIN [dbo].[Master_City] c ON s.CityCode = c.CityCode
        INNER JOIN [dbo].[Master_Region] r ON c.RegionCode = r.RegionCode
        INNER JOIN [dbo].[Master_Country] co ON r.CountryCode = co.CountryCode
        WHERE s.CityCode = @cityCode OR @cityCode IS NULL
              AND (s.DateOfSale = @dateOfSale OR @dateOfSale IS NULL)
              AND (r.RegionCode = @regionCode OR @regionCode IS NULL)
              AND (co.CountryCode = @countryCode OR @countryCode IS NULL)
        ORDER BY s.DateOfSale
        OFFSET @offset ROWS  
        FETCH NEXT @pageSize ROWS ONLY;

        SELECT COUNT(s.Id)    
        FROM [dbo].[Master_SalesRecord] s
        INNER JOIN [dbo].[Master_Product] p ON s.ProductID = p.ProductID
        INNER JOIN [dbo].[Master_City] c ON s.CityCode = c.CityCode
        INNER JOIN [dbo].[Master_Region] r ON c.RegionCode = r.RegionCode
        INNER JOIN [dbo].[Master_Country] co ON r.CountryCode = co.CountryCode
        WHERE s.CityCode = @cityCode OR @cityCode IS NULL
              AND (s.DateOfSale = @dateOfSale OR @dateOfSale IS NULL)
              AND (r.RegionCode = @regionCode OR @regionCode IS NULL)
              AND (co.CountryCode = @countryCode OR @countryCode IS NULL);

               END TRY
    BEGIN CATCH
        INSERT INTO [dbo].[Error_Logs]
    VALUES
      (SUSER_SNAME(),
       ERROR_NUMBER(),
       ERROR_STATE(),
       ERROR_SEVERITY(),
       ERROR_LINE(),
       ERROR_PROCEDURE(),
       ERROR_MESSAGE(),
       GETDATE());

       THROW
    END CATCH            
END
