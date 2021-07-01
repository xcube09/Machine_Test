CREATE PROCEDURE [dbo].[spGet_Cities_By_Region_Code]
	@regionCode char(3)
AS

BEGIN
	SET NOCOUNT ON

    SELECT [CityCode],[RegionCode],[CityName]
	FROM [dbo].[Master_City]
	WHERE RegionCode = @regionCode
	ORDER BY CityName ASC
END
