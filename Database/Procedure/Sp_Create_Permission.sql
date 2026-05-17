
--SELECT * FROM Permission
--DELETE Permission
-- declare @Err_No INT=0  
-- declare @Err_Msg VARCHAR(MAX)=''
--exec Sp_Create_Permission 
--'7DDC3516-F805-407F-8FD4-3DF3B800E272',
--'2B43AE40-2112-4143-9FF3-FE4E5067CDCB',
--@Err_No,
--@Err_Msg

--select @Err_No output
--select @Err_Msg output


DROP PROCEDURE IF EXISTS Sp_Create_Permission;
GO
create procedure Sp_Create_Permission
 @ClientId UNIQUEIDENTIFIER,
 @CreatedBy UNIQUEIDENTIFIER,
 @Err_No INT OUTPUT,  
 @Err_Msg VARCHAR(MAX) OUTPUT 
as
begin
SET NOCOUNT ON;  
Begin Try
    DECLARE @Permissions TABLE
            (
                PermissionId UNIQUEIDENTIFIER,
                ClientId UNIQUEIDENTIFIER,
                MenuId UNIQUEIDENTIFIER,
                ActionName NVARCHAR(50),
                PermissionKey NVARCHAR(200)
            );
             INSERT INTO @Permissions
            SELECT
                NEWID(),
                @ClientId,
                M.MenuId,
                1,
                UPPER(REPLACE(M.MenuName, ' ', '')) + '_VIEW'
            FROM Menu M;

             SELECT
                PermissionId,
                ClientId,
                MenuId,
                ActionName,
                PermissionKey,
                GETDATE(),
                @CreatedBy
            FROM @Permissions;
            ---------------------------------------------------
            -- ADD
            ---------------------------------------------------

            INSERT INTO @Permissions
            SELECT
                NEWID(),
                @ClientId,
                M.MenuId,
                2,
                UPPER(REPLACE(M.MenuName, ' ', '')) + '_ADD'
            FROM Menu M;

            ---------------------------------------------------
            -- EDIT
            ---------------------------------------------------

            INSERT INTO @Permissions
            SELECT
                NEWID(),
                @ClientId,
                M.MenuId,
                3,
                UPPER(REPLACE(M.MenuName, ' ', '')) + '_EDIT'
            FROM Menu M;

            ---------------------------------------------------
            -- DELETE
            ---------------------------------------------------

            INSERT INTO @Permissions
            SELECT
                NEWID(),
                @ClientId,
                M.MenuId,
                4,
                UPPER(REPLACE(M.MenuName, ' ', '')) + '_DELETE'
            FROM Menu M;


             -- approve
            ---------------------------------------------------

            INSERT INTO @Permissions
            SELECT
                NEWID(),
                @ClientId,
                M.MenuId,
                5,
                UPPER(REPLACE(M.MenuName, ' ', '')) + '_APPROVE'
            FROM Menu M;


             -- approve
             ---------------------------------------------------

            INSERT INTO @Permissions
            SELECT
                NEWID(),
                @ClientId,
                M.MenuId,
                6,
                UPPER(REPLACE(M.MenuName, ' ', '')) + '_EXPORT'
            FROM Menu M;



            -------------------------------------------------
             --INSERT INTO PERMISSION TABLE
            -------------------------------------------------

            INSERT INTO Permission
            (
                PermissionId,
                ClientId,
                MenuId,
                [Action],
                PermissionKey,
                CreatedAt,
                CreatedBy
            )
            SELECT
                PermissionId,
                ClientId,
                MenuId,
                ActionName,
                PermissionKey,
                GETDATE(),
                @CreatedBy
            FROM @Permissions;

        SET @Err_Msg = 'Permission Created';  
        SET @Err_No = 0; 
End Try
 BEGIN CATCH  

        SET @Err_Msg = ERROR_MESSAGE();  
        SET @Err_No = 1;  
        EXEC sp_LogError 
            @ClientId = @ClientId,
            @CreatedBy=@CreatedBy;
        THROW;
    END CATCH  

end


