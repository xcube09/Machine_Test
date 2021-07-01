CREATE PROCEDURE [dbo].[spGet_Regions_By_Country_Code]
	@countryCode char(3) 
AS

BEGIN
	SET NOCOUNT ON

	SELECT [RegionCode],[CountryCode],[RegionName]
	FROM [dbo].[Master_Region]
	WHERE CountryCode = @countryCode
END
