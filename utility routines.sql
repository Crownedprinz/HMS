if exists (select 1 from sysobjects where xtype = 'P' and name ='update_public_table')
        drop procedure update_public_table
GO

--updating the public table use to hold information across the system
create procedure update_public_table
	@name1 			varchar(50),
	@cvalue			varchar(max),
	@p_userid		varchar(10)

with encryption

  as

begin
	delete from pub_table where userid=@p_userid and name1=@name1
	if @cvalue <> '' insert into pub_table(userid,name1,cvalue) values (@p_userid, @name1, @cvalue)

end

go


if exists (select 1 from sysobjects where xtype = 'P' and name ='col_table_create')
        drop procedure col_table_create
GO

-- create alphanumeric table with columns
create procedure col_table_create
	@tablename 		varchar(max)

with encryption

as
declare
	@str2			varchar(max)

begin
	if exists (select 1 from sysobjects where xtype = 'U' and name =@tablename) 
		execute( 'drop table [' + @tablename + ']')

	set @str2 ='CREATE TABLE [' + @tablename + '] (
	snumber varchar (50) not null default '' '' ,
	column1 varchar (max) not null default '' '' ,
	column2 varchar (max) not null default '' '' ,
	column3 varchar (max) not null default '' '' ,
	column4 varchar (max) not null default '' '' ,
	column5 varchar (max) not null default '' '' ,
	column6 varchar (max) not null default '' '' ,
	column7 varchar (max) not null default '' '' ,
	column8 varchar (max) not null default '' '' ,
	column9 varchar (max) not null default '' '' ,
	column10 varchar (max) not null default '' '' ,
	column11 varchar (max) not null default '' '' ,
	column12 varchar (max) not null default '' '' ,
	column13 varchar (max) not null default '' '' ,
	column14 varchar (max) not null default '' '' ,
	column15 varchar (max) not null default '' '' ,
	column16 varchar (max) not null default '' '' ,
	column17 varchar (max) not null default '' '' ,
	column18 varchar (max) not null default '' '' ,
	column19 varchar (max) not null default '' '' ,
	column20 varchar (max) not null default '' '' ,
	column21 varchar (max) not null default '' '' ,
	column22 varchar (max) not null default '' '' ,
	column23 varchar (max) not null default '' '' ,
	column24 varchar (max) not null default '' '' ,
	column25 varchar (max) not null default '' '' ,
	column26 varchar (max) not null default '' '' ,
	column27 varchar (max) not null default '' '' ,
	column28 varchar (max) not null default '' '' ,
	column29 varchar (max) not null default '' '' ,
	column30 varchar (max) not null default '' '' ,
	column31 varchar (max) not null default '' '' ,
	column32 varchar (max) not null default '' '' ,
	column33 varchar (max) not null default '' '' ,
	column34 varchar (max) not null default '' '' ,
	column35 varchar (max) not null default '' '' ,
	column36 varchar (max) not null default '' '' ,
	column37 varchar (max) not null default '' '' ,
	column38 varchar (max) not null default '' '' ,
	column39 varchar (max) not null default '' '' ,
	column40 varchar (max) not null default '' '' ,
	colperiod varchar(50) not null default '''',
	cust_code varchar(50) not null default '''',
	cust_date varchar(50) not null default '''',
	key_column varchar (max) not null default '' '' ,
	colseq int not null default 0
)'

	execute (@str2)    

End 

go

if exists (select 1 from sysobjects where xtype = 'P' and name = 'table_text_create') 
        drop procedure table_text_create
GO

-- create  table with columns for full st01 needs
create procedure table_text_create
	@tablename 		varchar(max)

with encryption

as
declare
	@str2			varchar(max)

begin
	if exists (select 1 from sysobjects where xtype = 'U' and name =@tablename) 
		execute( 'drop table [' + @tablename + ']')

	execute col_table_create @tablename
	set @str2 = 'alter table [' + @tablename + ']  add
	level1_code varchar(max) not null default '''', 
	level1_name varchar(max) not null default '''', 
	level2_code varchar(max) not null default '''', 
	level2_name varchar(max) not null default '''',
	level3_code varchar(max) not null default '''', 
	level3_name varchar(max) not null default '''',
	level4_code varchar(max) not null default '''', 
	level4_name varchar(max) not null default '''',
	level5_code varchar(max) not null default '''', 
	level5_name varchar(max) not null default '''',  
	record_counter int not null default 0, 
	autoid int identity(1,1), 
	level0 varchar(max) not null default '''' ,
	colnum1 numeric(16,6) default 0 not null,
	colnum2 numeric(16,6) default 0 not null,
	colnum3 numeric(16,6) default 0 not null,
	colnum4 numeric(16,6) default 0 not null,
	colnum5 numeric(16,6) default 0 not null,
	colnum6 numeric(16,6) default 0 not null '
	execute (@str2)

End 

go
if exists (select 1 from sysobjects where xtype = 'P' and name ='delete_temp_tables')
        drop procedure delete_temp_tables
GO

-- delete all temporary tables created
create procedure delete_temp_tables
	@p_userid 			varchar(max),
	@hist_flag			integer = 0,
	@exempt_tables		varchar(max)=''

with encryption

as
declare
	@str2				varchar(max),
	@tmp_str			varchar(max),
	@tapp_str			varchar(max),
	@debug_flag			varchar(10)

begin

	--select @debug_flag=field2 from tab_train_default where default_code='00000'
	--if @debug_flag = 'Y'
		return

    delete from pub_table where userid=@p_userid and not name1 in ('printer', 'public')
    
    set @tapp_str=''
    if @hist_flag = 3
    	set @tapp_str = ' and not name in (''' + @p_userid + 'ophead'',''' + @p_userid + 'st01'') '
    else if @hist_flag = 2
    	set @tapp_str = ' and not name in (''' + @p_userid + 'sta01'') '
    else if @hist_flag = 1
    	set @tapp_str = ' and not name in (''' + @p_userid + 'tapp'') '
    else if @hist_flag = 4
    	set @tapp_str = ' and not name in (''' + @p_userid + 'tapp'',''' + @p_userid + 'tappme'',''' + @p_userid + 'tappreq'',''' + @p_userid + 'tappd'') '

-- only delete if userid not spaces
    if @p_userid <> ''
    begin
		set @str2= 'declare cur_delete cursor for select name from sysobjects where xtype = ''U'' and name like ''' + @p_userid + '%''' + @tapp_str + @exempt_tables
print 'delete ' + @str2
		execute (@str2)
		execute( 'open cur_delete')
		if @@error = 0
		begin
			fetch next from cur_delete into @tmp_str
			while (@@fetch_status = 0)
			begin
				execute( 'drop table [' + @tmp_str + ']')
				fetch next from cur_delete into @tmp_str
			end
		end
		close cur_delete
		deallocate cur_delete
	End

End 

go
if exists (select 1 from sysobjects where xtype = 'FN' and name ='pads')
        drop function pads
GO

create function Bloyd.pads
	(@col1		varchar(max))

returns varchar(max)

with encryption

as
begin

declare
	@colname 		varchar(50)


    return '''' + ltrim(rtrim(replace(@col1,'''',''''''))) + ''''

end
go


if exists (select 1 from sysobjects where xtype = 'P' and name ='delete_table')
        drop procedure delete_table
GO

-- deleting a single table
create procedure delete_table
	@tablename 	varchar(max)

with encryption

as
declare
	@str2		varchar(max)

begin

	set @str2 = @tablename
	if exists (select 1 from sysobjects where xtype = 'U' and name = @str2)
		execute( 'drop table [' + @str2 + ']')
    
End 

go

if exists (select 1 from sysobjects where xtype = 'FN' and name ='date_yyyymmdd')
        drop function date_yyyymmdd
GO

create function Bloyd.date_yyyymmdd
	(@date1		varchar(10))

returns varchar(10)

with encryption

as
begin

declare
	@date2			varchar(10),
	@dd				varchar(2),
	@mm				varchar(2),
	@yyyy			varchar(4),
	@yy1			varchar(2)

    set @date2 = Replace(@date1, '/', '')
	if len(@date2) = 5 or len(@date2) = 7 set @date2='0' + @date2
	if len(@date2) = 6
	begin
		select @yy1=  year(getdate())
		set @date2 = substring(@date2,1,4) + @yy1 + substring(@date2,5,2)
	end

    set @dd = substring(@date2, 1, 2)
    set @mm = substring(@date2, 3, 2)
    set @yyyy = substring(@date2, 5, 4)
    
    return @yyyy + @mm + @dd

End
go



if exists (select 1 from sysobjects where xtype = 'FN' and name ='date_ddmmyyyy')
        drop function date_ddmmyyyy
GO

create function Bloyd.date_ddmmyyyy
	(@date1		varchar(10))

returns varchar(10)

with encryption

as
begin

declare
	@date2			varchar(10),
	@dd				varchar(2),
	@mm				varchar(2),
	@yyyy			varchar(4),
	@yy1			varchar(2)

    set @date2 = Replace(@date1, '/', '')

    set @dd = substring(@date2, 7, 2)
    set @mm = substring(@date2, 5, 2)
    set @yyyy = substring(@date2, 1, 4)
    
    return @dd + @mm + @yyyy	

End
go


if exists (select 1 from sysobjects where xtype = 'FN' and name ='date_out')
        drop function date_out
GO

create function Bloyd.date_out
	(@date1		varchar(10))

returns varchar(10)

with encryption

as
begin

declare
	@date2			varchar(10),
	@dd				varchar(2),
	@mm				varchar(2),
	@yyyy			varchar(4)

	if rtrim(@date1) = '' or len(@date1) <> 8 or patindex('%/%',@date1) > 0
		return @date1

    set @dd = substring(@date1, 7, 2)
    set @mm = substring(@date1, 5, 2)
    set @yyyy = substring(@date1, 1, 4)
    
    return @dd + '/' + @mm + '/' + @yyyy

End
go

if exists (select 1 from sysobjects where xtype = 'P' and name ='drop_column')
        drop procedure drop_column
GO

--drop a column by dropping all constrains related to the field
create procedure drop_column
	@table_name		varchar(max),
	@col_name		varchar(max)

with encryption

  as

declare
	@str1				varchar(max)

begin

	select @str1 = 'ALTER TABLE [' + @table_name + '] drop constraint ' + d.name 
	 from sys.tables t
	  join    sys.default_constraints d  on (d.parent_object_id = t.object_id)
	  join    sys.columns c  on (c.object_id = t.object_id and c.column_id = d.parent_column_id)
	 where t.name = @table_name
	  and c.name = @col_name

	  execute(@str1)
	  
	  select @str1 = 'alter table [' + @table_name + '] drop column [' + @col_name + ']'
	  execute(@str1)

end

go

if exists (select 1 from sysobjects where xtype = 'P' and name ='vw_user')
        drop view vw_user
GO
 alter view [dbo].[vw_user] as
SELECT 'local' type1,*, Bloyd.find_name('2','',staff_id) hvalue FROM user_table union all
SELECT 'local' type1,'ADMIN','','','','','','','','', 'ADMIN' hvalue 
GO

if exists (select 1 from sysobjects where xtype = 'P' and name ='find_name')
        drop function find_name
GO
alter function Bloyd.find_name(
	@type1		varchar(05),
	@pcode		varchar(10),
	@rcode		varchar(20)
)
	
	returns varchar(100)

	as
	begin
	declare
	@name2			varchar(100),
	@flagc			varchar(50)

	set @name2=''
	if @type1='1'
		select @name2 =c4 from all_select_query where c1=@pcode and c2=@rcode
	else if @type1 = '2'
	begin
		select @name2 =first_name+ ' '+surname from staff_table where staff_id=@rcode 
	end
	else if @type1 = '3'
	begin
	select @name2 =hvalue from vw_user where [user_id]=@rcode 
	
	end
	else if @type1='4'
		select @name2 =desc1 from msg_file where para_code=@pcode and id_code=@rcode
	return @name2

	end
	go

if exists (select 1 from sysobjects where xtype = 'P' and name = 'initial_tables') 
        drop procedure initial_tables
GO

create procedure initial_tables
	@p_userid				varchar(max)
	
with encryption

as
declare
	@str2					varchar(max)

begin

    exec delete_temp_tables @p_userid=@p_userid
	delete from pub_table where userid=@p_userid

End

go