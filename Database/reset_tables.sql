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


--delete from Client
--delete from Menu
--delete from [Role]
--delete from RolePermission
--delete from [User]
--delete from UserRole
--delete from [User]