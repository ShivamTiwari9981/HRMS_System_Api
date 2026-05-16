DROP PROCEDURE IF EXISTS sp_Signup_AssignRolePermissionsToCompanyAdmin
Go
CREATE PROCEDURE sp_Signup_AssignRolePermissionsToCompanyAdmin
(
    @ClientId UNIQUEIDENTIFIER,
    @RoleId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Err_No INT OUTPUT,
    @Err_Msg VARCHAR(MAX) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IsAdmin BIT = 0;

    BEGIN TRY
        SELECT  @IsAdmin = IsSystemRole FROM [Role] With (nolock) WHERE ClientId  = @ClientId and RoleId=@RoleId 
        
        IF @IsAdmin = 1
        BEGIN
            DELETE FROM RolePermission
            WHERE ClientId = @ClientId and RoleId = @RoleId 

            INSERT INTO RolePermission
            (
                RolePermissionId,
                ClientId,
                RoleId,
                PermissionId,
                CreatedBy,
                CreatedAt,
                IsActive
            )
            SELECT
                NEWID(),
                @ClientId,
                @RoleId,
                PermissionId,
                @CreatedBy,
                GETUTCDATE(),
                1
            FROM Permission;

            SET @Err_No = 0;
            SET @Err_Msg = 'Permissions assigned successfully!';
        END
        ELSE
            BEGIN
                SET @Err_No = 1;
                SET @Err_Msg = 'Permission assigned is failed becauese this is not Company Admin';

            END
    END TRY

    BEGIN CATCH

        SET @Err_No = 1;
        SET @Err_Msg = ERROR_PROCEDURE() + ERROR_MESSAGE() ;

        -------- Create LogError  ---------------
        EXEC sp_LogError 
            @ClientId = @ClientId,
            @CreatedBy=@CreatedBy;

        THROW;



    END CATCH
END
