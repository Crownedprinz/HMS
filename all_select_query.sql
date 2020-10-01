

alter VIEW [dbo].[all_select_query]
AS
SELECT        '001' c1,room_no c2, room_desc c3, room_no+'-'+room_desc+'-'+(select desc1 from msg_file where para_code = 'RMT' and id_code = room_type) c4,room_status c5 FROM dbo.room_table union all
SELECT        'GEN' c1,id_code c2, desc1 c3,desc1 c4,para_code c5 FROM dbo.msg_file union all
SELECT        '002' c1,cast(customer_id as varchar) c2, customer_name+' '+customer_surname c3, customer_name+' '+customer_surname c4,'' c5 FROM dbo.cust_table union all
SELECT        '003' c1,cast(item_id as varchar) c2, item_name c3, item_name +' '+description+' '+cast(quantity as varchar)+' left' c4,purpose c5 FROM dbo.ordering_table union all
SELECT		  '004' c1, cast(guest_id as varchar) c2 ,first_name+' '+surname c3, first_name+' '+surname c4,flag c5  from checkin_table union all
SELECT		  '006' c1, cast(customer_id as varchar) c2 ,first_name+' '+surname c3, first_name+' '+surname c4,'' c5  from checkin_table union all
SELECT		  '007' c1, role_id c2 ,Role_description c3, Role_description c4,'' c5  from role_table where flag = 'H' union all
SELECT		  '008' c1, cast(bill_no as varchar) c2 ,cast(bill_no as varchar) c3, cast(bill_no as varchar) c4,'' c5  from item_trans union all
SELECT		  '005' c1, cast(staff_id as varchar) c2 ,first_name+' '+surname c3, first_name+' '+surname c4,'' c5  from staff_table union all
SELECT        '009' c1,cast(item_id as varchar) c2, item_name c3, item_name+' '+description c4,purpose c5 FROM dbo.ordering_table union all
SELECT		  '010' c1, cast(guest_id as varchar) c2 ,first_name+' '+surname c3, first_name+' '+surname c4,flag c5  from checkin_table where reserved_stat='Y' 

GO


