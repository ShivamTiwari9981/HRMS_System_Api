--CREATE TYPE UserIdTableType AS TABLE
--(
--    UserId UNIQUEIDENTIFIER
--)
--CREATE TYPE RoleIdTableType AS TABLE
--(
--    RoleId UNIQUEIDENTIFIER
--)
DROP PROCEDURE IF EXISTS sp_AssignBulkUserRoles
GO
CREATE PROCEDURE sp_AssignBulkUserRoles
(
    @ClientId UNIQUEIDENTIFIER,
    @UserIds UserIdTableType READONLY,
    @RoleIds RoleIdTableType READONLY,
    @UserId UNIQUEIDENTIFIER,
    @Err_No INT OUTPUT,
    @Err_Msg VARCHAR(MAX) OUTPUT
)
AS
BEGIN

    BEGIN TRY

        BEGIN TRANSACTION;

        -- Remove old roles
        DELETE UR
        FROM UserRole UR
        INNER JOIN @UserIds U
            ON UR.UserId = U.UserId
            where UR.ClientId = ClientId;

        -- Insert new roles
        INSERT INTO UserRole
        (
            UserRoleId,
            ClientId,
            UserId,
            RoleId,
            IsActive,
            CreatedAt,
            CreatedBy
        )
        SELECT
            NEWID(),
            @ClientId,
            U.UserId,
            R.RoleId,
            1,
            GETUTCDATE(),
            @UserId
            
        FROM @UserIds U
        CROSS JOIN @RoleIds R;

        COMMIT TRANSACTION;
        SET @Err_No = 0;
        SET @Err_Msg = 'Role assign successfully';

    END TRY

    BEGIN CATCH
      IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @Err_No = ERROR_NUMBER();
        SET @Err_Msg = ERROR_MESSAGE();

    END CATCH

END