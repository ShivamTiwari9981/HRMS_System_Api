
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

         ---------------------- User ---------------------------
        SELECT TOP 1  
        UserId,UserName,UserEmail,ProfileImagePath,IsCompanyProfileCreated 
        FROM [USER]  WHERE ClientId = @ClientId AND UserId = @UserId AND IsActive = 1;

        ---------------------- Client ---------------------------
        SELECT TOP 1  
        C.ClientId,C.CompanyName,C.CompanyEmail,C.SubscriptionStartDate,C.SubscriptionEndDate
        FROM CLIENT AS C
        WHERE C.ClientId = @ClientId
        AND C.IsActive = 1


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
        PM.MenuId,
        PM.MenuName,
        PM.ParentMenuId,
        PM.RouterLink,
        PM.MenuIcon,
        PM.DisplayOrder,
        PM.MenuType,
        PM.IsVisible,
        (
           SELECT DISTINCT
            CM.MenuId,
            CM.MenuName,
            CM.ParentMenuId,
            CM.RouterLink,
            CM.MenuIcon,
            CM.DisplayOrder,
            CM.MenuType,
            CM.IsVisible

            FROM RolePermission CRP

            INNER JOIN Permission CP
                ON CRP.PermissionId = CP.PermissionId

            INNER JOIN Menu CM
                ON CP.MenuId = CM.MenuId

            WHERE
                CRP.RoleId = UR.RoleId
                AND CM.ParentMenuId = PM.MenuId
                AND CM.IsVisible = 1

            ORDER BY CM.DisplayOrder

            FOR JSON PATH

        ) AS SubMenus

        FROM UserRole UR

        INNER JOIN RolePermission RP
            ON UR.RoleId = RP.RoleId

        INNER JOIN Permission P
            ON RP.PermissionId = P.PermissionId

        INNER JOIN Menu M
            ON P.MenuId = M.MenuId

        /* Parent Menu */

        INNER JOIN Menu PM
            ON (
                CASE
                    WHEN M.ParentMenuId IS NULL
                    THEN M.MenuId
                    ELSE M.ParentMenuId
                END
            ) = PM.MenuId

        WHERE
            UR.UserId = @UserId
            AND P.ClientId = @ClientId
            AND PM.IsVisible = 1

        ORDER BY
            PM.DisplayOrder,
            PM.MenuName;




        --SELECT DISTINCT
        --M.MenuId,
        --M.MenuName,
        --M.ParentMenuId,
        --M.RouterLink,
        --M.MenuIcon,
        --M.DisplayOrder,
        --M.MenuType,
        --M.IsVisible
        --FROM UserRole UR

        --INNER JOIN RolePermission RP
        --ON UR.RoleId = RP.RoleId

        --INNER JOIN Permission P
        --ON RP.PermissionId = P.PermissionId

        -- INNER JOIN Menu M
        --ON P.MenuId = M.MenuId

        --WHERE UR.UserId = @UserId
        --AND P.ClientId = @ClientId
        --AND M.IsActive = 1
        --ORDER BY M.DisplayOrder;

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





