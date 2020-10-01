using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class MainContext : DbContext
    {

        public MainContext()
       : base("name = MainContext")
        {

            // Get the ObjectContext related to this DbContext
            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            string timeouts = ConfigurationManager.AppSettings["ctime"];

            // Sets the command timeout for all the commands
            this.Database.CommandTimeout = Convert.ToInt16(timeouts);
        }

        public DbSet<cust_table> cust_table { get; set; }
        public DbSet<room_table> room_table { get; set; }
        public DbSet<checkin_table> checkin_table { get; set; }
        public DbSet<msg_file> msg_file { get; set; }
        public DbSet<company_settings>company_settings { get; set; }
        public DbSet<ordering_table> ordering_table { get; set; }
        public DbSet<trans_details> trans_details { get; set; }
        public DbSet<staff_table> staff_table { get; set; }
        public DbSet<role_table> role_table { get; set; }
        public DbSet<user_table> user_table { get; set; }
        public DbSet<item_trans> item_trans { get; set; }
        public DbSet<stock_details> stock_details { get; set; }
        public DbSet<vw_user> vw_user { get; set; }
    }


}