
if exists (select * from sys.objects where name = 'checkin_table' and type = 'u')
    drop table checkin_table
CREATE TABLE [dbo].[checkin_table](
	[guest_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](20) NOT NULL,
	[surname] [varchar](20) NOT NULL,
	[customer_id] [int] NOT NULL,
	[checkin_date] [varchar](10) NOT NULL,
	[exp_checkout_date] [varchar](10) NOT NULL,
	[act_checkout_date] [varchar](10) NOT NULL,
	[titles] [varchar](10) NOT NULL,
	[payment_type] [varchar](10) NOT NULL,
	[reserved_stat] [varchar](10) NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[r_balance] [numeric](18, 2) NOT NULL,
	[r_deposit] [numeric](18, 2) NOT NULL,
	[total_amount] [decimal](18, 2) NOT NULL,
	[flag] [varchar](2) NOT NULL,
	[checkin_time] [varchar](10) NOT NULL,
	[early_cin] [varchar](5) NOT NULL,
	[gender] [varchar](10) NOT NULL,
	[address] [varchar](50) NOT NULL,
	[passport_type] [varchar](10) NOT NULL,
	[passport_no] [varchar](20) NOT NULL,
	[room_number] [varchar](50) NOT NULL,
	[adults] [int] NOT NULL,
	[no_of_nights] [int] NOT NULL,
	[phone_number] [varchar](50) NOT NULL,
	[children] [int] NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[disc_amount] [numeric](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[company_settings]    Script Date: 7/14/2018 12:43:36 PM ******/

if exists (select * from sys.objects where name = 'company_settings' and type = 'u')
    drop table company_settings
CREATE TABLE [dbo].[company_settings](
	[id_code] [varchar](10) NOT NULL,
	[field1] [varchar](200) NOT NULL,
	[field2] [varchar](200) NOT NULL,
	[field3] [varchar](200) NOT NULL,
	[field4] [varchar](200) NOT NULL,
	[field5] [varchar](200) NOT NULL,
	[field6] [varchar](200) NOT NULL,
	[field7] [varchar](200) NOT NULL,
	[field8] [varchar](200) NOT NULL,
	[field9] [varchar](200) NOT NULL,
	[com_logo] [image] NULL,
	[created_date] [smalldatetime] NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cust_table]    Script Date: 7/14/2018 12:43:37 PM ******/
if exists (select * from sys.objects where name = 'cust_table' and type = 'u')
    drop table cust_table
CREATE TABLE [dbo].[cust_table](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_name] [varchar](20) NOT NULL,
	[customer_surname] [varchar](20) NOT NULL,
	[customer_address] [varchar](50) NOT NULL,
	[cust_city] [varchar](20) NOT NULL,
	[cust_state] [varchar](20) NOT NULL,
	[cust_country] [varchar](20) NOT NULL,
	[cust_phone] [varchar](15) NOT NULL,
	[cust_email] [varchar](50) NOT NULL,
	[title] [varchar](10) NOT NULL,
	[sex] [varchar](10) NOT NULL,
	[passport_number] [varchar](15) NOT NULL,
	[nationality] [varchar](20) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[item_trans]    Script Date: 7/14/2018 12:43:38 PM ******/
if exists (select * from sys.objects where name = 'item_trans' and type = 'u')
    drop table item_trans
CREATE TABLE [dbo].[item_trans](
	[sequence_no] [int] NOT NULL,
	[bill_no] [int] NOT NULL,
	[guest_id] [int] NOT NULL,
	[transaction_date] [varchar](10) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[msg_file]    Script Date: 7/14/2018 12:43:38 PM ******/
if exists (select * from sys.objects where name = 'msg_file' and type = 'u')
    drop table msg_file
CREATE TABLE [dbo].[msg_file](
	[para_code] [varchar](20) NOT NULL,
	[id_code] [varchar](20) NOT NULL,
	[desc1] [varchar](500) NOT NULL,
	[desc2] [varchar](500) NOT NULL,
	[desc3] [varchar](500) NOT NULL,
	[desc4] [varchar](500) NOT NULL,
	[desc5] [varchar](500) NOT NULL,
	[desc6] [varchar](500) NOT NULL,
	[desc7] [varchar](500) NOT NULL,
	[desc8] [varchar](500) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ordering_table]    Script Date: 7/14/2018 12:43:40 PM ******/
if exists (select * from sys.objects where name = 'ordering_table' and type = 'u')
    drop table ordering_table
CREATE TABLE [dbo].[ordering_table](
	[item_id] [int] IDENTITY(1,1) NOT NULL,
	[item_name] [varchar](30) NOT NULL,
	[description] [varchar](200) NOT NULL,
	[purpose] [varchar](5) NOT NULL,
	[picture] [varbinary](max) NULL,
	[uom] [varchar](20) NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[quantity] [int] NOT NULL,
	[flat_discount] [decimal](18, 2) NOT NULL,
	[per_discount] [decimal](18, 2) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pub_table]    Script Date: 7/14/2018 12:43:40 PM ******/
if exists (select * from sys.objects where name = 'pub_table' and type = 'u')
    drop table pub_table
CREATE TABLE [dbo].[pub_table](
	[userid] [varchar](10) NOT NULL,
	[name1] [varchar](50) NOT NULL,
	[cvalue] [varchar](2000) NOT NULL,
	[date1] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_table]    Script Date: 7/14/2018 12:43:41 PM ******/
if exists (select * from sys.objects where name = 'role_table' and type = 'u')
    drop table role_table
CREATE TABLE [dbo].[role_table](
	[role_id] [varchar](10) NOT NULL,
	[flag] [varchar](50) NOT NULL,
	[menu_option] [varchar](10) NOT NULL,
	[Role_description] [varchar](50) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[room_table]    Script Date: 7/14/2018 12:43:41 PM ******/
if exists (select * from sys.objects where name = 'room_table' and type = 'u')
    drop table room_table
CREATE TABLE [dbo].[room_table](
	[room_no] [varchar](10) NOT NULL,
	[room_desc] [varchar](50) NOT NULL,
	[room_status] [varchar](10) NOT NULL,
	[room_type] [varchar](20) NOT NULL,
	[room_price] [decimal](18, 2) NOT NULL,
	[customer_id] [int] NOT NULL,
	[max_no_adults] [int] NOT NULL,
	[max_no_kids] [int] NOT NULL,
	[neat_status] [varchar](10) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[staff_table]    Script Date: 7/14/2018 12:43:42 PM ******/
if exists (select * from sys.objects where name = 'staff_table' and type = 'u')
    drop table staff_table
CREATE TABLE [dbo].[staff_table](
	[staff_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](30) NOT NULL,
	[surname] [varchar](20) NOT NULL,
	[staff_photo] [image] NULL,
	[job_role] [varchar](10) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[phone1] [varchar](20) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[gender] [varchar](10) NOT NULL,
	[city] [varchar](20) NOT NULL,
	[state] [varchar](50) NOT NULL,
	[country] [varchar](10) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stock_details]    Script Date: 7/14/2018 12:43:42 PM ******/
if exists (select * from sys.objects where name = 'stock_details' and type = 'u')
    drop table stock_details
CREATE TABLE [dbo].[stock_details](
	[sequence_no] [int] IDENTITY(1,1) NOT NULL,
	[item_id] [varchar](10) NOT NULL,
	[quantity] [int] NOT NULL,
	[transaction_date] [varchar](10) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_stock_details] PRIMARY KEY CLUSTERED 
(
	[sequence_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_array]    Script Date: 7/14/2018 12:43:45 PM ******/
if exists (select * from sys.objects where name = 'tab_array' and type = 'u')
    drop table tab_array
CREATE TABLE [dbo].[tab_array](
	[para_code] [varchar](10) NOT NULL,
	[array_code] [varchar](10) NOT NULL,
	[count_array] [decimal](4, 0) NOT NULL,
	[operand] [varchar](500) NOT NULL,
	[source1] [varchar](500) NOT NULL,
	[period] [varchar](500) NOT NULL,
	[operator1] [varchar](10) NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[percent1] [decimal](18, 2) NOT NULL,
	[select1] [varchar](500) NOT NULL,
	[sort1] [varchar](500) NOT NULL,
	[true_desc] [varchar](500) NOT NULL,
	[false_desc] [varchar](500) NOT NULL,
	[internal_use] [varchar](1) NOT NULL,
	[amended_by] [varchar](50) NOT NULL,
	[date_amended] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_calc]    Script Date: 7/14/2018 12:43:45 PM ******/
if exists (select * from sys.objects where name = 'tab_calc' and type = 'u')
    drop table tab_calc
CREATE TABLE [dbo].[tab_calc](
	[para_code] [varchar](10) NOT NULL,
	[calc_code] [varchar](11) NOT NULL,
	[name1] [varchar](50) NOT NULL,
	[report_name] [varchar](50) NOT NULL,
	[transfer_code] [varchar](500) NOT NULL,
	[suppress_zero] [varchar](500) NOT NULL,
	[line_spacing] [decimal](4, 0) NOT NULL,
	[wide_column] [varchar](500) NOT NULL,
	[column_no] [int] NOT NULL,
	[report_type] [varchar](500) NOT NULL,
	[menu_option] [varchar](500) NOT NULL,
	[internal_use] [varchar](1) NOT NULL,
	[comment_code] [varchar](50) NOT NULL,
	[addnotes] [text] NOT NULL,
	[created_by] [varchar](50) NOT NULL,
	[date_created] [smalldatetime] NOT NULL,
	[amended_by] [varchar](50) NOT NULL,
	[date_amended] [smalldatetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[trans_details]    Script Date: 7/14/2018 12:43:46 PM ******/
if exists (select * from sys.objects where name = 'trans_details' and type = 'u')
    drop table trans_details
CREATE TABLE [dbo].[trans_details](
	[bill_no] [int] NOT NULL,
	[sequence_no] [int] NOT NULL,
	[guest_id] [int] NOT NULL,
	[item_id] [varchar](10) NOT NULL,
	[quantity] [int] NOT NULL,
	[transaction_date] [varchar](10) NOT NULL,
	[discount_flat] [decimal](18, 2) NOT NULL,
	[discount_per] [decimal](18, 2) NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[discount_amount] [decimal](18, 2) NOT NULL,
	[total_amount] [decimal](18, 2) NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_table]    Script Date: 7/14/2018 12:43:46 PM ******/
if exists (select * from sys.objects where name = 'user_table' and type = 'u')
    drop table user_table
CREATE TABLE [dbo].[user_table](
	[user_id] [varchar](10) NOT NULL,
	[user_role] [varchar](20) NOT NULL,
	[staff_id] [int] NOT NULL,
	[user_password] [varchar](500) NOT NULL,
	[last_access] [smalldatetime] NOT NULL,
	[created_by] [varchar](30) NOT NULL,
	[created_date] [smalldatetime] NOT NULL,
	[modified_by] [varchar](30) NOT NULL,
	[modified_date] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_first_name]  DEFAULT ('') FOR [first_name]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_surname]  DEFAULT ('') FOR [surname]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_customer_id]  DEFAULT ((0)) FOR [customer_id]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_checkin_date]  DEFAULT ('') FOR [checkin_date]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_exp_checkout_date]  DEFAULT ('') FOR [exp_checkout_date]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_act_checkout_date]  DEFAULT ('') FOR [act_checkout_date]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_titles]  DEFAULT ('') FOR [titles]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_payment_type]  DEFAULT ('') FOR [payment_type]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_reserved_stat]  DEFAULT ('') FOR [reserved_stat]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_r_balance]  DEFAULT ((0)) FOR [r_balance]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_r_deposit]  DEFAULT ((0)) FOR [r_deposit]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_total_amount]  DEFAULT ((0)) FOR [total_amount]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_flag]  DEFAULT ('') FOR [flag]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_checkin_time]  DEFAULT ('') FOR [checkin_time]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_early_cin]  DEFAULT ('') FOR [early_cin]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_gender]  DEFAULT ('') FOR [gender]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_address]  DEFAULT ('') FOR [address]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_passport_type]  DEFAULT ('') FOR [passport_type]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_passport_no]  DEFAULT ('') FOR [passport_no]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_room_number]  DEFAULT ('') FOR [room_number]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_adults]  DEFAULT ((0)) FOR [adults]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_no_of_nights]  DEFAULT ((0)) FOR [no_of_nights]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_phone_number]  DEFAULT ('') FOR [phone_number]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_children]  DEFAULT ((0)) FOR [children]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [DF_checkin_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[checkin_table] ADD  CONSTRAINT [checkin_table_disc_amount]  DEFAULT ((0)) FOR [disc_amount]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_id_code]  DEFAULT ('') FOR [id_code]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field1]  DEFAULT ('') FOR [field1]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field2]  DEFAULT ('') FOR [field2]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field3]  DEFAULT ('') FOR [field3]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field4]  DEFAULT ('') FOR [field4]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field5]  DEFAULT ('') FOR [field5]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field6]  DEFAULT ('') FOR [field6]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field7]  DEFAULT ('') FOR [field7]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field8]  DEFAULT ('') FOR [field8]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_field9]  DEFAULT ('') FOR [field9]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[company_settings] ADD  CONSTRAINT [DF_company_settings_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_customer_name]  DEFAULT ('') FOR [customer_name]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_customer_surname]  DEFAULT ('') FOR [customer_surname]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_customer_address]  DEFAULT ('') FOR [customer_address]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_cust_city]  DEFAULT ('') FOR [cust_city]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_cust_state]  DEFAULT ('') FOR [cust_state]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_cust_country]  DEFAULT ('') FOR [cust_country]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_cust_phone]  DEFAULT ('') FOR [cust_phone]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_cust_email]  DEFAULT ('') FOR [cust_email]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_title]  DEFAULT ('') FOR [title]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_sex]  DEFAULT ('') FOR [sex]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_passport_number]  DEFAULT ('') FOR [passport_number]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_nationality]  DEFAULT ('') FOR [nationality]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[cust_table] ADD  CONSTRAINT [DF_cust_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_bill_no]  DEFAULT ((0)) FOR [bill_no]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_guest_id]  DEFAULT ((0)) FOR [guest_id]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_transaction_date]  DEFAULT ('') FOR [transaction_date]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[item_trans] ADD  CONSTRAINT [DF_item_trans_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_para_code]  DEFAULT ('') FOR [para_code]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_id_code]  DEFAULT ('') FOR [id_code]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc1]  DEFAULT ('') FOR [desc1]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc2]  DEFAULT ('') FOR [desc2]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc3]  DEFAULT ('') FOR [desc3]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc4]  DEFAULT ('') FOR [desc4]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc5]  DEFAULT ('') FOR [desc5]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc6]  DEFAULT ('') FOR [desc6]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc7]  DEFAULT ('') FOR [desc7]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_desc8]  DEFAULT ('') FOR [desc8]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[msg_file] ADD  CONSTRAINT [DF_msg_file_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_item_name]  DEFAULT ('') FOR [item_name]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_description]  DEFAULT ('') FOR [description]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_purpose]  DEFAULT ('') FOR [purpose]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_uom]  DEFAULT ('') FOR [uom]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_flat_discount]  DEFAULT ((0)) FOR [flat_discount]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_per_discount]  DEFAULT ((0)) FOR [per_discount]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[ordering_table] ADD  CONSTRAINT [DF_ordering_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[pub_table] ADD  CONSTRAINT [DF_pub_table_userid]  DEFAULT ('') FOR [userid]
GO
ALTER TABLE [dbo].[pub_table] ADD  CONSTRAINT [DF_pub_table_name1]  DEFAULT ('') FOR [name1]
GO
ALTER TABLE [dbo].[pub_table] ADD  CONSTRAINT [DF_pub_table_cvalue]  DEFAULT ('') FOR [cvalue]
GO
ALTER TABLE [dbo].[pub_table] ADD  CONSTRAINT [DF_pub_table_date1]  DEFAULT (getutcdate()) FOR [date1]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_role_id]  DEFAULT ('') FOR [role_id]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_flag]  DEFAULT ('') FOR [flag]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_menu_option]  DEFAULT ('') FOR [menu_option]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_Role_description]  DEFAULT ('') FOR [Role_description]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[role_table] ADD  CONSTRAINT [DF_role_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_room_no]  DEFAULT ('') FOR [room_no]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_room_desc]  DEFAULT ('') FOR [room_desc]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_room_status]  DEFAULT ('') FOR [room_status]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_room_type]  DEFAULT ('') FOR [room_type]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_room_price]  DEFAULT ((0)) FOR [room_price]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_customer_id]  DEFAULT ((0)) FOR [customer_id]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_max_no_adults]  DEFAULT ((0)) FOR [max_no_adults]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_max_no_kids]  DEFAULT ((0)) FOR [max_no_kids]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_neat_status]  DEFAULT ('') FOR [neat_status]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[room_table] ADD  CONSTRAINT [DF_room_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_first_name]  DEFAULT ('') FOR [first_name]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_surname]  DEFAULT ('') FOR [surname]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_job_role]  DEFAULT ('') FOR [job_role]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_email]  DEFAULT ('') FOR [email]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_phone1]  DEFAULT ('') FOR [phone1]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_address]  DEFAULT ('') FOR [address]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_gender]  DEFAULT ('') FOR [gender]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_city]  DEFAULT ('') FOR [city]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_state]  DEFAULT ('') FOR [state]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_country]  DEFAULT ('') FOR [country]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[staff_table] ADD  CONSTRAINT [DF_staff_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_item_id]  DEFAULT ('') FOR [item_id]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_transaction_date]  DEFAULT ('') FOR [transaction_date]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[stock_details] ADD  CONSTRAINT [DF_stock_details_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_para_code]  DEFAULT ('') FOR [para_code]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_array_code]  DEFAULT ('') FOR [array_code]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_count_array]  DEFAULT ((0)) FOR [count_array]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_operand]  DEFAULT ('') FOR [operand]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_source1]  DEFAULT ('') FOR [source1]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_period]  DEFAULT ('') FOR [period]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_operator1]  DEFAULT ('') FOR [operator1]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_percent1]  DEFAULT ((0)) FOR [percent1]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_select1]  DEFAULT ('') FOR [select1]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_sort1]  DEFAULT ('') FOR [sort1]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_true_desc]  DEFAULT ('') FOR [true_desc]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_false_desc]  DEFAULT ('') FOR [false_desc]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_internal_use]  DEFAULT ('') FOR [internal_use]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_amended_by]  DEFAULT ('') FOR [amended_by]
GO
ALTER TABLE [dbo].[tab_array] ADD  CONSTRAINT [DF_tab_array_date_amended]  DEFAULT (getutcdate()) FOR [date_amended]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_para_code]  DEFAULT ('') FOR [para_code]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_calc_code]  DEFAULT ('') FOR [calc_code]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_name1]  DEFAULT ('') FOR [name1]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_report_name]  DEFAULT ('') FOR [report_name]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_transfer_code]  DEFAULT ('') FOR [transfer_code]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_suppress_zero]  DEFAULT ('') FOR [suppress_zero]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_line_spacing]  DEFAULT ((0)) FOR [line_spacing]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_wide_column]  DEFAULT ('') FOR [wide_column]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_column_no]  DEFAULT ((0)) FOR [column_no]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_report_type]  DEFAULT ('') FOR [report_type]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_menu_option]  DEFAULT ('') FOR [menu_option]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_internal_use]  DEFAULT ('') FOR [internal_use]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_comment_code]  DEFAULT ('') FOR [comment_code]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_addnotes]  DEFAULT ('') FOR [addnotes]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_date_created]  DEFAULT (getutcdate()) FOR [date_created]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_amended_by]  DEFAULT ('') FOR [amended_by]
GO
ALTER TABLE [dbo].[tab_calc] ADD  CONSTRAINT [DF_tab_calc_date_amended]  DEFAULT (getutcdate()) FOR [date_amended]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_bill_no]  DEFAULT ((0)) FOR [bill_no]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_sequence_no]  DEFAULT ((0)) FOR [sequence_no]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_guest_id]  DEFAULT ((0)) FOR [guest_id]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_item_id]  DEFAULT ('') FOR [item_id]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_transaction_date]  DEFAULT ('') FOR [transaction_date]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_discount_flat]  DEFAULT ((0)) FOR [discount_flat]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_discount_per]  DEFAULT ((0)) FOR [discount_per]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_discount_amount]  DEFAULT ((0)) FOR [discount_amount]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_total_amount]  DEFAULT ((0)) FOR [total_amount]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_created_by]  DEFAULT (getutcdate()) FOR [created_by]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[trans_details] ADD  CONSTRAINT [DF_trans_details_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_user_id]  DEFAULT ('') FOR [user_id]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_user_role]  DEFAULT ('') FOR [user_role]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_staff_id]  DEFAULT ((0)) FOR [staff_id]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_user_password]  DEFAULT ('') FOR [user_password]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_last_access]  DEFAULT (getutcdate()) FOR [last_access]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_created_by]  DEFAULT ('') FOR [created_by]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_created_date]  DEFAULT (getutcdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_modified_by]  DEFAULT ('') FOR [modified_by]
GO
ALTER TABLE [dbo].[user_table] ADD  CONSTRAINT [DF_user_table_modified_date]  DEFAULT (getutcdate()) FOR [modified_date]
GO
