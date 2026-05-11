--declare @ErrNumber INT; 
--declare @ErrMsg VARCHAR(MAX) 
--DECLARE @ClientId UNIQUEIDENTIFIER = NEWID();
--exec Sp_Sign_Up 'SHI','shivam','shivam@gmail.com','1234','',@ClientId,@ErrNumber,@ErrMsg
--select @ErrMsg output

DROP PROCEDURE IF EXISTS Sp_Sign_Up;
GO

CREATE PROCEDURE Sp_Sign_Up  
    @ClientKey VARCHAR(10),
    @UserName NVARCHAR(50),  
    @UserEmail NVARCHAR(100),  
    @HashPassword NVARCHAR(MAX),  
    @UserSalt NVARCHAR(MAX), 
    @CreatedBy NVARCHAR(200),
    @ErrNumber INT OUTPUT,  
    @ErrMsg VARCHAR(MAX) OUTPUT 
AS  
BEGIN  
    SET NOCOUNT ON;  

    BEGIN TRY  
        BEGIN TRANSACTION;

        DECLARE @ClientId UNIQUEIDENTIFIER = NEWID();
        DECLARE @Code VARCHAR(50);
        DECLARE @Err_no INT;
        DECLARE @Cmp_Err INT;
        DECLARE @Cmp_Msg VARCHAR(200)


        -- Validations

        IF EXISTS (SELECT 1 FROM [User] WHERE UserName = @UserName)  
            THROW 50001, 'UserName already exists.', 1;  

        IF EXISTS (SELECT 1 FROM [User] WHERE UserEmail = @UserEmail)  
            THROW 50002, 'User Email already exists.', 1;  


        -- Company Register
         EXEC Sp_Sign_Up_Client 
            @ClientId = @ClientId,
            @ClientKey =@ClientKey,
            @CreatedBy=@CreatedBy,
            @Err_No = @Cmp_Err OUTPUT,
            @Err_Msg = @Cmp_Msg OUTPUT;

        IF @Cmp_Err <> 0
            THROW 50001, @Cmp_Err, 1;

        -- Generate Code
        EXEC sp_GenerateMasterCode 
            @ClientId = @ClientId,
            @TableName = 'User',
            @CreatedBy=@CreatedBy,
            @ErrNo = @Err_no OUTPUT,
            @Msg = @Code OUTPUT;

        IF @Err_no <> 0
            THROW 50002, 'Code generation failed.', 1;

        -- Insert User
        INSERT INTO [User] 
        (
            ClientId,UserId, UserCode, UserName, PasswordHash, UserSalt, 
            UserEmail, RoleName,IsLocked,
            IsActive, CreatedAt, CreatedBy
        ) 
        VALUES 
        (
            @ClientId,NEWID(), @Code, @UserName, @HashPassword, @UserSalt, 
            @UserEmail, 'A', 0,1, GETUTCDATE(), @CreatedBy
        );  

        COMMIT TRANSACTION;

        SET @ErrMsg = 'User Created';  
        SET @ErrNumber = 0;  

    END TRY  
    BEGIN CATCH  
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @ErrMsg = ERROR_MESSAGE();  
        SET @ErrNumber = 1;  

        THROW;
    END CATCH  
END