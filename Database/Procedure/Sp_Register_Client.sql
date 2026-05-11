DROP PROCEDURE IF EXISTS Sp_Register_Client;
GO

CREATE PROCEDURE Sp_Register_Client
(
    @ClientId UNIQUEIDENTIFIER,
    @ClientKey VARCHAR(10),
    @ClientName VARCHAR(200),
    @CompanyName VARCHAR(200),
    @CompanyLogo VARCHAR(200) = NULL,
    @Domain VARCHAR(200),
    @ContactPerson VARCHAR(200),
    @CompanyEmail VARCHAR(200),
    @Phone VARCHAR(20),
    @ExpiryDate DATETIME = NULL,
    @GSTNumber VARCHAR(50) = NULL,
    @Address VARCHAR(500) = NULL,
    @UpdatedBy UNIQUEIDENTIFIER,
    @Err_No INT OUTPUT,
    @Err_Msg VARCHAR(MAX) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS
        (
            SELECT 1
            FROM Client
            WHERE ClientId = @ClientId
              AND ClientKey = @ClientKey
        )
        BEGIN
            SET @Err_No = 1;
            SET @Err_Msg = 'Client does not exist';

            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS
        (
            SELECT 1
            FROM Client
            WHERE ClientId = @ClientId
              AND ClientKey = @ClientKey
              AND ISNULL(IsActive,0) = 0
        )
        BEGIN
            SET @Err_No = 1;
            SET @Err_Msg = 'Client is deactivated. Please contact support';

            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Client
        SET
            ClientName = @ClientName,
            CompanyName=@CompanyName,
            CompanyLogo = @CompanyLogo,
            [Domain] = @Domain,
            ContactPerson = @ContactPerson,
            CompanyEmail = @CompanyEmail,
            Phone = @Phone,
            ExpiryDate = @ExpiryDate,
            GSTNumber = @GSTNumber,
            [Address] = @Address,
            IsCompanyProfileCreated = 1,
            UpdatedAt = GETUTCDATE(),
            UpdatedBy = @UpdatedBy
        WHERE ClientId = @ClientId
          AND ClientKey = @ClientKey
          AND IsActive = 1;

        COMMIT TRANSACTION;
        SET @Err_No = 0;
        SET @Err_Msg = 'Client profile created successfully';

    END TRY

    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @Err_No = ERROR_NUMBER();
        SET @Err_Msg = ERROR_MESSAGE();
    END CATCH
END
GO