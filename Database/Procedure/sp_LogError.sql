DROP PROCEDURE IF EXISTS sp_LogError
Go
CREATE PROCEDURE sp_LogError
@ClientId UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO ErrorLog
    (
        ErrorLogId,
        ClientId,
        ProcedureName,
        ErrorMessage,
        ErrorLine,
        ErrorNumber,
        IsActive,
        CreatedAt,
        CreatedBy
    )
    VALUES
    (
        NEWID(),
        @ClientId,
        ERROR_PROCEDURE(),
        ERROR_MESSAGE(),
        ERROR_LINE(),
        ERROR_NUMBER(),
        1,
        GETUTCDATE(),
        @CreatedBy
    );

END