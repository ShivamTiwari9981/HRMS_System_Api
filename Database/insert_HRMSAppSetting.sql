insert into HRMSAppSetting
(
AppSettingId,
ClientId,
SettingKey,
SettingValue,
DataType,
Description,
IsActive,
CreatedAt,
CreatedBy
)
Select 
NEWID(),
ClientId,
'EnableEmailOtp',
'true',
'bool',
'',
1,
GETUTCDATE(),
UserId
from [User]


select * from HRMSAppSetting
select * from [User]