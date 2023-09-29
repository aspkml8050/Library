if not exists
(
select * from sys.tables where name='MssDbVersion'
)
begin
declare @curversion varchar(20)
set @curversion='1.1'
  create table MssDbVersion
  (
   VersionNo varchar(50),
   DateCreated DateTime
  )
  insert into MssDbVersion values (@curversion,GETDATE())

end
else
begin
  declare @vno varchar(50)
  select @vno=VersionNo from MssDbVersion

  if @vno<>@curversion --current version
  begin
    delete from MssDbVersion
	insert into MssDbVersion values (@curversion,GETDATE())
  end
end
select VersionNo,DateCreated
from MssDbVersion