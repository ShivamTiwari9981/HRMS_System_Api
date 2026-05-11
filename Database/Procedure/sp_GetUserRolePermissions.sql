Drop procedure if exists sp_GetUserRolePermissions
Go
CREATE PROCEDURE sp_GetUserRolePermissions
(
    @ClientId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER
)
AS
BEGIN

    SET NOCOUNT ON;

    SELECT DISTINCT
    R.RoleId,
    R.RoleName
    FROM UserRole UR
    INNER JOIN Role R
    ON UR.RoleId = R.RoleId
    WHERE UR.ClientId =@ClientId and UR.UserId = @UserId

    SELECT DISTINCT
    P.PermissionId,
    P.Module,
    P.Action,
    P.Module + '.' + P.Action AS PermissionName
FROM UserRole UR
INNER JOIN RolePermission RP
    ON UR.RoleId = RP.RoleId
INNER JOIN Permission P
    ON RP.PermissionId = P.PermissionId
WHERE UR.ClientId=@ClientId and UR.UserId = @UserId

END


