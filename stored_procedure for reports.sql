
if exists (select 1 from sysobjects where xtype = 'P' and name ='preport')
        drop procedure [preport]
GO
create procedure [dbo].[preport]
	@select_str					varchar(500),
	@p_userid					varchar(10)

--with encryption

as
declare
	@str1						varchar(max),
	@str1n						nvarchar(max),
	@code1						varchar(50),
	@code2						varchar(50),
	@num1						varchar(50),
	@num2						varchar(50),
	@counter1					int,
	@counter2					int,
	@col_values 				varchar(500),
	@col_string 				varchar(500),
	@str2						varchar(max),
	@table_name					varchar(50),
	@table_size					varchar(50),
	@table_st01					varchar(50),
	@s_report					varchar(50),
	@colnames					varchar(max),
	@code_str					varchar(50),
	@num_str					varchar(50),
	@read_str					varchar(max),
	@read_str2					varchar(max),
	@report_name				varchar(50),
	@fname						varchar(50),
	@shname						varchar(max),
	@orderby					varchar(500),
	@temp_val1					varchar(500),
	@pflag						int,
	@datet						varchar(20)

begin

	set @s_report = substring(@select_str,1,10)
	set @code1 = substring(@select_str,11,10)
	set @code2 = substring(@select_str,21,10)
	set @num1 = substring(@select_str,31,10)
	set @num2 = substring(@select_str,41,10)
	execute update_public_table 'qcode','ax',@p_userid
	execute update_public_table 'qreport',@s_report,@p_userid
	set @datet = ''
	if @s_report = '04' or @s_report = '05R'
	set @datet = 'checkin_date'
	else if SUBSTRING(@s_report,1,2) = '05'
	set @datet = 'transaction_date'
	else 
	set @datet = 'created_date'
    --select @report_name=report_name1,@table_name=report_name3,@code_str=report_name4,@colnames=report_name8,@read_str2=report_name6,
    --@num_str=report_name9,@orderby=report_name5 from tab_soft where para_code='REPCODE' and report_code=@s_report
    
	 select @report_name=desc1,@table_name=desc2,@code_str=desc3,@colnames=desc4,@read_str2=desc5,
    @num_str=desc6,@orderby=desc7 from msg_file where para_code='REPCODE' and id_code=@s_report

	select @shname='[' + schema_name() + '].'
	set @read_str=replace(@read_str2,'dbo.',@shname)

    set @str2 = ''
	set @pflag=patindex('%where%', @read_str)
	if @pflag =0
		set @str2=' where ''1''=''1'' '
				
    if @code1 <> ''
		set @str2 = @str2 + ' and ' + @code_str + ' between ' + Bloyd.pads(@code1) + ' and ' + Bloyd.pads(@code2)
	if @num1 <> ''
		set @str2 = @str2 + ' and '+@datet+' between ' + Bloyd.pads(@num1) + ' and ' + Bloyd.pads(@num2)
	if @s_report = '04'
	set @str2 = @str2 + ' union all ' + @read_str + ' where flag = ''I'' and guest_id not in (select guest_id from checkin_table where checkin_date between '+Bloyd.pads(@num1)+' and '+Bloyd.pads(@num2)+') '
    
	set @read_str = @read_str + ' ' + @str2    

	set @table_name = @p_userid + 'ophead'
    execute col_table_create @table_name
	set @table_size = @p_userid + 'size'
    execute col_table_create @table_size
	set @table_name = '[' + @table_name + ']'
	set @pflag= len(@colnames)

		--set @str1 = 'insert into ' + @table_name + '(column1,column2, column3, column4,column5,column6,column11,column12) '
		--set @str1 = @str1 + ' select rep_name1, report_name1, report_name2,report_name3,report_name4,numeric_ind,report_name8,report_name6 from tab_soft '
		--set @str1 = @str1 + ' where para_code=''RPARA'' and report_code like ''' + @colnames + ''' and report_name6 <> '''' '
		set @str1 = 'insert into ' + @table_name + '(column1,column2, column3, column4,column5,column6,column11,column12) '
		set @str1 = @str1 + ' select desc1, desc2, desc3,desc4,desc5,desc6,desc7,desc8 from msg_file '
		set @str1 = @str1 + ' where para_code=''RPARA'' and id_code like ''' + @colnames + ''' and desc8 <> '''' '
		
	execute (@str1)
print 'others ' + @str1
						
-- if entries, copy to st1b
		set @table_st01 = @p_userid + 'st01'		
		execute table_text_create @table_st01
		set @table_st01 = '[' + @table_st01 + ']'

		--execute set_tables_col 'ax', @s_report, @p_userid, @col_string output, @col_values output

	set @str1n = 'select @strg='''''' para_code '''''' + (select '','' + column12 from ' + @table_name + ' order by cast(column1 as int) for xml path(''''))  '
	execute sp_executesql @str1n, N'@strg varchar(max) output', @strg=@col_values output
	
print @col_values

	set @str1n = 'select @strg= ''snumber '' + (select '',column'' + column1 from ' + @table_name + ' order by cast(column1 as int) for xml path('''')) '
	execute sp_executesql @str1n, N'@strg varchar(max) output', @strg=@col_string output
	
print @col_string
		
	--set @str1 = 'alter table ' + @table_st01 + ' add rectr int identity '
	--execute(@str1)
	set @str1 = 'insert into ' + @table_st01 + ' (' + @col_string + ') select ' + @col_values + ' from (' + @read_str + ') q ' + @orderby
print @str1		
	execute (@str1)
	print '1'
	execute update_public_table 'displayv','0',@p_userid
-- we need to return back to adjust dates to client's zone and then run the next proced

--    set @str1 = 'select column1 from ' + @table_name + ' where column5=''DL'' '
--print 'column type ' + @str1
--    execute('declare rec1 cursor for ' + @str1 )
--    execute('open rec1')
--    if @@error = 0
--    begin
--        fetch next from rec1 into @temp_val1
--        while (@@fetch_status = 0)
--		begin
--			set @read_str = 'column'+@temp_val1
--			set @str2 = 'update ' + @table_st01 + ' set ' + @read_str + ' = Bloyd.locdate(' + @read_str  + ',0) '
--			fetch next from rec1 into @temp_val1
--		end
--    end
--    close rec1
--    deallocate rec1

	--set @str1 = 'alter table ' + @table_st01 + ' drop column rectr '
 --   execute(@str1)

	execute insert_table_headers @p_userid

end
go


if exists (select 1 from sysobjects where xtype = 'P' and name = 'insert_table_headers') 
        drop procedure insert_table_headers
GO

create procedure insert_table_headers
	@p_userid					varchar(10)

--with encryption

as
declare
	@str1						varchar(max),
	@str1n						nvarchar(max),
	@col1						varchar(50),
	@col2						varchar(50),
	@counter1					int,
	@counter2					int,
	@temp_value					varchar(50),
	@table_name					varchar(50),
	@str2						varchar(max),
	@upd_string					varchar(max),
	@str41						varchar(max),
	@str42						varchar(max),
	@str43						varchar(max),
	@str44						varchar(max),
	@str45						varchar(max),
	@str46						varchar(max),
	@str47						varchar(max),
	@str48						varchar(max),
	@level_str					varchar(max),
	@add_string					varchar(max),
	@add_string1				varchar(max),
	@table_size					varchar(50),
	@colname					varchar(50),
	@colname2					varchar(50),
	@colname3					varchar(50),
	@pflag						int,
	@fname						varchar(50),
	@id_flag					varchar(05),
	@psummary					varchar(10),
	@numeric_only				varchar(10),
	@pcode						varchar(10),
	@s_report					varchar(10)

begin
print 'insert_table_headers'
	
	set @table_size = '[' + @p_userid + 'st01]'
	set @table_name = '[' + @p_userid + 'ophead]'
	set @fname = @p_userid + 'sta01'
	
--formating amount fields
	set @counter1=2
	set @counter2=0
	set @upd_string=''
	set @pflag = 0
	set @str41 = ''
	set @str42 = ''
	set @str43 = ''
	set @str44 = ''
--// string for summary with distinct text
	set @str45 = ''
	set @str46 = ''
	set @str47 = ''
	set @str48 = ''
	set @numeric_only='Y'
	set @psummary=''
	
	select @pcode=cvalue from pub_table where name1='qcode' and userid=@p_userid
	select @s_report=cvalue from pub_table where name1='qreport' and userid=@p_userid
	select @psummary=cvalue from pub_table where name1='paysummary' and userid=@p_userid
	select @numeric_only=cvalue from pub_table where name1='numericonly' and userid=@p_userid
	
	--set @str1 = 'update ' + @table_name + ' set column2='''',column3=report_name2, column4=report_name3 from ' + @table_name + ' a, tab_soft b '
	--set @str1 = @str1 + ' where b.para_code=''HEXCPT'' and column7+column8=report_name1 '
	set @str1 = 'update ' + @table_name + ' set column2='''',column3=desc2, column4=desc3 from ' + @table_name + ' a, msg_file b '
	set @str1 = @str1 + ' where b.para_code=''HEXCPT'' and column7+column8=desc1 '
	execute(@str1)
	
print 'report routine'
	execute report_rtn1 @p_userid
print CAST(SYSDATETIME() AS TIME) 
	if @pcode = 'F18'
	begin
		set @str1 = 'update [' + @p_userid + 'st01] set level1_code=b.level1_code,level1_name=b.level1_name,level2_code=b.level2_code,level2_name=b.level2_name,level3_code=b.level3_code,'
		set @str1 = @str1 + ' level3_name=b.level3_name,level4_code=b.level4_code,level4_name=b.level4_name,level5_code=b.level5_code,level5_name=b.level5_name '
		set @str1 = @str1 + ' from [' + @p_userid + 'st01] a, [' + @p_userid + 'st01m] b where a.snumber=b.column1 + cast(b.colseq as varchar)'
		execute(@str1)
print @str1
	end
print 'complete report'
print CAST(SYSDATETIME() AS TIME) 
-- formatting - upd
--sizing str42 and str46
--numeric only str41, str44, str45
--both str41, str44, str43
--41-sum
--42-max len of each
--43-non adding fields (insert)
--44-numeric columns
--45-column no to delete
--46-max len of sum of numeric cols
--47-non adding fields (select)
--48-new cols for numeric only
	set @str1 = 'select column1,column5 from ' + @table_name + ' order by cast(column1 as int) '
    execute ('declare rec_doc2 cursor for ' + @str1 )
    execute( 'open rec_doc2')
    if @@error = 0 
    begin
		fetch from rec_doc2 into @col1,@col2
		while (@@fetch_status = 0)
		begin	

		set @colname = 'column' + ltrim(@col1)
		set @colname3 = 'column' + cast(cast(@col1 as int)+1 as varchar)
		set @colname2 = @colname3
		if @psummary = 'S' 
		begin
			if @numeric_only = 'Y'
				set @colname2 = 'column' + cast(@counter1 as varchar)
		end
		else
			set @colname2=@colname

		if @psummary = 'S' and @numeric_only <> 'Y' and not (@col2='A' or @col2='M' or @col2='NT')
		begin
			set @str43 = @str43 + ',' + @colname3
			set @str47 = @str47 + ',' + @colname
		end
		if @psummary = 'S' and @numeric_only = 'Y' and not (@col2='A' or @col2='M' or @col2='NT')
			set @str45= @str45 + ', ' + @col1

		else if  @col2='A' or @col2= 'M' or @col2= 'NA'
		begin
			set @upd_string = @upd_string + ',' + @colname2 + '=  convert(varchar,cast(' + @colname2 + ' as money),1) '
			set @str42 = ' max(len(convert(varchar,cast(' + @colname + ' as money),1))) +2 '
			set @str46 = @str42
			if @col2='A' or @col2='M'
			begin
				set @str44 = @str44 + ',' + @colname3
				set @str48 = @str48 + ',' + @colname2
				set @str41 = @str41 + ', sum(cast(' + @colname + ' as money)) '
				set @str46 =' len(convert(varchar,sum(cast(' + @colname + ' as money)),1))+2 '
				set @counter1=@counter1+1
			end
		end
		else if @col2='N' or @col2= 'NT'
		begin
			set @upd_string = @upd_string + ',' + @colname2 + '=  cast(cast(' + @colname2 + ' as money) as numeric(8,0)) '
			set @str42 = ' max(len(convert(varchar,cast(' + @colname + ' as money),1))) '
			set @str46 = @str42
			if @col2='NT'
			begin
				set @str44 = @str44 + ',' + @colname3
				set @str48 = @str48 + ',' + @colname2
				set @str41 = @str41 + ', sum(cast(' + @colname + ' as money)) '
				set @str46 = ' len(convert(varchar,sum(cast(' + @colname + ' as money)),1)) '
				set @counter1=@counter1+1
			end
		end
		else if  @col2='R'
		begin
			set @upd_string = @upd_string + ',' + @colname + '=  case ' + @colname + ' when '''' then ''0.0000'' else cast(cast(' + @colname + ' as numeric(8,4)) as varchar) end'
			set @str42 = ' max(len(cast(cast(' + @colname + ' as money) as numeric(8,0)))) +2 '
			set @str46 = @str42
		end
		else if  @col2='D'
		begin
			set @upd_string = @upd_string + ',' + @colname + '=  Bloyd.date_out(' + @colname + ') '
			set @str42 = ' max(len(Bloyd.date_out(' + @colname + '))) +2 '
			set @str46 = @str42
		end
		else if  @col2='P'
		begin
			set @upd_string = @upd_string + ',' + @colname + '=  Bloyd.period_out(' + @colname + ') '
			set @str42 = ' max(len(Bloyd.period_out(' + @colname + '))) +2 '
			set @str46 = @str42
		end
		else
		begin
			set @str42 = ' isnull(max(len(' + @colname + ')),0) +2  '
			set @str46 = @str42
		end



			if @str42 <> ''
			begin
				set @str1 = ' case when ' + @str42 + ' > ' + @str46  + ' then ' + @str42 + ' else ' + @str46 + ' end ' 
				set @str1=' update ' + @table_name + ' set column15 = isnull((select ' + @str1	 + ' from [' + @p_userid + 'st01] b ),0) from ' + @table_name + ' a where a.column1=' + @col1 
	print @str1
				execute(@str1)
			end
			fetch from rec_doc2 into @col1, @col2
		end
    end
    CLOSE rec_doc2
    DEALLOCATE rec_doc2

print ' numeric ' + @numeric_only
print ' summary ' + @psummary
print ' str41 ' + @str41
print ' str42 ' + @str42
print ' str43 ' + @str43
print ' str44 ' + @str44
print ' str45 ' + @str45
print ' str46 ' + @str46
print ' str47 ' + @str47
print ' str48 ' + @str48
--print @upd_string	
--	set @str1 = 'update [' + @p_userid + 'st01] set snumber=snumber ' + @upd_string
--	execute (@str1)

-- size of headers
print 'headers'
	set @str1 = 'update ' + @table_name + ' set column11= (case when len(column2) > len(column3) then case when len(column2) > len(column4) then len(column2) else len(column4) end else '
	set @str1 = @str1 + ' case when len(column3)> len(column4) then len(column3) else len(column4) end  end) + 2 from '+ @table_name
	execute(@str1)

	set @str1 = 'update ' + @table_name + ' set column11= column15 where cast(column15 as integer) > cast(column11 as integer) '
	execute(@str1)

--for 'B' columns we reset back to column6 when column5='B'
	set @str1 = 'update ' + @table_name + ' set column11= column6 where column5=''B'' '
	execute(@str1)

--summary option

	if @psummary = 'S'
	begin
		set @str1n = 'select top 1 @strg=count_array from tab_array where array_code=' + Bloyd.pads(@s_report) 
		set @str1n = @str1n + ' and para_code=''' + @pcode + 'S'' and (operand=''Y'' or source1=''Y'' or period=''Y'')  order by count_array '
		execute sp_executesql @str1n, N'@strg int output', @strg=@counter1 output
		set @counter1=isnull(@counter1,0)

print 'summary'
print @counter1
	-- create new tns and st01 with backup of old ones
	-- if counter=0 just summary page
		set @temp_value = @p_userid + 'st01x'
		execute delete_table @temp_value
		set @level_str = ',level1_code,level1_name,level2_code,level2_name,level3_code,level3_name,level4_code,level4_name,level5_code,level5_name  '
		
		set @str1 = ' select * into [' + @p_userid + 'st01x] from [' + @p_userid + 'st01]'
		execute(@str1)
		set @str1 = ' truncate table  [' + @p_userid + 'st01]'
		execute(@str1)

		set @add_string=''''
		if @counter1 <> 0
		begin
			set @add_string = 'level' + cast(@counter1 as varchar) + '_name';
			set @add_string1 = 'level' + cast(@counter1 as varchar) + '_code';

			set @level_str = replace(@level_str, ','+@add_string,'')
			set @level_str = replace(@level_str, ','+@add_string1,'')
		end

print @level_str

		if @numeric_only = 'Y'
		begin
			set @str1 = 'insert into [' + @p_userid + 'st01] (column1' + @str48 + @level_str+ ' ) '
			set @str1 = @str1 + ' select ' + @add_string + @str41 + @level_str 
			set @str1 = @str1 + ' from [' + @p_userid + 'st01x] group by level1_code,level1_name,level2_code,level2_name,level3_code,level3_name,level4_code,level4_name,level5_code,level5_name '
			execute(@str1)
print @str1

			set @str1 = 'delete from [' + @p_userid + 'ophead] where column1 in (0' + @str45 + ')'
			execute(@str1)
print @str1
			set @str1 = @p_userid + 'iden'
			execute id_table_create @str1
			set @str1 = 'insert into [' + @p_userid + 'iden] (rep_count) select cast(column1 as int) from [' + @p_userid + 'ophead] order by cast(column1 as int) '
			execute (@str1)
			set @str1 = ' update [' + @p_userid + 'ophead] set column1=id+1 from [' + @p_userid + 'ophead] a, [' + @p_userid + 'iden] b where cast(column1 as int)=rep_count '
			execute (@str1)
		end
		else
		begin
			set @str1 = 'insert into [' + @p_userid + 'st01] (column1' + @str44 + @str43 + @level_str + ' ) '
			set @str1 = @str1 + ' select ' + @add_string + @str41 + @str47 + @level_str
			set @str1 = @str1 + ' from [' + @p_userid + 'st01x] group by '
			set @str1 = @str1 + ' level1_code,level1_name,level2_code,level2_name,level3_code,level3_name,level4_code,level4_name,level5_code,level5_name  '  + @str47
			execute(@str1)
print @str1
			set @str1 = ' update [' + @p_userid + 'ophead] set column1 = cast(column1 as integer) + 1 '
			execute(@str1)
		end

print @add_string
		if @add_string <> ''''
		begin
			set @str1 = 'insert into [' + @p_userid + 'ophead] (column1,column4,column5,column6,column11) '
			set @str1 = @str1 + ' select ''1'',' + @add_string1 + ',''T'',''12'',max(len(' + @add_string +')) from [' + @p_userid + 'st01m]'
			set @str1 = @str1 + ' group by ' + @add_string1
			execute(@str1)

			if @pcode = 'F18'
			begin
				set @str1 = 'update [' + @p_userid + 'st01] set ' + @add_string + '= b.' + @add_string + ', ' + @add_string1 + '=b.' + @add_string1
				set @str1 = @str1 + ' from [' + @p_userid + 'st01] a, [' + @p_userid + 'st01m] b where a.column1=b.' + @add_string
				execute(@str1)
				execute th_flmant_summary @p_userid
			end

			set @str1 = 'update [' + @p_userid + 'st01] set ' + @add_string + '='''', ' + @add_string1 + '='''' '
			execute(@str1)
		end

--		if @str43 <> ''
--		begin
--			set @str1 = ' update [' + @p_userid + 'st01] set ' + substring(@str43, 2, 5000)
--print @str1
--			execute(@str1)
--		end

--if report then sequence by partition else no partition but all levels
		set @str1 = 'update [' + @p_userid + 'st01] set record_counter= a.rw from [' + @p_userid + 'st01] b, '
		set @str1 = @str1 + ' (select row_number() over(partition by level5_name+level4_name+level3_name+level2_name+level1_name '
		set @str1 = @str1 + ' order by level5_name+level4_name+level3_name+level2_name+level1_name) rw, autoid from [' + @p_userid + 'st01]) a where '
		set @str1 = @str1 + ' a.autoid=b.autoid '
		execute(@str1)

	end		

print @upd_string	
	set @str1 = 'update [' + @p_userid + 'st01] set snumber=snumber ' + @upd_string
	execute (@str1)

	execute view_reports @p_userid

end
go


if exists (select 1 from sysobjects where xtype = 'P' and name = 'view_reports') 
        drop procedure view_reports
GO

create procedure view_reports
	@p_userid					varchar(10)

--with encryption

as
declare
	@str1						varchar(max),
	@str2						varchar(max),
	@str1n						nvarchar(max),
	@col1						varchar(50),
	@col2						varchar(50),
	@counter1					int,
	@counter2					int,
	@reptype					varchar(10),
	@temp_value					varchar(50),
	@table_size					varchar(50),
	@fname						varchar(50),
	@repview					varchar(05)

begin
	set @fname = @p_userid + 'sta01'
	set @table_size = '[' + @p_userid + 'st01]'
	set @reptype=''
	select @reptype=cvalue from pub_table where userid=@p_userid and name1='displayv'
	print 'cvalue' + @reptype
	if @reptype = '0'
		set @repview = '2'  -- keep sta01
	else if isnull(@reptype,'') =''
		set @repview = '1'  -- keep tapp
	else
		set @repview = '3'  -- keep header and st01

print 'view report ' + @repview
	if @repview = '2'
	begin
		select @temp_value = cvalue from pub_table where userid=@p_userid and name1='tns1'
		set @temp_value=isnull(@temp_value,'''')
		if @temp_value = '' 
			set @temp_value = 'tns'

		execute delete_table @fname

		set @str1= 'create table [' + @fname +'] (report_column varchar(max) , row_seq_id int ) '
		execute (@str1)
		
		delete from pub_table where userid=@p_userid and name1='tns1'
		delete from pub_table where userid=@p_userid and name1='dflag'

		set @str1n = 'select @strg=count(0) from [' + @p_userid + 'ophead]  '
		execute sp_executesql @str1n, N'@strg int output', @strg=@counter1 output
print @counter1

		set @str1n = 'select @strg=''<td>No.</td><td> '' + (select column2 + '' '' + column3 + '' '' + column4 + ''/tdtd'' from [' + @p_userid + 'ophead] order by cast(column1 as int) for xml path(''''))  '
		execute sp_executesql @str1n, N'@strg varchar(max) output', @strg=@str2 output

		if not @str2 is null
		begin
			set @str2=replace(@str2,'/tdtd','</td><td>')
print @str2
			set @str2=rtrim(@str2)
			set @str2= substring(@str2,1,len(@str2)-4) 
			set @str1= 'insert into [' + @fname + '] (report_column,row_seq_id) values (' + Bloyd.pads(@str2) + ',0)'
	print @str1
			execute (@str1)

			set @str2=''
			set @counter2=1
			while @counter2 <= @counter1
			begin
				set @str2 = @str2 + ' + ''<td>'' + isnull(column' + ltrim(@counter2) + ','''') + ''</td>'' '
				set @counter2 = @counter2 + 1
			end
			set @str1 = 'insert into [' + @fname + '] (row_seq_id, report_column) select row_number() over (order by level5_code,level4_code,level3_code,level2_code,level1_code,level0,snumber ),''<td>'' + cast('
			set @str1 = @str1 + 'row_number() over (order by level5_code,level4_code,level3_code,level2_code,level1_code,level0,snumber ) as varchar) + ''</td>'' ' + @str2 + ' from ' + @table_size 
	print @str1		
			execute(@str1)

		end
	end

	if @repview <> '3'
		execute delete_temp_tables @p_userid,@repview

end	
go


if exists (select 1 from sysobjects where xtype = 'P' and name = 'report_rtn1') 
        drop procedure report_rtn1
GO

create procedure report_rtn1
	@p_userid					varchar(10)

--with encryption

as
declare
	@str1						varchar(max),
	@col1						varchar(50),
	@col2						varchar(50),
	@counter1					int,
	@counter2					int,
	@temp_value					varchar(50),
	@str2						varchar(max),
	@str1n						nvarchar(max),
	@upd_string					varchar(max),
	@add_string					varchar(max),
	@orderby					varchar(max),
	@table_rep					varchar(50),
	@colname					varchar(50),
	@pflag						int,
	@pcode						varchar(10),
	@s_report					varchar(10),
	@btotal						varchar(10),
	@bpage						varchar(10),
	@bstaff						varchar(10),
	@bcount						varchar(10)


begin
	
	set @table_rep = @p_userid + 'st01'
	select @pcode=cvalue from pub_table where name1='qcode' and userid=@p_userid
	select @s_report=cvalue from pub_table where name1='qreport' and userid=@p_userid
	
-- get the sorting order
	set @upd_string=''

--create amount field only if amount exist
	set @upd_string=''
	set @add_string=''

-- for reporting, create level1_code(name) -> 5
	set @str1 = ' alter table [' + @table_rep + '] add level1_code varchar(max) not null default '''', level1_name varchar(max) not null default '''', '
	set @str1 = @str1 + ' level2_code varchar(max) not null default '''', level2_name varchar(max) not null default '''', '
	set @str1 = @str1 + ' level3_code varchar(max) not null default '''', level3_name varchar(max) not null default '''', '
	set @str1 = @str1 + ' level4_code varchar(max) not null default '''', level4_name varchar(max) not null default '''', '
	set @str1 = @str1 + ' level5_code varchar(max) not null default '''', level5_name varchar(max) not null default '''' ' 
	set @str1 = @str1 + ', record_counter int, autoid int identity(1,1), level0 varchar(max) not null default '''' '
	--execute(@str1)
print @str1


	set @colname =  @p_userid + 'tns'
	if exists (select 1 from sysobjects where xtype = 'U' and name = @colname) 
	begin
		set @str1 = ' update [' + @table_rep + '] set level1_code=b.level1_code,level1_name=case b.sort1+b.level1_name when '''' then '''' else b.sort1 + '' : '' + b.level1_name end, '
		set @str1 = @str1 + ' level2_code=b.level2_code,level2_name=case b.sort2+b.level2_name when '''' then '''' else b.sort2 + '' : '' + b.level2_name end, '
		set @str1 = @str1 + ' level3_code=b.level3_code,level3_name=case b.sort3+b.level3_name when '''' then '''' else b.sort3 + '' : '' + b.level3_name end, '
		set @str1 = @str1 + ' level4_code=b.level4_code,level4_name=case b.sort4+b.level4_name when '''' then '''' else b.sort4 + '' : '' + b.level4_name end, '
		set @str1 = @str1 + ' level5_code=b.level5_code,level5_name=case b.sort5+b.level5_name when '''' then '''' else b.sort5 + '' : '' + b.level5_name end, '
		set @str1 = @str1 + ' level0=b.level0 '
		set @str1 = @str1 + ' from [' + @table_rep + '] a, [' + @p_userid + 'tns] b where a.key_column=b.key_column '
		execute(@str1)
	end

	set @str1n = 'select @strg='''' + (select '',level'' + cast(count_array as varchar) + ''_name'' from tab_array where array_code=' + Bloyd.pads(@s_report) 
	set @str1n = @str1n + ' and para_code=''' + @pcode + 'S'' and (operand=''Y'' or source1=''Y'' or period=''Y'')  order by count_array desc for xml path('''')) '
	execute sp_executesql @str1n, N'@strg varchar(max) output', @strg=@add_string output
	set @add_string=isnull(@add_string,'')

--if report then sequence by partition else no partition but all levels
	set @str1 = 'update [' + @table_rep + '] set record_counter= a.rw from [' + @table_rep + '] b, '
	if @add_string='' 
		set @str1 = @str1 + ' (select row_number() over(  '
	else
		set @str1 = @str1 + ' (select row_number() over(partition by ' + substring(@add_string,2,1000)

	set @str1 = @str1 + ' order by level5_name+level4_name+level3_name+level2_name+level1_name,level0,snumber) rw, autoid from [' + @table_rep + ']) a where '
	set @str1 = @str1 + ' a.autoid=b.autoid '
	execute(@str1)
print @str1

	set @str1 = 'update [' + @p_userid + 'ophead] set column4=column3, column3='''' where ltrim(column4)='''' '
	execute(@str1)
	set @str1 = 'update [' + @p_userid + 'ophead] set column3=column2, column2='''' where ltrim(column3)='''' '
	execute(@str1)
	set @str1 = 'update [' + @p_userid + 'ophead] set column4=column3, column3='''' where ltrim(column4)='''' '
	execute(@str1)
	set @str1 = 'update [' + @p_userid + 'ophead] set column3=column2, column2='''' where ltrim(column3)='''' '
	execute(@str1)
print ' colname ' +@str1

	set @counter1=1
	while @counter1 < 6
	begin
		set @str1='update [' + @p_userid + 'st01] set level' + cast(@counter1 as varchar) + '_code='''',level' + cast(@counter1 as varchar) + '_name='''' where ' + cast(@counter1 as varchar) + ' in '
		set @str1 = @str1 + '(select count_array from tab_array where para_code='''+@pcode + 'S'' and array_code=' + Bloyd.pads(@s_report) + ' and not (operand=''Y'' or source1=''Y'' or period=''Y''))'
		execute(@str1)
print @str1
		set @counter1 = @counter1+1
	end

end
go


if exists (select 1 from sysobjects where xtype = 'P' and name ='id_table_create')
        drop procedure id_table_create
GO

create procedure id_table_create
	@tablename 	varchar(max)

with encryption

as
declare
	@str2		varchar(max)

begin
	if exists (select 1 from sysobjects where xtype = 'U' and name = @tablename)
		execute( 'drop table [' + @tablename + ']' )

    set @str2 ='CREATE TABLE [' + @tablename + '] (
		id int identity ,
		rep_count int,
		col1 varchar (max) not null default '' '',
		col2 varchar (max) not null default '' '',
		col3 varchar (max) not null default '' '',
		col4 varchar (max) not null default '' '',
		col5 varchar (max) not null default '' '',
		col6 varchar (max) not null default '' '' )'
	
    execute (@str2)
    
End

go


