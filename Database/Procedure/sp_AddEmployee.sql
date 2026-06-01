
DROP PROCEDURE IF EXISTS sp_AddEmployee;
GO

CREATE PROCEDURE sp_AddEmployee
(
    @JsonData NVARCHAR(MAX),
    @CreatedBy UNIQUEIDENTIFIER,
    @Err_No INT OUTPUT,
    @Err_Msg VARCHAR(MAX) OUTPUT
)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    ---------------- Employee ----------------

    DECLARE @EmployeeId UNIQUEIDENTIFIER;

    DECLARE @ClientId UNIQUEIDENTIFIER;
    DECLARE @DepartmentId UNIQUEIDENTIFIER;
    DECLARE @DesignationId UNIQUEIDENTIFIER;
    DECLARE @ManagerId UNIQUEIDENTIFIER;
    DECLARE @CountryId UNIQUEIDENTIFIER;
    DECLARE @StateId UNIQUEIDENTIFIER;
    DECLARE @CityId UNIQUEIDENTIFIER;
    DECLARE @EmployeeCode NVARCHAR(20);
    DECLARE @FirstName NVARCHAR(200);
    DECLARE @LastName NVARCHAR(200);
    DECLARE @EmployeeEmail NVARCHAR(200);
    DECLARE @Phone NVARCHAR(20);
    DECLARE @PostalCode NVARCHAR(20);
    DECLARE @JoiningDate DATETIME2;
    DECLARE @BirthDate DATETIME2;
    DECLARE @Gender INT;
    DECLARE @EmergencyContact NVARCHAR(200);
    DECLARE @AddressLine1 NVARCHAR(200);
    DECLARE @AddressLine2 NVARCHAR(200);
    DECLARE @IsLoginUser BIT;
    DECLARE @SalaryJson NVARCHAR(MAX);
    DECLARE @UserJson NVARCHAR(MAX);

    BEGIN TRY
       
        ---------------- Parse JSON ----------------

        SELECT
            @EmployeeId = JSON_VALUE(@JsonData, '$.EmployeeId'),
            @ClientId = JSON_VALUE(@JsonData, '$.ClientId'),
            @EmployeeCode = JSON_VALUE(@JsonData, '$.EmployeeCode'),
            @FirstName = JSON_VALUE(@JsonData, '$.FirstName'),
            @LastName = JSON_VALUE(@JsonData, '$.LastName'),
            @EmployeeEmail = JSON_VALUE(@JsonData, '$.EmployeeEmail'),
            
            @Phone = JSON_VALUE(@JsonData, '$.Phone'),
            @DepartmentId = JSON_VALUE(@JsonData, '$.DepartmentId'),
            @DesignationId = JSON_VALUE(@JsonData, '$.DesignationId'),
            @JoiningDate = JSON_VALUE(@JsonData, '$.JoiningDate'),
            @BirthDate = JSON_VALUE(@JsonData, '$.BirthDate'),
            @Gender = JSON_VALUE(@JsonData, '$.Gender'),
            @AddressLine1 = JSON_VALUE(@JsonData, '$.AddressLine1'),
            @AddressLine2 = JSON_VALUE(@JsonData, '$.AddressLine2'),
            @CountryId = JSON_VALUE(@JsonData, '$.CountryId'),
            @StateId = JSON_VALUE(@JsonData, '$.StateId'),
            @CityId = JSON_VALUE(@JsonData, '$.CityId'),
            @PostalCode = JSON_VALUE(@JsonData, '$.PostalCode'),
            @EmergencyContact = JSON_VALUE(@JsonData, '$.EmergencyContact'),
            @ManagerId = JSON_VALUE(@JsonData, '$.ManagerId'),
            @IsLoginUser = JSON_VALUE(@JsonData, '$.IsLoginUser')

            Set @SalaryJson = JSON_QUERY(@JsonData, '$.Salary');
            Set @UserJson = JSON_QUERY(@JsonData, '$.User');
        ---------------- Department Validation ----------------

        IF NOT EXISTS
        (
            SELECT 1
            FROM Department
            WHERE DepartmentId = @DepartmentId
              AND ClientId = @ClientId
              AND ISNULL(IsActive,1) = 1
        )
        BEGIN

            SET @Err_No = 1;
            SET @Err_Msg = 'Department not found';

            RETURN;

        END

        ---------------- Designation Validation ----------------

        IF NOT EXISTS
        (
            SELECT 1
            FROM Designation
            WHERE DesignationId = @DesignationId
              AND DepartmentId = @DepartmentId
              AND ClientId = @ClientId
              AND ISNULL(IsActive,1) = 1
        )
        BEGIN

            SET @Err_No = 1;
            SET @Err_Msg = 'Invalid designation for department';

            RETURN;

        END

        ---------------- Manager Validation ----------------

        IF @ManagerId IS NOT NULL
        BEGIN

            IF NOT EXISTS
            (
                SELECT 1
                FROM Employee
                WHERE EmployeeId = @ManagerId
                  AND ClientId = @ClientId
            )
            BEGIN

                SET @Err_No = 1;
                SET @Err_Msg = 'Manager not found';

                RETURN;

            END

        END

        ---------------- Email Validation ----------------

        IF EXISTS
        (
            SELECT 1
            FROM Employee
            WHERE ClientId = @ClientId
              AND EmployeeEmail = @EmployeeEmail
        )
        BEGIN

            SET @Err_No = 1;
            SET @Err_Msg = 'Employee email already exists';

            RETURN;

        END

        ---------------- Phone Validation ----------------

        IF EXISTS
        (
            SELECT 1
            FROM Employee
            WHERE ClientId = @ClientId
              AND Phone = @Phone
              AND ISNULL(IsActive,1) = 1
        )
        BEGIN

            SET @Err_No = 1;
            SET @Err_Msg = 'Phone number already exists';

            RETURN;

        END

    BEGIN TRANSACTION;
         EXEC sp_GenerateMasterCode
            @ClientId = @ClientId,
            @TableName = 'Employee',
            @CreatedBy = @CreatedBy,
            @ErrNo = @Err_No OUTPUT,
            @Msg = @Err_Msg OUTPUT;

           

            IF @Err_No <> 0
             BEGIN
                SET @Err_No = 1;
                SET @Err_Msg = 'Error In Employee  code generation';

                ROLLBACK TRANSACTION;
                RETURN;

            END
        ---------------- Insert Employee ----------------
        SET @EmployeeCode=@Err_Msg;
        INSERT INTO Employee
        (
            EmployeeId,
            ClientId,
            EmployeeCode,
            FirstName,
            LastName,
            EmployeeEmail,
            Phone,
            DepartmentId,
            DesignationId,
            JoiningDate,
            BirthDate,
            Gender,
            AddressLine1,
            AddressLine2,
            CountryId,
            StateId,
            CityId,
            PostalCode,
            EmergencyContact,
            ManagerId,
            IsLoginUser,
            IsActive,
            CreatedAt,
            CreatedBy,
            IsSynced
        )
        VALUES
        (
            @EmployeeId,
            @ClientId,
            @EmployeeCode,
            @FirstName,
            @LastName,
            @EmployeeEmail,
            @Phone,
            @DepartmentId,
            @DesignationId,
            @JoiningDate,
            @BirthDate,
            @Gender,
            @AddressLine1,
            @AddressLine2,
            @CountryId,
            @StateId,
            @CityId,
            @PostalCode,
            @EmergencyContact,
            @ManagerId,
            @IsLoginUser,
            1,
            GETUTCDATE(),
            @CreatedBy,
            0
        );

        IF @SalaryJson IS NULL
            BEGIN
                SET @Err_No =1;  
                SET @Err_Msg ='Employee Salary can not null or empty';  
                ROLLBACK TRANSACTION;
                RETURN
            END
             


        EXEC Sp_EmployeeSalary   
            @JsonData = @SalaryJson,  
            @CreatedBy=@CreatedBy,  
            @Err_No = @Err_No OUTPUT,  
            @Err_Msg = @Err_Msg OUTPUT;  
  
         IF @Err_No <> 0  
            BEGIN
                SET @Err_No = 1;  
                SET @Err_Msg =@Err_Msg;  
                ROLLBACK TRANSACTION;
                RETURN
            END

        IF @IsLoginUser = 1
        BEGIN

            IF @UserJson IS NULL
            BEGIN
                SET @Err_No =1;  
                SET @Err_Msg ='User data can not null or empty';  
                ROLLBACK TRANSACTION;
                RETURN
            END

            EXEC SP_Add_User   
            @JsonData = @UserJson,  
            @CreatedBy=@CreatedBy,  
            @Err_No = @Err_No OUTPUT,  
            @Err_Msg = @Err_Msg OUTPUT;  
  
         IF @Err_No <> 0  
             BEGIN
                SET @Err_No = 1;  
                SET @Err_Msg =@Err_Msg;  
                ROLLBACK TRANSACTION;
                RETURN
            END  

        END
        

        COMMIT TRANSACTION;  
        SET @Err_No = 0;
        SET @Err_Msg = 'Employee created successfully';

    END TRY

    BEGIN CATCH
        IF @@TRANCOUNT > 0  
            ROLLBACK TRANSACTION; 

        SET @Err_No = ERROR_NUMBER();

        SET @Err_Msg =
            ISNULL(ERROR_PROCEDURE(), '')
            + ' - '
            + ERROR_MESSAGE();

    END CATCH

END
GO

--select * from Employee