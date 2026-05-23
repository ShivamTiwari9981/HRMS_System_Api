SELECT 
    TABLE_SCHEMA,
    TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME


select * from Client
select * from Menu
select * from [Role]
select * from RolePermission
select * from [User]
select * from UserRole
select * from [User]


delete from Client
--delete from Menu
delete from [Role]
delete from RolePermission
delete from UserRole
--delete from [User]


update [User] set IsCompanyProfileCreated =0, clientId= null

SELECT * FROM MENU WHERE ParentMenuId IS NULL

UPDATE MENU SET IsVisible = 0 WHERE ParentMenuId IS NULL AND MenuName NOT IN 
(
'Dashboard','Access Control','Reports','Master Management','Settings'
)

SELECT * FROM MENU WHERE ParentMenuId IS NULL AND MenuName NOT IN 
(
'Dashboard','Access Control','Reports','Master Management','Settings'
)