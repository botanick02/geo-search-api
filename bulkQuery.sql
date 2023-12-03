INSERT INTO Location (
    City,
    Latitude,
    Longitude,
    Country
)
SELECT
    city AS City,
    CAST(lat AS DECIMAL(9,6)) AS Latitude,
    CAST(lng AS DECIMAL(9,6)) AS Longitude,
    country AS Country
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',
    'Excel 12.0;Database=C:\worldcities.xlsx;HDR=YES',
    'SELECT city, lat, lng, country FROM [Sheet1$]'
) AS t
WHERE 
    t.city IS NOT NULL 
    AND t.lat IS NOT NULL 
    AND t.lng IS NOT NULL 
    AND t.country IS NOT NULL;