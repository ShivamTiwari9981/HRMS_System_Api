DROP PROCEDURE IF EXISTS Sp_Create_UserRole;
GO

CREATE PROCEDURE Sp_Create_UserRole 

    @ClientId UNIQUEIDENTIFIER,
    @RoleId UNIQUEIDENTIFIER,
    @CreatedBy NVARCHAR(200),
    @Err_No INT OUTPUT,  
    @Err_Msg VARCHAR(MAX) OUTPUT 
AS  
BEGIN  
    SET NOCOUNT ON;  

    BEGIN TRY  
        INSERT INTO [UserRole] 
        (
           UserRoleId, ClientId,UserId,RoleId,
            IsActive, CreatedAt, CreatedBy
        ) 
        VALUES 
        (
           NEWID(),@ClientId,@CreatedBy,@RoleId,1,GETUTCDATE(),@CreatedBy
        );  
        SET @Err_Msg = 'UserRole Created';  
        SET @Err_No = 0;  

    END TRY  
    BEGIN CATCH  

        SET @Err_Msg = ERROR_MESSAGE();  
        SET @Err_No = 1;  
         EXEC sp_LogError 
            @ClientId = @ClientId,
            @CreatedBy=@CreatedBy;

        THROW;
        THROW;
    END CATCH  
END