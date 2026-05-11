--CREATE TYPE RoleIdTableType AS TABLE
--(
--    RoleId UNIQUEIDENTIFIER
--)

DROP PROCEDURE IF EXISTS sp_AssignUserRoles;
GO

CREATE PROCEDURE sp_AssignUserRoles
(
    @ClientId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @RoleIds RoleIdTableType READONLY,
    @CreatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    
    DELETE FROM UserRole
    WHERE ClientId = @ClientId and UserId = @UserId;

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
        @UserId,
        RoleId,
        1,
        GETUTCDATE(),
        @CreatedBy
        
    FROM @RoleIds;

END