DROP PROCEDURE IF EXISTS Sp_Create_Role;
GO

CREATE PROCEDURE Sp_Create_Role 

    @ClientId UNIQUEIDENTIFIER,
    @RoleId UNIQUEIDENTIFIER,
    @RoleName VARCHAR(20),
    @IsSystemRole bit=0,
    @CreatedBy NVARCHAR(200),
    @Err_No INT OUTPUT,  
    @Err_Msg VARCHAR(MAX) OUTPUT 
AS  
BEGIN  
    SET NOCOUNT ON;  

    BEGIN TRY  
        DECLARE @IsActive BIT;

        SELECT @IsActive = IsActive
        FROM [Role]
        WHERE RoleName = @RoleName;

        IF @IsActive IS NOT NULL
        BEGIN
            IF @IsActive = 0
                THROW 50001, 'Role already exists but is deactivated. Please contact support.', 1;
            END

        -- Insert User
        INSERT INTO [Role] 
        (
            RoleId,ClientId,RoleName,IsSystemRole,
            IsActive, CreatedAt, CreatedBy
        ) 
        VALUES 
        (
            @RoleId,@ClientId,@RoleName,@IsSystemRole,1,GETUTCDATE(), @CreatedBy
        );  
        SET @Err_Msg = 'Role Created';  
        SET @Err_No = 0;  

    END TRY  
    BEGIN CATCH  

        SET @Err_Msg = ERROR_MESSAGE();  
        SET @Err_No = 1;  

        THROW;
    END CATCH  
END