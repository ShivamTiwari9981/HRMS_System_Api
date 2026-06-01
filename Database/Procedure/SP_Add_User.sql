DROP PROCEDURE IF EXISTS SP_Add_User;
GO

CREATE PROCEDURE SP_Add_User
    @JsonData NVARCHAR(MAX),
    @CreatedBy UNIQUEIDENTIFIER,
    @Err_No INT OUTPUT,
    @Err_Msg VARCHAR(MAX) OUTPUT
AS  
BEGIN  
    SET NOCOUNT ON;  
    SET XACT_ABORT ON;

    ---------------- User ----------------
    DECLARE @UserId UNIQUEIDENTIFIER = NEWID();
    DECLARE @ClientId UNIQUEIDENTIFIER;
    DECLARE @UserCode NVARCHAR(20) = NULL;
    DECLARE @UserName NVARCHAR(20);
    DECLARE @PasswordHash NVARCHAR(20);
    DECLARE @UserSalt NVARCHAR(20);
    DECLARE @UserEmail NVARCHAR(20);
    DECLARE @Phone NVARCHAR(20)  = NULL;
    DECLARE @ProfileImagePath NVARCHAR(20)  = NULL;
    DECLARE @FailedLoginAttempts int = NULL;
    DECLARE @LockoutEnd datetime2  = NULL;
    DECLARE @IsLocked bit  = NULL;
    DECLARE @IsCompanyProfileCreated bit =0;
    DECLARE @EmployeeId uniqueidentifier = NULL;
    DECLARE @IsActive bit 

    BEGIN TRY  
         ---------------- Parse JSON ----------------

        SELECT
            @ClientId = JSON_VALUE(@JsonData, '$.ClientId'),
            @UserCode = JSON_VALUE(@JsonData, '$.UserCode'),
            @UserName = JSON_VALUE(@JsonData, '$.UserName'),
            @PasswordHash = JSON_VALUE(@JsonData, '$.PasswordHash'),
            @UserSalt = JSON_VALUE(@JsonData, '$.UserSalt'),
            @UserEmail =  JSON_VALUE(@JsonData, '$.UserEmail'),
            @Phone = JSON_VALUE(@JsonData, '$.Phone'),
            @ProfileImagePath = JSON_VALUE(@JsonData, '$.ProfileImagePath'),
            @FailedLoginAttempts = JSON_VALUE(@JsonData, '$.FailedLoginAttempts'),
            @LockoutEnd = JSON_VALUE(@JsonData, '$.LockoutEnd'),
            @IsLocked = JSON_VALUE(@JsonData, '$.IsLocked'),
            @IsCompanyProfileCreated = JSON_VALUE(@JsonData, '$.IsCompanyProfileCreated'),
            @EmployeeId = JSON_VALUE(@JsonData, '$.EmployeeId'),
            @IsActive = JSON_VALUE(@JsonData, '$.IsActive')
            
        -- Validations

        IF EXISTS (SELECT 1 FROM [User] WHERE ClientId = @ClientId and UserName = @UserName)  
            THROW 50001, 'User Name already exists.', 1;  

        IF EXISTS (SELECT 1 FROM [User] WHERE ClientId = @ClientId and  UserEmail = @UserEmail)  
            THROW 50002, 'User Email already exists.', 1; 
        
        EXEC sp_GenerateMasterCode
            @ClientId = @ClientId,
            @TableName = 'User',
            @CreatedBy = @CreatedBy,
            @ErrNo = @Err_No OUTPUT,
            @Msg = @Err_Msg OUTPUT;
            IF @Err_No <> 0
             BEGIN
                SET @Err_No = 1;
                SET @Err_Msg = 'Error In User  code generation';

                ROLLBACK TRANSACTION;
                RETURN;

            END
        -- Insert User
        SET @UserCode=@Err_Msg;
        INSERT INTO [User] 
        (
            UserId,ClientId,UserCode, UserName, PasswordHash, UserSalt,EmployeeId,
            UserEmail,Phone,ProfileImagePath,FailedLoginAttempts,LockOutEnd,IsLocked,
            IsCompanyProfileCreated,IsActive, CreatedAt, CreatedBy, IsSynced
        ) 
        VALUES 
        (
            NEWID(),@ClientId,@UserCode,@UserName, @PasswordHash, @UserSalt,@EmployeeId,
            @UserEmail,@Phone,@ProfileImagePath,@FailedLoginAttempts,@LockoutEnd,@IsLocked,
            @IsCompanyProfileCreated,1, GETUTCDATE(), @CreatedBy,0
        );  

        SET @Err_No = 0;  
        SET @Err_Msg = 'User created successfully';

    END TRY  
    BEGIN CATCH  
        SET @Err_No = 1  
        SET @Err_Msg = ERROR_MESSAGE();;  
    END CATCH  
END


