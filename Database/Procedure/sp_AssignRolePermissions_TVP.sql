DROP PROCEDURE IF EXISTS sp_AssignRolePermissions_TVP;

--CREATE TYPE PermissionIdTableType AS TABLE
--(
--    PermissionId UNIQUEIDENTIFIER
--)
Go
CREATE PROCEDURE sp_AssignRolePermissions_TVP
(
    @ClientId UNIQUEIDENTIFIER,
    @RoleId UNIQUEIDENTIFIER,
    @PermissionIds PermissionIdTableType READONLY,
    @CreatedBy UNIQUEIDENTIFIER,
    @Err_No INT OUTPUT,
    @Err_Msg VARCHAR(MAX) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DELETE FROM RolePermission
        WHERE RoleId = @RoleId and ClientId = @ClientId;

        INSERT INTO RolePermission
        (
            RolePermissionId,
            ClientId,
            RoleId,
            PermissionId,
            CreatedBy,
            CreatedAt
        )
        SELECT
            NEWID(),
            @ClientId,
            @RoleId,
            PermissionId,
            @CreatedBy,
            GETUTCDATE()
        FROM @PermissionIds;

        COMMIT TRANSACTION;

        SET @Err_No = 0;
        SET @Err_Msg = 'Permissions assigned successfully!';


    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        SET @Err_No = 1;
        SET @Err_Msg = ERROR_MESSAGE() ;

    END CATCH
END