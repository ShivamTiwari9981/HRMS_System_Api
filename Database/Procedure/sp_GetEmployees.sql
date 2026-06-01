

DROP PROCEDURE IF EXISTS sp_GetEmployees;
GO

CREATE PROCEDURE sp_GetEmployees
(
    @ClientId UNIQUEIDENTIFIER,

    @PageNumber INT = 1,
    @PageSize INT = 10,

    @SearchText NVARCHAR(200) = NULL,

    @DepartmentId UNIQUEIDENTIFIER = NULL,
    @DesignationId UNIQUEIDENTIFIER = NULL,

    @IsActive BIT = NULL,

    @SortColumn NVARCHAR(50) = 'CreatedAt',
    @SortDirection NVARCHAR(4) = 'DESC'
)
AS
BEGIN
    SET NOCOUNT ON;

    -------------------------------------------------
    -- Validate Pagination
    -------------------------------------------------

    IF @PageNumber <= 0
        SET @PageNumber = 1;

    IF @PageSize <= 0
        SET @PageSize = 10;

    DECLARE @Offset INT =
        (@PageNumber - 1) * @PageSize;

    -------------------------------------------------
    -- Base Query
    -------------------------------------------------

    ;WITH EmployeeCTE AS
    (
        SELECT
            E.EmployeeId,
            E.EmployeeCode,

            FullName =
                CONCAT(
                    ISNULL(E.FirstName,''),
                    ' ',
                    ISNULL(E.LastName,'')
                ),

            E.EmployeeEmail,
            E.Phone,

            D.DepartmentName,
            DG.DesignationName,

            E.JoiningDate,
            E.IsActive,
            E.CreatedAt,

           COUNT(*) OVER() AS TotalRecords

        FROM Employee E

        INNER JOIN Department D
            ON D.DepartmentId = E.DepartmentId

        INNER JOIN Designation DG
            ON DG.DesignationId = E.DesignationId

        WHERE
            E.ClientId = @ClientId

            AND
            (
                @DepartmentId IS NULL
                OR E.DepartmentId = @DepartmentId
            )

            AND
            (
                @DesignationId IS NULL
                OR E.DesignationId = @DesignationId
            )

            AND
            (
                @IsActive IS NULL
                OR E.IsActive = @IsActive
            )

            AND
            (
                @SearchText IS NULL
                OR
                E.EmployeeCode LIKE '%' + @SearchText + '%'
                OR E.FirstName LIKE '%' + @SearchText + '%'
                OR E.LastName LIKE '%' + @SearchText + '%'
                OR E.EmployeeEmail LIKE '%' + @SearchText + '%'
                OR E.Phone LIKE '%' + @SearchText + '%'
            )
    )



    --SELECT COUNT(1) AS TotalRecords
    --FROM EmployeeCTE;

    -------------------------------------------------
    -- Paged Data
    -------------------------------------------------

    SELECT *
    FROM EmployeeCTE

    ORDER BY

        CASE
            WHEN @SortColumn = 'EmployeeCode'
            AND @SortDirection = 'ASC'
            THEN EmployeeCode
        END ASC,

        CASE
            WHEN @SortColumn = 'EmployeeCode'
            AND @SortDirection = 'DESC'
            THEN EmployeeCode
        END DESC,

        CASE
            WHEN @SortColumn = 'CreatedAt'
            AND @SortDirection = 'ASC'
            THEN CreatedAt
        END ASC,

        CASE
            WHEN @SortColumn = 'CreatedAt'
            AND @SortDirection = 'DESC'
            THEN CreatedAt
        END DESC

    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO