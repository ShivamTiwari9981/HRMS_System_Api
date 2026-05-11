DROP PROCEDURE IF EXISTS Sp_Sign_Up_Client;
GO

CREATE PROCEDURE Sp_Sign_Up_Client  

    @ClientId UNIQUEIDENTIFIER,
    @ClientKey VARCHAR(10),
    @CreatedBy NVARCHAR(200),
    @Err_No INT OUTPUT,  
    @Err_Msg VARCHAR(MAX) OUTPUT 
AS  
BEGIN  
    SET NOCOUNT ON;  

    BEGIN TRY  
        DECLARE @IsActive BIT;

        SELECT @IsActive = IsActive
        FROM Client
        WHERE ClientKey = @ClientKey;

        IF @IsActive IS NOT NULL
        BEGIN
            IF @IsActive = 1
                THROW 50001, 'Client already exists.', 1;
            ELSE
                THROW 50002, 'Client already exists but is deactivated. Please contact support.', 1;
            END

        -- Insert User
        INSERT INTO [Client] 
        (
            ClientId,ClientKey,IsCompanyProfileCreated,
            IsActive, CreatedAt, CreatedBy
        ) 
        VALUES 
        (
            @ClientId,@ClientKey,0, 1,GETUTCDATE(), @CreatedBy
        );  
        SET @Err_Msg = 'Comapny Created';  
        SET @Err_No = 0;  

    END TRY  
    BEGIN CATCH  

        SET @Err_Msg = ERROR_MESSAGE();  
        SET @Err_No = 1;  

        THROW;
    END CATCH  
END