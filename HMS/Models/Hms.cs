using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class vw_genlay
    {
        public string vwstring0 { get; set; }
        public string vwstring1 { get; set; }
        public string vwstring2 { get; set; }
        public string vwstring3 { get; set; }
        public string vwstring4 { get; set; }
        public string vwstring5 { get; set; }
        public string vwstring6 { get; set; }
        public string vwstring7 { get; set; }
        public string vwstring8 { get; set; }
        public string vwstring9 { get; set; }
        public string vwstring10 { get; set; }

        public string vwstring11 { get; set; }
        public string vwstring12 { get; set; }
        public string vwstring13 { get; set; }
        public string vwstring14 { get; set; }
        public string vwstring15 { get; set; }
        public string vwstring16 { get; set; }
        public string vwstring17 { get; set; }
        public string vwstring18 { get; set; }
        public string vwstring19 { get; set; }
        public string vwstring20 { get; set; }
        public decimal vwdecimal0 { get; set; }
        public decimal vwdecimal1 { get; set; }
        public decimal vwdecimal2 { get; set; }
        public decimal vwdecimal3 { get; set; }
        public decimal vwdecimal4 { get; set; }
        public decimal vwdecimal5 { get; set; }
        public decimal vwdecimal6 { get; set; }
        public decimal vwdecimal7 { get; set; }
        public decimal vwdecimal8 { get; set; }
        public decimal vwdecimal9 { get; set; }
        public decimal vwdecimal10 { get; set; }
        public decimal vwdecimal11 { get; set; }
        public decimal vwdecimal12 { get; set; }
        public decimal vwdecimal13 { get; set; }
        public decimal vwdecimal14 { get; set; }
        public decimal vwdecimal15 { get; set; }
        public decimal vwfloat1 { get; set; }
        public int vwint0 { get; set; }
        public int vwint1 { get; set; }
        public int vwint2 { get; set; }
        public int vwint3 { get; set; }
        public int vwint4 { get; set; }
        public int vwint5 { get; set; }
        public int vwint6 { get; set; }
        public int vwint7 { get; set; }
        public int vwint8 { get; set; }
        public int vwint9 { get; set; }
        public int vwint10 { get; set; }
        public int[] vwitarray0 { get; set; }
        public int[] vwitarray1 { get; set; }
        public int[] vwitarray2 { get; set; }

        public int[] vwitarray3 { get; set; }
        public int[] vwitarray4 { get; set; }
        public bool vwbool0 { get; set; }
        public bool vwbool1 { get; set; }
        public bool vwbool2 { get; set; }
        public bool[] vwblarray0 { get; set; }
        public bool[] vwblarray1 { get; set; }
        public bool[] vwblarray2 { get; set; }
        public bool[] vwblarray3 { get; set; }
        public bool[] vwblarray4 { get; set; }
        public bool[] vwblarray5 { get; set; }

        public DateTime vwdate0 { get; set; }
        public DateTime vwdate1 { get; set; }
        public DateTime vwdate2 { get; set; }
        public DateTime[] vwdtarray0 { get; set; }
        public DateTime[] vwdtarray1 { get; set; }

        public string[] vwstrarray0 { get; set; }
        public string[] vwstrarray1 { get; set; }
        public string[] vwstrarray2 { get; set; }
        public string[] vwstrarray3 { get; set; }
        public string[] vwstrarray4 { get; set; }
        public string[] vwstrarray5 { get; set; }
        public string[] vwstrarray6 { get; set; }
        public string[] vwstrarray7 { get; set; }
        public string[] vwstrarray8 { get; set; }
        public string[] vwstrarray9 { get; set; }
        public string[] vwstrarray10 { get; set; }
        public string[] vwstrarray11 { get; set; }
        public string[] vwstrarray12 { get; set; }
        public string[] vwstrarray13 { get; set; }
        public string[] vwstrarray14 { get; set; }
        public string[] vwstrarray15 { get; set; }
        public string[] vwstrarray16 { get; set; }
        public string[] vwstrarray17 { get; set; }
        public string[] vwstrarray18 { get; set; }
        public decimal[] vwdclarray0 { get; set; }
        public decimal[] vwdclarray1 { get; set; }
        public decimal[] vwdclarray2 { get; set; }
        public decimal[] vwdclarray3 { get; set; }
        public decimal[] vwdclarray4 { get; set; }
        public decimal[] vwdclarray5 { get; set; }
        public decimal[] vwdclarray6 { get; set; }
        public byte[] picture12 { get; set; }
        public byte vwbyte1 { get; set; }
        public string imagecat { get; set; }
        public string imagedesc { get; set; }
        public string datmode { get; set; }
        public string vwcode { get; set; }
        public string[] dsp_string { get; set; }
        public List<tempquery>[] vwlist0 { get; set; }


    }
    public class queryhead
    {
        public string query0 { get; set; }
        public string query1 { get; set; }
        public string query2 { get; set; }
        public string query3 { get; set; }
        public string query4 { get; set; }
        public string query5 { get; set; }
        public string query6 { get; set; }
        public string query7 { get; set; }
        public string query8 { get; set; }
        public string query9 { get; set; }
        public string query10 { get; set; }
        public int intquery0 { get; set; }
        public int intquery1 { get; set; }
        public int intquery2 { get; set; }
        public decimal dquery0 { get; set; }
        public decimal dquery1 { get; set; }
        public decimal dquery2 { get; set; }
        public bool bquery0 { get; set; }
        public string[] sarray4 { get; set; }
    }

    [Serializable]
    public class worksess
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string usergroup { get; set; }
        public DateTime datein { get; set; }
        public string idrep { get; set; }


        public string period_closing { get; set; }
        
        public string err_message { get; set; }
     
      
        public string processing_period { get; set; }
        public string viewflag { get; set; }
        public string filep { get; set; }
        public string pname { get; set; }       // staff name
        public string printer_code { get; set; }    // default printer
        public string temp0 { get; set; }
        public string temp1 { get; set; }
        public string temp2 { get; set; }
        public string temp3 { get; set; }
        public string temp4 { get; set; }
        public string temp5 { get; set; }
        public int xyr { get; set; }
        public string temp6 { get; set; }
        public string temp7 { get; set; }
        public string ptitle { get; set; }
        public string menu_option { get; set; }
        public string[] sarrayt1 { get; set; }

    }
    public class tempquery
    {
        public string c0 { get; set; }
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string c6 { get; set; }
        public string c7 { get; set; }
        public string c8 { get; set; }
        public string c9 { get; set; }
        public int intc1 { get; set; }
        public int intc2 { get; set; }
        public int intc3 { get; set; }
        public int intc4 { get; set; }
        public int intc5 { get; set; }
        public int intc6 { get; set; }
        public decimal decc1 { get; set; }
        public string hmenu { get; set; }
    }

}