
--SELECT * FROM [User]
--EXEC sp_GetUserRolePermissions
--'963370AD-44D4-45C8-B21B-11C34A052D23',
--'2B43AE40-2112-4143-9FF3-FE4E5067CDCB'

Drop procedure if exists sp_GetUserRolePermissions
Go
CREATE PROCEDURE sp_GetUserRolePermissions
(
    @ClientId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER
)
WITH RECOMPILE
AS
BEGIN
    
    SET NOCOUNT ON;
    BEGIN TRY
        ---------------------- User & Client ---------------------------
        SELECT TOP 1  
        C.ClientId,C.ClientName,C.CompanyName,C.CompanyEmail,C.ExpiryDate,
        U.UserId,U.UserName,U.UserEmail,U.ProfileImagePath,IsCompanyProfileCreated
        FROM CLIENT AS C
        INNER JOIN  [USER] AS U ON C.ClientId =U.ClientId AND C.IsActive = U.IsActive
        WHERE C.ClientId = @ClientId
        AND U.UserId = @UserId
        AND C.IsActive = 1
        AND U.IsActive = 1;


         ---------------------- User Role ---------------------------
        SELECT DISTINCT
        R.RoleId AS RoleIds,
        R.RoleName  AS RoleNames
        FROM UserRole UR
        INNER JOIN Role R
        ON UR.RoleId = R.RoleId
        WHERE UR.ClientId =@ClientId and UR.UserId = @UserId

        ---------------------- -- Menu ---------------------------
        SELECT DISTINCT
        M.MenuId,
        M.MenuName,
        M.ParentMenuId,
        M.RouterLink,
        M.MenuIcon,
        M.DisplayOrder,
        M.MenuType,
        M.IsVisible
        FROM UserRole UR

        INNER JOIN RolePermission RP
        ON UR.RoleId = RP.RoleId

        INNER JOIN Permission P
        ON RP.PermissionId = P.PermissionId

         INNER JOIN Menu M
        ON P.MenuId = M.MenuId

        WHERE UR.UserId = @UserId
        AND P.ClientId = @ClientId
        AND M.IsActive = 1
        ORDER BY M.DisplayOrder;

    ---------------------- -- Role Permission  ---------------------------
       SELECT
        P.PermissionId,
        P.ClientId,
        P.MenuId,
        P.[Action],
        P.PermissionKey,
        Rp.IsActive
        FROM UserRole UR
        INNER JOIN RolePermission RP
        ON UR.RoleId = RP.RoleId

        INNER JOIN Permission P
        ON RP.PermissionId = P.PermissionId

        WHERE UR.UserId = @UserId
        AND P.ClientId = @ClientId


    END TRY
    BEGIN CATCH
        
        THROW;
    END CATCH
    
END





