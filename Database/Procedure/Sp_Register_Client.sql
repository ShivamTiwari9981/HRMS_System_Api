DROP PROCEDURE IF EXISTS Sp_Register_Client;
GO
CREATE PROCEDURE Sp_Register_Client  
(  
    @CompanyName VARCHAR(200),  
    @CompanyEmail VARCHAR(200),  
    @Phone VARCHAR(20),  
    @SubscriptionStartDate DATETIME,  
    @SubscriptionEndDate DATETIME,  
    @GSTNumber VARCHAR(50) = NULL,  
    @CompanyType NUMERIC,  
    @SubscriptionPlanId UNIQUEIDENTIFIER,  
    @Address VARCHAR(500) = NULL,  
    @Client_Id UNIQUEIDENTIFIER OUTPUT,  
    @CreatedBy UNIQUEIDENTIFIER,  
    @Err_No INT OUTPUT,  
    @Err_Msg VARCHAR(MAX) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
    SET XACT_ABORT ON;  
  
    DECLARE @ClientId UNIQUEIDENTIFIER =NEWID();  
    DECLARE @RoleId UNIQUEIDENTIFIER =NEWID();  
    DECLARE @Per_Err_No INT;   
    DECLARE @Per_Err_Msg VARCHAR(MAX);  
    DECLARE @Role_Err_No INT;   
    DECLARE @Role_Err_Msg VARCHAR(MAX) ;  
  
    BEGIN TRY  
        BEGIN TRANSACTION;  
        IF EXISTS  
        (  
            SELECT 1  
            FROM Client  
            WHERE CompanyEmail = @CompanyEmail  
        )  
        BEGIN  
            SET @Err_No = 1;  
            SET @Err_Msg = 'Client is already exist';  
  
            ROLLBACK TRANSACTION;  
            RETURN;  
        END  
  
        IF EXISTS  
        (  
            SELECT 1  
            FROM Client  
            WHERE CompanyName = @CompanyName   
              AND CompanyEmail = @CompanyEmail  
              AND ISNULL(IsActive,0) = 0  
        )  
        BEGIN  
            SET @Err_No = 1;  
            SET @Err_Msg = 'Client is deactivated. Please contact support';  
  
            ROLLBACK TRANSACTION;  
            RETURN;  
        END  
        select * from Client  
        Insert into Client  
        (  
            ClientId,  
            CompanyName,  
            CompanyEmail,  
            Phone,  
            SubscriptionStartDate,  
            SubscriptionEndDate,  
            GSTNumber,  
            CompanyType,  
            SubscriptionPlanId,  
            [Address],  
            IsActive,  
            CreatedAt,  
            CreatedBy,  
            IsSynced  
        )  
        VALUES   
        (  
            @ClientId,  
            @CompanyName,  
            @CompanyEmail,  
            @Phone,  
            @SubscriptionStartDate,  
            @SubscriptionEndDate,  
            @GSTNumber,  
            @CompanyType,  
            @SubscriptionPlanId,  
            @Address,  
            1,  
            GETUTCDATE(),  
            @CreatedBy,  
            0  
        );    
  
        -------- Update User ---------------  
  
        Update [User] set ClientId = @ClientId, IsCompanyProfileCreated=1,  
        UpdatedAt = GETUTCDATE(), UpdatedBy =@CreatedBy  
        where UserId = @CreatedBy  
  
  
        -------- Create Role ---------------  
  
         EXEC Sp_Create_Role   
            @ClientId = @ClientId,  
            @RoleId = @RoleId,  
            @RoleName='CompanyAdmin',  
            @IsSystemRole=1,  
            @CreatedBy=@CreatedBy,  
            @Err_No = @Role_Err_No OUTPUT,  
            @Err_Msg = @Role_Err_Msg OUTPUT;  
  
            IF @Role_Err_No <> 0  
            THROW 50001, @Role_Err_Msg, 1;  
  
  
         -------- Create User Role ---------------  
  
         EXEC Sp_Create_UserRole   
            @ClientId = @ClientId,  
            @RoleId = @RoleId,  
            @CreatedBy=@CreatedBy,  
            @Err_No = @Per_Err_No OUTPUT,  
            @Err_Msg = @Per_Err_Msg OUTPUT;  
  
         IF @Per_Err_No <> 0  
            THROW 50002, @Per_Err_Msg, 1;  

  
  
         -------- Create Permission ---------------  
         EXEC Sp_Create_Permission   
            @ClientId = @ClientId,  
            @CreatedBy=@CreatedBy,  
            @Err_No = @Per_Err_No OUTPUT,  
            @Err_Msg = @Per_Err_Msg OUTPUT;  
  
         IF @Per_Err_No <> 0  
            THROW 50003, @Per_Err_Msg, 1;  
           
  
         --   ------ Create Assign Permission  ---------------  
             EXEC sp_Signup_AssignRolePermissionsToCompanyAdmin   
                @ClientId = @ClientId,  
                @RoleId = @RoleId,  
                @CreatedBy=@CreatedBy,  
                @Err_No = @Per_Err_No OUTPUT,  
                @Err_Msg = @Per_Err_Msg OUTPUT; 
                
            IF @Per_Err_No <> 0  
            THROW 50004, @Per_Err_Msg, 1;  

             -------- Create User Role ---------------  
  
         EXEC Sp_Create_MasterCodeGeneration   
            @ClientId = @ClientId,  
            @CreatedBy=@CreatedBy,  
            @Err_No = @Per_Err_No OUTPUT,  
            @Err_Msg = @Per_Err_Msg OUTPUT;  
  
         IF @Per_Err_No <> 0  
            THROW 50005, @Per_Err_Msg, 1;  
  
  
  
        COMMIT TRANSACTION;  
        SET @Err_No = 0;  
        set @Client_Id =@ClientId;  
        SET @Err_Msg = 'Company created successfully';  
  
    END TRY  
  
    BEGIN CATCH  
        IF @@TRANCOUNT > 0  
            ROLLBACK TRANSACTION;  
  
        SET @Err_No = ERROR_NUMBER();  
        SET @Err_Msg =ERROR_PROCEDURE() + ERROR_MESSAGE();  
          
        THROW;  
    END CATCH  
END  