DROP PROCEDURE IF EXISTS Sp_User_Login;
GO
CREATE  PROCEDURE Sp_User_Login
(   @ClientKey Varchar(3),
    @UserEmail NVARCHAR(100),
    @ErrNumber INT OUTPUT,
    @ErrMsg VARCHAR(MAX) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @IsActive BIT;
        DECLARE @UserId UNIQUEIDENTIFIER;

        SELECT @IsActive = IsActive
        FROM Client
        WHERE ClientKey = @ClientKey;

        IF @IsActive is null
            Begin 
                THROW 50001, 'Client does not exist', 1;
            end 

        If @IsActive <> 1
            Begin
                THROW 50002, 'Client already exists but is deactivated. Please contact support.', 1;
            end

       
        -- Check user exists
        SELECT 
           
            @UserId = UserId,
            @IsActive = IsActive
        FROM [User]
        WHERE UserEmail = @UserEmail or UserName =@UserEmail;

        IF @UserId IS NULL
            THROW 50003, 'Invalid email or password.', 1;

        -- Check active
        IF @IsActive = 0
            THROW 50004, 'User is inactive.', 1;

        -- Success → return user data
        SELECT 
            UserId,
            ClientId,
            UserCode,
            UserName,
            UserEmail,
            @ClientKey as ClientKey,
            PasswordHash,
            UserSalt,
            RoleName
        FROM [User]
        WHERE UserId = @UserId;

        SET @ErrNumber = 0;
        SET @ErrMsg = 'Login successful';

    END TRY
    BEGIN CATCH
        SET @ErrNumber = 1;
        SET @ErrMsg = ERROR_MESSAGE();

        THROW;
    END CATCH
END