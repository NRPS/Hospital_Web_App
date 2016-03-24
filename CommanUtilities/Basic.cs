using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanUtilities
{
    public class Basic
    {
        MSAccessDataUtility objDataUtility = new MSAccessDataUtility();

        public Basic()
        {
        }

        private DateTime _FromDate;
        private DateTime _ToDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public String GetReportDateRange()
        {
            return "From " + _FromDate.ToShortDateString() + " To " + _ToDate.ToShortDateString();
        }

        #region Key Coding
        public int GetMax(String TableName, String FieldName)
        {
            return GetMax(TableName, FieldName, "", false);
        }

        public int GetMax(String TableName, String FieldName, String WhereCondition)
        {
            return GetMax(TableName, FieldName, WhereCondition, false);
        }
        public int GetMax(String TableName, String FieldName, bool isCompanyCondition)
        {
            return GetMax(TableName, FieldName, "", isCompanyCondition);
        }
        public int GetMax(String TableName, String FieldName, String WhereCondition, bool isCompanyCondition)
        {
            String Condition = "";
            int id = 0;
            try
            {

                if (isCompanyCondition || WhereCondition.Length > 0)
                {
                    Condition = " where ";
                    string andCondition = "";
                    if (isCompanyCondition)
                    {
                        Condition = Condition + " CompanyCode = '" + LogDetails.CurrentCompanyCode + "'  and fyear = " + LogDetails.CurrentFinancialYear;
                        andCondition = " and ";
                    }
                    if (WhereCondition.Length > 0)
                        Condition = Condition + andCondition + WhereCondition;

                }

                String commandString = "select max(" + FieldName + ") as maxid from " + TableName + Condition;
                id = objDataUtility.GetScalarValue(commandString);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);

            }
            return id;
        }

        public String GetKey(int Srno, Char Prefix = '~', int Length = 6)
        {
            return GetKey(Srno, Prefix, false, false, false,Length);
        }
        public String GetKey(int Srno, Char Prefix = '~', bool isYearPrefix = false, bool isMonthPrefix = false)
        {
            return GetKey(Srno, Prefix, false, isYearPrefix, isMonthPrefix);
        }
        public String GetKey(int Srno, Char Prefix = '~', bool isCompanyCodePrefix = false, bool isYearPrefix = false, bool isMonthPrefix = false, int Length = 6)
        {
            /*
                * P-Patient  1603P00001
                * B - Bill 1603B00001
                * C - PAYMENT / RECEIPT 1603C000001
           */

            String s = "";
            if (isCompanyCodePrefix)
                s = LogDetails.CurrentCompanyCode;
            if (isYearPrefix)
                s = s + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            if (isMonthPrefix)
            {
                string s2 = ("00" + DateTime.Now.Date.Month.ToString());
                s = s + s2.Substring(s2.Length-2);
            }
            if (Prefix != '~' && Prefix != ' ')
                s = s + Prefix;
            if (Length > 0)
            {
                string s1 = "0000000000".Substring(0, (Length - Srno.ToString().Length));
                s = s + s1 + Srno.ToString();
            }
            else
            {
                s = s + Srno.ToString();
            }
            return s;
        }
        #endregion
    }

    public static class LogDetails
    {
        public static String CurrentCompanyCode = "11"; //Program.CurrentCompanyCode;
        public static int CurrentFinancialYear = 2016; //Program.CurrentFinancialYear;
        public static String CurrentDivID = "11";     // Program.CurrentDivID;
        public static int UserId = 1; // Program.UserId;
        public static int DeletedTrue = 1;
        public static int DeletedFalse = 0;
        public static DateTime FromDate = DateTime.Now.Date;// Program.FromDate;
        public static DateTime ToDate = DateTime.Now.AddDays(90);// Program.ToDate;

    }

}
