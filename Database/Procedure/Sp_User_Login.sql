DROP PROCEDURE IF EXISTS Sp_User_Login;
GO
CREATE  PROCEDURE Sp_User_Login
(   
    @UserEmail NVARCHAR(100),
    @ErrNumber INT OUTPUT,
    @ErrMsg VARCHAR(MAX) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ClientId UNIQUEIDENTIFIER;
    DECLARE @UserId UNIQUEIDENTIFIER;
    DECLARE @IsActive bit =0 ;
    BEGIN TRY
        -- Check user exists
        SELECT 
            @UserId = UserId,
            @IsActive = IsActive,
            @ClientId= ClientId
        FROM [User] NOLOCK
        WHERE UserEmail = @UserEmail 

        IF @UserId IS NULL
            THROW 50003, 'Invalid email or password.', 1;

        -- Check active
        IF @IsActive = 0
            THROW 50004, 'User is inactive.', 1;

        -- Success → return user data

                SELECT  TOP 1
                UserId,
                ClientId,
                UserName,
                UserEmail,
                IsCompanyProfileCreated,
                PasswordHash,
                UserSalt
                FROM [User] NOLOCK
                WHERE UserId = @UserId 

        SET @ErrNumber = 0;
        SET @ErrMsg = 'Login successful';

    END TRY
    BEGIN CATCH
        SET @ErrNumber = 1;
        SET @ErrMsg = ERROR_MESSAGE();
        THROW;
    END CATCH
END