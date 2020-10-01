using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class cust_table
    {
        [Key, Column(Order = 0)]
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string  customer_surname { get; set;  }
        public string customer_address { get; set; }
        public string cust_city { get; set; }
        public string cust_state { get; set; }
        public string cust_country { get; set; }
        public string cust_phone { get; set; }
        public string cust_email { get; set; }
        public string title { get; set; }
        public string sex { get; set; }
        public string passport_number { get; set; }
        public string nationality { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; } 
    }
    public class room_table
    {
        [Key, Column(Order = 0)]
        public string room_no { get; set; }
        public string room_desc { get; set; }
        public string room_status { get; set; }
        public string room_type { get; set; }
        public decimal room_price { get; set; }
        public int customer_id { get; set; }
        public int max_no_adults { get; set; }
        public int max_no_kids { get; set; }
        public string neat_status { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }
    public class checkin_table
    {
        [Key, Column(Order = 0)]
        public int guest_id { get; set; }
        public string checkin_date { get; set; }
        public string exp_checkout_date { get; set; }
        public string act_checkout_date { get; set; }
        public string titles { get; set; }
        public decimal amount { get; set; }
        public decimal total_amount { get; set; }
        public decimal disc_amount { get; set; }
        public string payment_type { get; set; }
        public string reserved_stat { get; set; }
        public string flag { get; set; }
        public string room_number { get; set; }
        public decimal r_deposit { get; set; }
        public decimal r_balance { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public int customer_id { get; set; }
        public string checkin_time { get; set; }
        public string gender { get; set; }
        public string passport_type { get; set; }
        public string passport_no { get; set; }
        public string first_name { get; set; }
        public string surname { get; set; }
        public int no_of_nights { get; set; }
        public string phone_number { get; set; }
        public string early_cin { get; set; }
        public string address { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }

    public class msg_file
    {
        [Key,Column(Order=0)]
        public string para_code { get; set; }
        [Key, Column(Order = 1)]
        public string id_code { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }

        public string desc3 { get; set; }
        public string desc4 { get; set; }
        public string desc5 { get; set; }
        public string desc6 { get; set; }
        public string desc7 { get; set; }
        public string desc8 { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }
    public class company_settings
    {
        [Key, Column(Order = 0)]
        public string id_code { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }
        public string field9 { get; set; }
        public byte[] com_logo { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }

    public class ordering_table
    {
        [Key, Column(Order = 0)]
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string description { get; set; }
        public string purpose { get; set; }
        public byte[] picture { get; set; }
        public string uom { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal flat_discount { get; set; }
        public decimal per_discount { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

    }



    public class trans_details
    {
        [Key, Column(Order = 0)]
        public int bill_no { get; set; }
        [Key, Column(Order = 1)]
        public int sequence_no { get; set; }
        public int guest_id { get; set; }
        public string item_id { get; set; }
        public int quantity { get; set; }
        public decimal discount_flat { get; set; }
        public decimal discount_per { get; set; }
        public decimal total_amount { get; set; }
        public decimal amount { get; set; }
        public decimal discount_amount { get; set; }
        public string transaction_date { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

    }

    public class staff_table
    {
        [Key, Column(Order = 0)]
        public int staff_id { get; set; }
        public string first_name { get; set; }
        public string surname { get; set; }
        [Column(TypeName = "image")]
        public byte[] staff_photo { get; set; }
        public string job_role { get; set; }
        public string email { get; set; }
        public string phone1 { get; set; }
        public string address{ get; set; }
        public string gender { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public DateTime created_date { get; set; }
        public String created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }

    }
    public class role_table
    {
        [Key, Column(Order = 0)]
        public string role_id { get; set; }
        [Key, Column(Order = 1)]
        public string flag { get; set; }
        [Key, Column(Order = 2)]
        public string menu_option { get; set; }
        public string Role_description { get; set; }
       
        public DateTime created_date { get; set; }
        public String created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }
    public class user_table
    {
        [Key, Column(Order = 0)]
        public string user_id { get; set; }
        public string user_role { get; set; }
        public int staff_id { get; set; }
        public DateTime last_access { get; set; }
        public string user_password { get; set; }
        public DateTime created_date { get; set; }
        public String created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }
    public class item_trans
    {
        [Key, Column(Order =0)]

        public int sequence_no { get; set; }
        [Key, Column(Order = 1)]
        public int bill_no { get; set; }

       
        public int guest_id { get; set; }
        public string transaction_date { get; set; }
        public DateTime created_date { get; set; }
        public String created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }

    public class stock_details
    {
        [Key, Column(Order = 0)]
        public int sequence_no { get; set; }
        public string item_id { get; set; }
        public int quantity { get; set; }
        public string transaction_date { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

    }

    public class vw_user
    {
        [Key, Column(Order = 0)]
        public string type1 { get; set; }
        [Key, Column(Order = 1)]
        public string user_id { get; set; }

        public string user_role { get; set; }
        public int staff_id { get; set; }
        public string user_password { get; set; }
        public DateTime last_access { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public string hvalue { get; set; }
    }

}