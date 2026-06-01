--declare @ErrNumber INT; 
--declare @ErrMsg VARCHAR(MAX) 
--DECLARE @ClientId UNIQUEIDENTIFIER = NEWID();
--exec Sp_Sign_Up 'SHI','shivam','shivam@gmail.com','1234','',@ClientId,@ErrNumber,@ErrMsg
--select @ErrMsg output



--select * from [User]

DROP PROCEDURE IF EXISTS Sp_Sign_Up;
GO

CREATE PROCEDURE Sp_Sign_Up 
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

        -- Validations

        IF EXISTS (SELECT 1 FROM [User] WHERE UserName = @UserName)  
            THROW 50001, 'User Name already exists.', 1;  

        IF EXISTS (SELECT 1 FROM [User] WHERE UserEmail = @UserEmail)  
            THROW 50002, 'User Email already exists.', 1; 
            
        -- Insert User
        INSERT INTO [User] 
        (
            UserId, UserName, PasswordHash, UserSalt,
            UserEmail,IsLocked,IsCompanyProfileCreated,
            IsActive, CreatedAt, CreatedBy
        ) 
        VALUES 
        (
            NEWID(),@UserName, @HashPassword, @UserSalt,
            @UserEmail,0, 0,1, GETUTCDATE(), @CreatedBy
        );  

        SET @ErrMsg = 'User Created';  
        SET @ErrNumber = 0;  

    END TRY  
    BEGIN CATCH  
        SET @ErrMsg = ERROR_MESSAGE();  
        SET @ErrNumber = 1;  
    END CATCH  
END