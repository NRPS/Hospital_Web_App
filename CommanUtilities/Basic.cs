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


        public String GetKey(int Srno, Char Prefix = '~', bool isCompanyCodePrefix = false, bool isYearPrefix = false, bool isMonthPrefix = false, int Length = 6)
        {
            /*
           CRFNo Index Key

        * P-Patient
       * B - Bill
       * C - PAYMENT / RECEIPT
       * Q
       */
            String s = "";
            if (isCompanyCodePrefix)
                s = LogDetails.CurrentCompanyCode;
            if (isYearPrefix)
                s = s + LogDetails.CurrentFinancialYear.ToString().Substring(2, 2);
            if (isMonthPrefix)
                s = s + DateTime.Now.Date.Month.ToString("##");
            if (Prefix != '~' && Prefix != ' ')
                s = s + Prefix;
            if (Length > 0)
            {
                string s1 = "0000000000".Substring(0, (Length - Srno.ToString().Length)) ;
                s = s + s1 + Srno.ToString();
            }
            else
            {
                s = s + Srno.ToString();
            }
            return s;
        }
        //public String GetKey(int Srno, Char Prefix, String Pad)
        //{
        //    String s = "";
        //    try
        //    {
        //        s = Srno.ToString();
        //        if (Prefix == '~')
        //            s = Pad.Substring(0, (Pad.Length - s.Length)) + s;
        //        else
        //            s = Prefix + Pad.Substring(0, (Pad.Length - s.Length)) + s;
        //    }
        //    catch (Exception Ex)
        //    {
        //        // MessageBox.Show(Ex.Message);
        //        return s;
        //    }
        //    return s;
        //}

        //public String GetKey(int Srno)
        //{
        //    return GetKey(Srno, '~');
        //}

        //public String GetKey(int Srno, Char ch)
        //{
        //    //FOR ALL VOUCHER CRFNO WILL CONTINUE AS FINANCIAL YEAR


        //    String s, s1;
        //    s = ""; s1 = "";
        //    //  LogDetails logdetails = new LogDetails();
        //    try
        //    {
        //        s = Srno.ToString();
        //        s1 = LogDetails.CurrentFinancialYear.ToString();
        //        s = LogDetails.CurrentCompanyCode + LogDetails.CurrentDivID + s1 + "0000".Substring(0, (5 - s.Length)) + s;
        //    }
        //    catch (Exception Ex)
        //    {
        //        // MessageBox.Show(Ex.Message);
        //        return s;
        //    }
        //    return s;


        //    //    Sale, Purchasse, Process, Ready Stock 
        //    //    CRFNO (15#)  = CompanyID (3#) + DivID (3#) + Fyear(4#)+Srno(5#)


        //    //    Ledger, Group
        //    //    CRFNO(12#)  = CompanyID (3#), Fyear (4#), Srno (5#)



        //    //
        //    /* L-Ledger
        //     * G-Group
        //     * P-Party
        //     */

        //    String s, s1;
        //    s = ""; s1 = "";
        //    // LogDetails logdetails = new LogDetails();
        //    try
        //    {
        //        s = Srno.ToString();
        //        s1 = LogDetails.CurrentFinancialYear.ToString();
        //        switch (ch)
        //        {
        //            case 'G':
        //            case 'L':
        //            case 'I':
        //            case 'H':
        //            case 'M':
        //            case 'S':
        //            case 'R':
        //            case 'K':
        //            case 'Q':
        //            case 'E':
        //            case 'T':
        //            case 'A':
        //            case 'B':
        //                s = LogDetails.CurrentCompanyCode + s1 + ch + "0000".Substring(0, (5 - s.Length)) + s;
        //                break;
        //            case 'P':
        //            case 'C':
        //                //s = LogDetails.CurrentCompanyCode.Substring(1) + "P000".Substring(0, (4 - s.Length)) + s;
        //                s = LogDetails.CurrentCompanyCode + DateTime.Now.Month.ToString() + s1 + ch + "0000".Substring(0, (5 - s.Length)) + s;

        //                break;
        //            default:
        //                s = LogDetails.CurrentCompanyCode + s1 + "0000".Substring(0, (5 - s.Length)) + s;
        //                break;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        // MessageBox.Show(Ex.Message);
        //        return s;
        //    }
        //    return s;

        //}
        #endregion


    }

    public static class LogDetails
    {

        public static String CurrentCompanyCode = "C01"; //Program.CurrentCompanyCode;
        public static int CurrentFinancialYear = 2016; //Program.CurrentFinancialYear;
        public static String CurrentDivID = "D01";     // Program.CurrentDivID;
        public static int UserId = 1; // Program.UserId;
        public static int DeletedTrue = 1;
        public static int DeletedFalse = 0;
        public static DateTime FromDate = DateTime.Now.Date;// Program.FromDate;
        public static DateTime ToDate = DateTime.Now.AddDays(90);// Program.ToDate;


    }

}
