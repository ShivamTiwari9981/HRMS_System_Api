--SElect * from dbo.fn_GetPrefixClean 'Client'

--SELECT dbo.fn_GetPrefixClean('Client')

CREATE OR ALTER FUNCTION dbo.fn_GetPrefix (@TableName NVARCHAR(100))
RETURNS NVARCHAR(3)
AS
BEGIN
    DECLARE @Clean NVARCHAR(100) = ''
    DECLARE @i INT = 1
    DECLARE @char NCHAR(1)

    WHILE @i <= LEN(@TableName)
    BEGIN
        SET @char = SUBSTRING(@TableName, @i, 1)

        IF @char LIKE '[A-Za-z]'
            SET @Clean = @Clean + @char

        SET @i = @i + 1
    END

    SET @Clean = UPPER(@Clean)

    RETURN LEFT(@Clean + 'XXX', 3)
END