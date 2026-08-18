--select * from Client

--exec sp_LoadEmployeeDropdown 'F38DA55F-DAB4-44F9-8C70-3C42CF03A095'
DROP PROCEDURE IF EXISTS sp_LoadEmployeeDropdown;
GO

CREATE PROCEDURE sp_LoadEmployeeDropdown
(
    @ClientId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ClientId,DepartmentId,DepartmentName,DepartmentCode
    FROM Department WHERE ClientId = @ClientId AND IsActive = 1
    ORDER BY DisplayOrder ASC

    SELECT ClientId,DesignationId,DesignationName,DesignationCode,DepartmentId
    FROM Designation WHERE ClientId = @ClientId AND IsActive = 1
    ORDER BY DisplayOrder ASC

    SELECT ClientId,EmployeeId,EmployeeEmail,DepartmentId FROM Employee
    where ClientId = @ClientId and ManagerId <> null  and IsActive=1 


    
END
GO