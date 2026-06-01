DROP PROCEDURE IF EXISTS Sp_EmployeeSalary;
GO
CREATE PROCEDURE Sp_EmployeeSalary
(   
    @JsonData NVARCHAR(MAX),   
    @CreatedBy UNIQUEIDENTIFIER,  
    @Err_No INT OUTPUT,  
    @Err_Msg VARCHAR(MAX) OUTPUT  
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @SalaryId UNIQUEIDENTIFIER = NEWID();
    DECLARE @EmployeeId UNIQUEIDENTIFIER;

    DECLARE @BasicSalary DECIMAL(18,2);
    DECLARE @HRA DECIMAL(18,2);
    DECLARE @Allowance DECIMAL(18,2);
    DECLARE @Deduction DECIMAL(18,2);
    DECLARE @NetSalary DECIMAL(18,2);
    DECLARE @EffectiveFrom DATETIME;
    DECLARE @IsCurrent BIT;

    BEGIN TRY
        SELECT
            @BasicSalary = JSON_VALUE(@JsonData, '$.BasicSalary'),
            @EmployeeId = JSON_VALUE(@JsonData, '$.EmployeeId'),
            @HRA = JSON_VALUE(@JsonData, '$.HRA'),
            @Allowance = JSON_VALUE(@JsonData, '$.Allowance'),
            @Deduction = JSON_VALUE(@JsonData, '$.Deduction'),
            @NetSalary = JSON_VALUE(@JsonData, '$.NetSalary'),
            @EffectiveFrom = JSON_VALUE(@JsonData, '$.EffectiveFrom'),
            @IsCurrent = JSON_VALUE(@JsonData, '$.IsCurrent');

         IF NOT EXISTS
        (
            SELECT 1
            FROM Employee
            WHERE EmployeeId = @EmployeeId
        )
        BEGIN
            SET @Err_No = 1;
            SET @Err_Msg = 'Employee not found in proc Sp_EmployeeSalary';
            RETURN;
        END

        IF(@IsCurrent = 1)
        BEGIN

            UPDATE EmployeeSalary
            SET IsCurrent = 0
            WHERE EmployeeId = @EmployeeId;

        END

        INSERT INTO EmployeeSalary
        (
            SalaryId,
            EmployeeId,
            BasicSalary,
            HRA,
            Allowance,
            Deduction,
            NetSalary,
            EffectiveFrom,
            IsCurrent,
            IsActive,
            CreatedAt,
            CreatedBy,
            IsSynced
        )
        VALUES
        (
            @SalaryId,
            @EmployeeId,
            @BasicSalary,
            @HRA,
            @Allowance,
            @Deduction,
            @NetSalary,
            @EffectiveFrom,
            @IsCurrent,
            1,
            GETUTCDATE(),
            @CreatedBy,
            0
        );

        SET @Err_No = 0;
        SET @Err_Msg = 'Salary added successfully';

    END TRY

    BEGIN CATCH

        SET @Err_No = 1;
        SET @Err_Msg = ERROR_PROCEDURE() + ' With Error ' + ERROR_MESSAGE() ;

    END CATCH

END

