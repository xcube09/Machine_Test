CREATE PROCEDURE [dbo].[spGet_Countries]	
AS

BEGIN
	SET NOCOUNT ON

	SELECT [CountryCode],[CountryName]
	FROM [dbo].[Master_Country];
END
