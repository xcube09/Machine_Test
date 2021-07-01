CREATE TABLE [dbo].[Master_City] (
    [CityCode]   INT            NOT NULL,
    [RegionCode] CHAR (3)       NOT NULL,
    [CityName]   NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Master_City] PRIMARY KEY CLUSTERED ([CityCode] ASC),
    CONSTRAINT [FK_Master_City_Master_Region] FOREIGN KEY ([RegionCode]) REFERENCES [dbo].[Master_Region] ([RegionCode])
);


GO

CREATE INDEX [IX_Master_City_CityName] ON [dbo].[Master_City] ([CityName])

GO

CREATE INDEX [IX_Master_City_RegionCode] ON [dbo].[Master_City] ([RegionCode])

GO
