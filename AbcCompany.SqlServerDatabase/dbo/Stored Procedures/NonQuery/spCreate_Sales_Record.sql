CREATE PROCEDURE [dbo].[spCreate_Sales_Record]
	@customerName nvarchar(100),
	@cityCode int,
	@dateOfSale datetime2(7),
	@productID int,
    @quantity int
AS
	
BEGIN
    SET NOCOUNT ON

    DECLARE @total numeric(18,2);

    BEGIN TRY

    SET @total =  (SELECT [Price] FROM [dbo].[Master_Product] WHERE ProductID = @productID) * @quantity;

    INSERT INTO [dbo].[Master_SalesRecord]
               ([CustomerName]
               ,[CityCode]
               ,[DateOfSale]
               ,[ProductID]
               ,[Quantity]
               ,[Total])
        OUTPUT Inserted.ID
         VALUES
               (@customerName,
                @cityCode,
                @dateOfSale,
                @productID,
                @quantity,
                @total);

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
