--drop database db_hrms
--create database db_hrms
--use db_hrms
CREATE TABLE [dbo].[Client](
	[ClientId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[ClientCode] [nvarchar](20) NOT NULL UNIQUE,
	[ClientName] [nvarchar](200) NOT NULL UNIQUE,
	[CompanyName] [nvarchar](200) NOT NULL,
	[CompanyLogo] [nvarchar](max) NULL,
	[Domain] [nvarchar](200) NOT NULL,
	[ContactPerson] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NOT NULL UNIQUE,
	[Phone] [nvarchar](20) NOT NULL UNIQUE,
	[ExpiryDate] [datetime2](7) NOT NULL,
	[GSTNumber] [NVARCHAR](50) NULL,
	[Address] [nvarchar](200) NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
)
CREATE TABLE [dbo].[Department](
	[ClientId] [uniqueidentifier] NOT NULL,
	[DepartmentId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[DepartmentCode] [nvarchar](20) NOT NULL UNIQUE,
	[DepartmentName] [nvarchar](200) NOT NULL UNIQUE,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Department FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
	)

	CREATE TABLE [dbo].[User](
	[ClientId] [uniqueidentifier] NOT NULL,
	[UserId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[UserCode] [nvarchar](20) NOT NULL UNIQUE,
	[FullName] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL UNIQUE,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[UserSalt] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](200) NOT NULL UNIQUE,
	[Phone] [nvarchar](20) NOT NULL UNIQUE,
	[ProfileImagePath] [nvarchar](max) NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_User FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
)

CREATE TABLE [dbo].[Employee](
	[ClientId] [uniqueidentifier] NOT NULL,
	[EmployeeId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[EmployeeCode] [nvarchar](20) NOT NULL UNIQUE,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](200) NOT NULL unique,
	[Phone] [nvarchar](20) NULL unique,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[Designation] [nvarchar](200) NOT NULL,
	[ProfileImagePath] [nvarchar](max) NULL,
	[DateOfJoining] [datetime2](7) NULL,
	[Salary] [decimal](18, 2) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Employee FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
	CONSTRAINT FK_Department_Employee FOREIGN KEY ([DepartmentId]) REFERENCES [Department]([DepartmentId]),
	CONSTRAINT FK_User_Employee FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
)

CREATE TABLE [dbo].[Attendance](
    [ClientId] [uniqueidentifier] NOT NULL,
	[AttendanceId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[AttendanceCode] [nvarchar](20) NOT NULL UNIQUE,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[CheckInTime] [datetime2](7) NOT NULL,
	[CheckOutTime] [datetime2](7) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Attendance FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
	CONSTRAINT FK_Employee_Attendance FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId])
	)


CREATE TABLE [dbo].[Leave](
	[ClientId] [uniqueidentifier] NOT NULL,
	[LeaveId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Reason] [nvarchar](500) NULL,
	[LeaveStatus] [int] NOT NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Leave FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
	CONSTRAINT FK_Employee_Leave FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId])

)

CREATE TABLE [dbo].[MasterCodeGeneration](
	[ClientId] [uniqueidentifier] NOT NULL,
	[MasterCodeGenerationId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[TableName] [nvarchar](100) NOT NULL unique,
	[Prefix] [nvarchar](3) NOT NULL,
	[LastNumber] [int] NOT NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_MasterCodeGeneration FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
)

CREATE TABLE Menu (
	[ClientId] [uniqueidentifier] NOT NULL,
    MenuId int NOT NULL IDENTITY(1,1) primary key,
    ParentMenuId int NULL default null,
    MenuName NVARCHAR(200) NOT NULL unique,
    MenuIcon NVARCHAR(50) NOT NULL,
    RouterLink NVARCHAR(200) NOT NULL,
    DisplayOrder int,
    [IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
    CONSTRAINT FK_Menu_Parent FOREIGN KEY (ParentMenuId) REFERENCES Menu(MenuId),
	CONSTRAINT FK_Client_Menu FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
);


CREATE TABLE [dbo].[Payroll](
	[ClientId] [uniqueidentifier] NOT NULL,
	[PayrollId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[BasicSalary] [decimal](18, 2) NOT NULL,
	[Bonus] [decimal](18, 2) NOT NULL,
	[Deductions] [decimal](18, 2) NOT NULL,
	[NetSalary] [decimal](18, 2) NOT NULL,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Payroll FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
	CONSTRAINT FK_Employee_Payroll FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId]),
	)

CREATE TABLE [dbo].[Permission](
	[ClientId] [uniqueidentifier] NOT NULL,
	[PermissionId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[PermissionName] [nvarchar](300) NOT NULL unique,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Permission FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
)

CREATE TABLE [dbo].[Role](
	[ClientId] [uniqueidentifier] NOT NULL,
	[RoleId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[RoleName] [nvarchar](200) NULL UNIQUE,
	[IsActive] [bit] NULL default 1,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
	CONSTRAINT FK_Client_Role FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]),
)



CREATE TABLE UserRoles (
    [ClientId] [uniqueidentifier] NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,

    CONSTRAINT PK_User_Roles PRIMARY KEY (UserId, RoleId),

    CONSTRAINT FK_UserRoles_User 
        FOREIGN KEY (UserId) REFERENCES [User](UserId),

    CONSTRAINT FK_UserRoles_Role 
        FOREIGN KEY (RoleId) REFERENCES Role(RoleId),

    CONSTRAINT FK_UserRoles_Client
        FOREIGN KEY ([ClientId]) REFERENCES Client([ClientId])
);

CREATE TABLE RolePermissions (
	[ClientId] [uniqueidentifier] NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,
    PermissionId UNIQUEIDENTIFIER NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsSynced] [bit] NULL,
    PRIMARY KEY (ClientId,RoleId, PermissionId),

    FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
    FOREIGN KEY (PermissionId) REFERENCES Permission(PermissionId),
	FOREIGN KEY (ClientId) REFERENCES Permission(PermissionId)
);