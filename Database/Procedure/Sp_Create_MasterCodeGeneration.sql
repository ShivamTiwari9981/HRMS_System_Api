--exec Sp_Create_MasterCodeGeneration 'F38DA55F-DAB4-44F9-8C70-3C42CF03A095','45626BFB-B4D4-4550-BF41-5BD11DE4A27B',0,''

DROP PROCEDURE IF EXISTS Sp_Create_MasterCodeGeneration;
GO

CREATE PROCEDURE Sp_Create_MasterCodeGeneration 

    @ClientId UNIQUEIDENTIFIER,
    @CreatedBy NVARCHAR(200),
    @Err_No INT OUTPUT,  
    @Err_Msg VARCHAR(MAX) OUTPUT 
AS  
BEGIN  
    SET NOCOUNT ON;

    BEGIN TRY  
        INSERT INTO [MasterCodeGeneration] 
        (
            MasterCodeGenerationId, ClientId,TableName,Prefix,LastNumber,
            IsActive, CreatedAt, CreatedBy
        ) 
        VALUES 
        (
           NEWID(),@ClientId,'Department','DEP',0,1,GETUTCDATE(),@CreatedBy
        ),
        (
          NEWID(),@ClientId,'Employee','EMP',0,1,GETUTCDATE(),@CreatedBy 
        ),
        (
             NEWID(),@ClientId,'User','USR',0,1,GETUTCDATE(),@CreatedBy
        )
        SET @Err_Msg = 'Master Code generation Created';  
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