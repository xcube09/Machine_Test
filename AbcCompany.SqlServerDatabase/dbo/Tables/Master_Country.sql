CREATE TABLE [dbo].[Master_Country] (
    [CountryCode] CHAR (3)       NOT NULL,
    [CountryName] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Master_Country] PRIMARY KEY CLUSTERED ([CountryCode] ASC)
);

