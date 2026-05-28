--select * from MasterCodeGeneratio
DROP PROCEDURE IF EXISTS sp_GenerateMasterCode;
go
CREATE  PROCEDURE sp_GenerateMasterCode 
@ClientId UNIQUEIDENTIFIER,
@TableName NVARCHAR(100),
@CreatedBy Nvarchar(200),
@ErrNo int output,
@Msg VARCHAR(MAX) output
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix NVARCHAR(20);
    DECLARE @LastNumber INT;
    DECLARE @NewNumber INT;

    Begin try 

        SELECT 
            @LastNumber = LastNumber,
            @Prefix = Prefix
            FROM MasterCodeGeneration WITH (UPDLOCK, HOLDLOCK)
            WHERE ClientId = @ClientId 
            AND TableName = @TableName 
            AND IsActive = 1;

        -- If not exists → insert
        IF @LastNumber IS NULL
        BEGIN
            SET @Prefix = dbo.fn_GetPrefix(@TableName)
            SET @NewNumber = 1;
            INSERT INTO MasterCodeGeneration
            (
                MasterCodeGenerationId,
                ClientId,
                TableName,
                Prefix,
                LastNumber,
                IsActive,
                CreatedAt,
                CreatedBy
            )
            VALUES
            (
                NEWID(),
                @ClientId,
                @TableName,
                @Prefix,
                @NewNumber,
                1,
                GETUTCDATE(),
                @CreatedBy
            );
        END
        ELSE
        BEGIN
            SET @NewNumber = @LastNumber + 1;

            UPDATE MasterCodeGeneration
            SET LastNumber = @NewNumber
            WHERE ClientId = @ClientId 
              AND TableName = @TableName 
              AND IsActive = 1;
        END

        -- Return generated code
         
          SET @Msg=  @Prefix + RIGHT('000' + CAST(@NewNumber AS VARCHAR), 3) ;
          SET @ErrNo=0;
    end try
    BEGIN CATCH
        -- Capture error info
        DECLARE @ErrorNumber INT = ERROR_NUMBER();
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        SET @Msg=ERROR_MESSAGE();
        SET @ErrNo=1;
        -- Return error message
        THROW;
    END CATCH
END