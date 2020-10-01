using HMS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HMS.utilities
{
    public class hutility
    {
        MainContext db = new MainContext();
        public string sqlquote(string sqlstr1)
        {
            if (sqlstr1 == null)
                sqlstr1 = "";

            string sqlstr2 = sqlstr1.Replace("'", "''");
            sqlstr2 = "'" + sqlstr2 + "'";
            return sqlstr2;
        }

        public SelectList all_select_query(string type1, string pcode="",string cond="")
        {
            string str1 = "";
            if (string.IsNullOrWhiteSpace(cond)) {
                str1 = "select c2 ,  c4 from all_select_query  where c1=" + sqlquote(type1) + " order by 2 ";
            }
            else
                str1 = "select c2 ,  c4 from all_select_query  where c1=" + sqlquote(type1) + "and c5="+sqlquote(cond)+" order by 2 ";
            var bg2 = db.Database.SqlQuery<tempquery>(str1).ToList();
                return new SelectList(bg2.ToList(), "c2", "c4", pcode);
        }
        public string date_yyyymmdd(string date1)
        {
            if (string.IsNullOrWhiteSpace(date1))
                return "";

            string date2 = date1.Replace("/", "");
            date2 = date2.Replace("-", "");
            string date3 = date2.Substring(4, 4) + date2.Substring(2, 2) + date2.Substring(0, 2);

            return date3;

        }

        public string find_name(string type1, string pcode, string rcode)
        {
            string str1 = "";
            if (type1 == "1")
                str1 = "select c0 = c4 from all_select_query where c1 = " + sqlquote(pcode) + " and   c2 = " + sqlquote(rcode);

            else if (type1 == "2")
                str1 = " select c0 = first_name+' '+surname from staff_table where staff_id = " + sqlquote(rcode);
            else if (type1 == "3")
                str1 = " select c0 = hvalue from vw_user where user_id = " + sqlquote(rcode);

            var exc = db.Database.SqlQuery<tempquery>(str1).FirstOrDefault();
            string name2 = exc == null ? "" : exc.c0;
           
            return name2;

        }
        public void htitles(string ptitle)
        {
            worksess worksess = (worksess)HttpContext.Current.Session["worksess"];
            worksess.ptitle = ptitle;

            HttpContext.Current.Session["worksess"] = worksess;

        }
        public Boolean date_validate(string date1)
        {
            if (string.IsNullOrWhiteSpace(date1))
                return false;

            DateTime invaliddate = new DateTime(1000, 01, 01);
            if (date1.Length == 10)
            {
                //int dd = Convert.ToInt16(date1.Substring(0, 2));
                //int mm = Convert.ToInt16(date1.Substring(3, 2));
                //int yy = Convert.ToInt16(date1.Substring(6, 4));

                DateTime d1;
                string[] format = { "dd/MM/yyyy", "dd-MM-yyyy" };
                if (DateTime.TryParseExact(date1, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out d1))
                    return true;
                else
                    return false;
            }
            else return false;
        }

        public string date_out(string date1)
        {
            if (string.IsNullOrWhiteSpace(date1))
                return "";
            string date2 = date1.Replace("/", "");
            if (date2.Length != 8)
                return "";
            else
                return date2.Substring(6, 2) + "/" + date2.Substring(4, 2) + "/" + date2.Substring(0, 4);
        }
        public string date_slash(string date1)
        {
            if (date1 != "")
            {
                string date2 = date_ddmmyyyyy(date1);
                date2 = date2.Substring(0, 2) + "/" + date2.Substring(2, 2) + "/" + date2.Substring(4);
                return date2;
            }
            else return "";
        }

        public string date_ddmmyyyyy(string date1)
        {
            if (string.IsNullOrWhiteSpace(date1))
                return "";

            string date2 = date1.Replace("/", "");
            string date3 = date2.Substring(6, 2) + date2.Substring(4, 2) + date2.Substring(0, 4);

            return date3;

        }

        /// <summary>
        /// This method hashes the given text with 
        /// the SHA1CryptoServiceProvider.
        /// </summary>
        /// <param name="text">Text to hash</param>
        /// <returns>Hashed Value</returns>
        public string HashString(string text)
        {
            // Create an instance of the SHA1 provider
            SHA1 sha = new SHA1CryptoServiceProvider();

            // Compute the hash 
            byte[] hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(text));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hashedData)
            {
                // Convert each byte to Hex
                stringBuilder.Append(String.Format("{0,2:X2}", b));
            }

            // Return the hashed value
            return stringBuilder.ToString();
        }
        
    }
}