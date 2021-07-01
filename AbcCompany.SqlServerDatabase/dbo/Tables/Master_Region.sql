CREATE TABLE [dbo].[Master_Region] (
    [RegionCode]  CHAR (3)       NOT NULL,
    [CountryCode] CHAR (3)       NOT NULL,
    [RegionName]  NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Master_Region] PRIMARY KEY CLUSTERED ([RegionCode] ASC),
    CONSTRAINT [FK_Master_Region_Master_Country] FOREIGN KEY ([CountryCode]) REFERENCES [dbo].[Master_Country] ([CountryCode])
);

