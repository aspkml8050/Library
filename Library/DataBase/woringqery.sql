--select a.name,b.name from sys.tables a join sys.columns b on a.object_id=b.object_id and b.name like '%edit%'

/*
insert into tmpbc
select  b.column_id, 'tx'+ b.name from sys.tables a join sys.columns b on a.object_id=b.object_id and a.name='Bookseries'
order by b.column_id
select  'bookaccn.'+ b.name+'=tx'+b.name+'.Text;' from sys.tables a join sys.columns b on a.object_id=b.object_id and a.name='Bookaccessionmaster'
create table tmpbc
(
column_id int,
colnm varchar(30)
)
delete from tmpbc
select * from tmpbc
create table tmphtmld
(
rowd varchar(800)
)
*/

declare @sn int,@tot int
,@str varchar(3000)
set @sn=2
select @tot=count(*) from tmpbc
declare @coln varchar(50)
while @sn<@tot
begin
   select @coln=colnm from tmpbc where column_id=@sn
   insert into tmphtmld values
   ('<div class="row">' )
   insert into tmphtmld values
   ('  <div class="col-md-2">' )
   insert into tmphtmld values
   ( '    <label for="'+@coln+'" class="label"    >X</label>' )
   insert into tmphtmld values
   ('  </div>' )
   insert into tmphtmld values
   ('  <div class="col-md-4">' )
   insert into tmphtmld values
   ( '    <asp:TextBox ID="'+@coln+'" runat="server" cssClass="form-control"    ></asp:TextBox>' )
   insert into tmphtmld values
   ('  </div>' )
  set @sn +=1
   if @sn <=@tot
   begin
	select @coln=colnm from tmpbc where column_id=@sn
   insert into tmphtmld values
   ('  <div class="col-md-2">' )
   insert into tmphtmld values
   ( '    <label for="'+@coln+'" class="label"    >X</label>' )
   insert into tmphtmld values
   ('  </div>' )
   insert into tmphtmld values
   ('  <div class="col-md-4">' )
   insert into tmphtmld values
   ( '    <asp:TextBox ID="'+@coln+'" runat="server" cssClass="form-control"    ></asp:TextBox>' )
   insert into tmphtmld values
   ('  </div>' )
   insert into tmphtmld values
   ('</div>' )
   end
/*   else
   begin
   insert into tmphtmld values
   ('  <div class="col-md-2">' )
   insert into tmphtmld values
   ( '    ' )
   insert into tmphtmld values
   ('  </div>' )
   insert into tmphtmld values
   ('  <div class="col-md-4">' )
   insert into tmphtmld values
   ( '    ' )
   insert into tmphtmld values
   ('  </div>' )
   insert into tmphtmld values
   ('</div>' )

   end
 */
 set @sn +=1
  
end

--select * from tmphtmld 
--truncate table tmphtmld
--select * from bookauthor