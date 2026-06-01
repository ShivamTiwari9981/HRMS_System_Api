DECLARE @JsonData NVARCHAR(MAX) = N'
{
  "ClientId": "b1f4d2a8-8c9d-4d2f-a5f6-123456789001",
  "EmployeeId": "046a463d-8ced-4df4-a715-7bb9e6e8ea10",
  "EmployeeCode": "",
  "FirstName": "Rahul",
  "LastName": "Sharma",
  "EmployeeEmail": "rahulsharma@company.com",
  "Phone": "9876543210",
  "DepartmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "DesignationId": "ce44d3cb-76ca-410a-aa3d-ccc880d9d1c1",
  "JoiningDate": "2026-01-15T09:00:00Z",
  "BirthDate": "1995-08-20T00:00:00Z",
  "Gender": 1,
  "AddressLine1": "123 Rajpur Road",
  "AddressLine2": "Near Clock Tower",
  "CountryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "StateId": "fed709d2-9549-4fd8-bf3a-ef5a841cf29a",
  "CityId": "325d936a-6927-4793-8b51-b9971aa0245e",
  "PostalCode": "248001",
  "EmergencyContact": "9876500000",
  "ManagerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "IsLoginUser": true,
  "Salary": {
    "SalaryId": "11111111-2222-3333-4444-555555555555",
    "EmployeeId": "046a463d-8ced-4df4-a715-7bb9e6e8ea10",
    "BasicSalary": 50000,
    "HRA": 10000,
    "Allowance": 5000,
    "Deduction": 2000,
    "NetSalary": 63000,
    "EffectiveFrom": "2026-01-15T00:00:00Z",
    "IsCurrent": true
  },
  "User": {
    "ClientId": "b1f4d2a8-8c9d-4d2f-a5f6-123456789001",
    "UserCode": "",
    "UserName": "rahul.sharma",
    "PasswordHash": "rahul@123",
    "UserSalt": "test",
    "UserEmail": "rahul.sharma@company.com",
    "Phone": "9876543210",
    "ProfileImagePath": "/uploads/profile/rahul.jpg",
    "FailedLoginAttempts": 0,
    "LockoutEnd": null,
    "IsLocked": false,
    "IsCompanyProfileCreated": true,
    "EmployeeId": "b1f4d2a8-8c9d-4d2f-a5f6-123456789001"
}
}';
--SELECT ISJSON(@JsonData);


  SELECT JSON_QUERY(@JsonData, '$.User') AS UserJson;

    --SELECT
    --JSON_VALUE(@JsonData, '$.User') AS [User];

--DECLARE @Err_No INT,
--        @Err_Msg VARCHAR(MAX);

--EXEC sp_AddEmployee
--    @JsonData = @JsonData,
--    @CreatedBy = '45626BFB-B4D4-4550-BF41-5BD11DE4A27B',
--    @Err_No = @Err_No OUTPUT,
--    @Err_Msg = @Err_Msg OUTPUT;

--SELECT @Err_No, @Err_Msg;