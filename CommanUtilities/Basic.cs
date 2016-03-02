using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanUtilities
{
    public class Basic
    {
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
            // SqlConnection Connnection;
            int i;
            String commandString;
            MSAccessDataUtility objDataUtility = new MSAccessDataUtility();
            i = 0;
            try
            {
                commandString = "select  max(" + FieldName + ") from " + TableName;
                i = objDataUtility.GetScalarValue(commandString);
            }
            catch (Exception Ex)
            {
                throw Ex;
                //MessageBox.Show(Ex.Message);
            }
            return i;
        }

        //public int GetMax(String TableName, String FieldName, String WhereCondition)
        //{
        //    //CRFNO(15#),SRFSR(bigint)
        //   // SqlConnection Connnection;
        //    int i;
        //    String commandString;
        //    i = 0;
        //    try
        //    {
        //        if (WhereCondition.Length > 0)
        //        {
        //            commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where " + WhereCondition;
        //        }
        //        else
        //        {
        //            commandString = "select max(" + FieldName + ") as maxid from " + TableName;
        //        }
        //        i = ExecuteQueryString(commandString);
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //    return i;
        //}

        //public int GetMax(String TableName, String FieldName, String WhereCondition, bool WithDivID)
        //{
        //    //CompanyID (3#), DivID (3#), Fyear (4#), Srno (5#)
        //    //CompanyID (3#), Fyear (4#), Srno (5#)
        //    if (WhereCondition.Length > 0)
        //        WhereCondition = " and " + WhereCondition;
        //   // SqlConnection Connnection;
        //    LogDetails logdetails = new LogDetails();
        //    int i;
        //    String commandString;
        //    i = 0;
        //    try
        //    {
        //        if (WithDivID ==true)
        //        {
        //            commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and divid='" + logdetails.CurrentDivID + "' and fyear=" + logdetails.CurrentFinancialYear + " "+ WhereCondition;
        //        }
        //        else
        //        {
        //            commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and fyear=" + logdetails.CurrentFinancialYear + " " + WhereCondition;
        //        }
        //        i = ExecuteQueryString(commandString);
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //    return i;
        //}

        //public int GetMax(String TableName, String FieldName, bool WithDivID)
        //{
        //    //CompanyID (3#), DivID (3#), Fyear (4#), Srno (5#)
        //    //CompanyID (3#), Fyear (4#), Srno (5#)

        //    // SqlConnection Connnection;
        //    LogDetails logdetails = new LogDetails();
        //    int i;
        //    String commandString;
        //    i = 0;
        //    try
        //    {
        //        if (WithDivID == true)
        //        {
        //            commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and divid='" + logdetails.CurrentDivID + "' and fyear=" + logdetails.CurrentFinancialYear;
        //        }
        //        else
        //        {
        //            commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and fyear=" + logdetails.CurrentFinancialYear;
        //        }
        //        i = ExecuteQueryString(commandString);
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //    return i;
        //}

        //public int GetMax(String TableName, String FieldName,DateTime datetime)
        //{
        //    //SqlCommand command = new SqlCommand("select sum(intresttdsamount) as intTotal from promissorey where browcode='" + BorrowerCodetextBox.Text + "' and landercode='" + LenderCodetextBox.Text + "' and permotDate<@dt", Connnection);
        //    Basic basic = new Basic();
        //    SqlConnection Connnection = new SqlConnection(basic.ConnString);
        //    int i = 0;
        //    try
        //    {

        //        SqlCommand command = new SqlCommand("select max(" + FieldName + ") as maxid from " + TableName + " Where LotDate>=@dt", Connnection);
        //        SqlParameter Sp = new SqlParameter("dt", DbType.Date);
        //        Sp.Value = datetime.Date;
        //        command.Parameters.Add(Sp);
        //        Connnection.Open();
        //        SqlDataReader Datareader = command.ExecuteReader();
        //        if (Datareader.HasRows)
        //        {
        //            while (Datareader.Read())
        //            {
        //                if (!(Convert.IsDBNull(Datareader[0]) == true))
        //                    i = Convert.ToInt32(Datareader[0]);
        //            }
        //        }
        //        Datareader.Close();
        //        Connnection.Close();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Connnection.Close();
        //        MessageBox.Show(Ex.Message);
        //    }
        //    return i;
        //}

        //public int GetMax(String TableName, String FieldName, String WhereCondition, bool WithDivID)
        //{
        //    //CompanyID (3#), DivID (3#), Fyear (4#), Srno (5#)
        //    //CompanyID (3#), Fyear (4#), Srno (5#)
        //                LogDetails logdetails = new LogDetails();
        //    int i;
        //    String commandString;
        //    i = 0;
        //    try
        //    {
        //        if (WhereCondition.Length > 0)
        //        {
        //            if (WithDivID == true)
        //            {
        //                commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and divid='" + logdetails.CurrentDivID + "' and fyear=" + logdetails.CurrentFinancialYear + " and " + WhereCondition;
        //            }
        //            else
        //            {
        //                commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and fyear=" + logdetails.CurrentFinancialYear;
        //            }
        //        }
        //        else
        //        {
        //            if (WithDivID == true)
        //            {
        //                commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and divid='" + logdetails.CurrentDivID + "' and fyear=" + logdetails.CurrentFinancialYear;
        //            }
        //            else
        //            {
        //                commandString = "select max(" + FieldName + ") as maxid from " + TableName + " Where CompanyCode='" + logdetails.CurrentCompanyCode + "' and fyear=" + logdetails.CurrentFinancialYear;
        //            }
        //        }

        //        i = ExecuteQueryString(commandString);

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //    return i;
        //}

        //public String GetKey(int Srno,Char Prefix,String Pad)
        //{
        //    String s="";
        //    try
        //    {
        //        s = Srno.ToString();
        //        if(Prefix=='~')
        //            s = Pad.Substring(0, (Pad.Length - s.Length)) + s;
        //        else
        //            s =Prefix + Pad.Substring(0, (Pad.Length - s.Length)) + s;
        //    }
        //    catch (Exception Ex)
        //    {
        //       // MessageBox.Show(Ex.Message);
        //        return s;
        //    }
        //    return s;
        //}

        public String GetKey(int Srno)
        {

            //FOR ALL VOUCHER CRFNO WILL CONTINUE AS FINANCIAL YEAR


            String s, s1;
            s = ""; s1 = "";
            //  LogDetails logdetails = new LogDetails();
            try
            {
                s = Srno.ToString();
                s1 = LogDetails.CurrentFinancialYear.ToString();
                s = LogDetails.CurrentCompanyCode + LogDetails.CurrentDivID + s1 + "0000".Substring(0, (5 - s.Length)) + s;
            }
            catch (Exception Ex)
            {
                // MessageBox.Show(Ex.Message);
                return s;
            }
            return s;
        }

        public String GetKey(int Srno, Char ch)
        {

            //    Sale, Purchasse, Process, Ready Stock 
            //    CRFNO (15#)  = CompanyID (3#) + DivID (3#) + Fyear(4#)+Srno(5#)


            //    Ledger, Group
            //    CRFNO(12#)  = CompanyID (3#), Fyear (4#), Srno (5#)

            /*
                CRFNo Index Key
                I-PURCHASE;
             *  S-SALE CASH 
             *  H-PCR
             *  C-PAYMENT/RECEIPT
             *  Q
             */

            //
            /* L-Ledger
             * G-Group
             * P-Party
             */

            String s, s1;
            s = ""; s1 = "";
            // LogDetails logdetails = new LogDetails();
            try
            {
                s = Srno.ToString();
                s1 = LogDetails.CurrentFinancialYear.ToString();
                switch (ch)
                {
                    case 'G':
                    case 'L':
                    case 'I':
                    case 'H':
                    case 'M':
                    case 'S':
                    case 'C':
                    case 'R':
                    case 'K':
                    case 'Q':
                    case 'E':
                    case 'T':
                    case 'A':
                    case 'B':
                        s = LogDetails.CurrentCompanyCode + s1 + ch + "0000".Substring(0, (5 - s.Length)) + s;
                        break;
                    case 'P':
                        s = LogDetails.CurrentCompanyCode.Substring(1) + "P000".Substring(0, (4 - s.Length)) + s;
                        break;
                    default:
                        s = LogDetails.CurrentCompanyCode + s1 + "0000".Substring(0, (5 - s.Length)) + s;
                        break;
                }
            }
            catch (Exception Ex)
            {
                // MessageBox.Show(Ex.Message);
                return s;
            }
            return s;

        }

        //public String GetKey(int Srno, Char ch, DateTime lotdate)
        //{
        //    //   for  Lotnot only

        //    String s, s1,day,month;
        //    s = ""; s1 = "";
        //    LogDetails logdetails = new LogDetails();
        //    try
        //    {
        //        s = Srno.ToString();
        //        s1 = logdetails.CurrentFinancialYear.ToString();
        //        switch (ch)
        //        {
        //            case 'T':
        //               // day = lotdate.Day.ToString("0#");
        //               // month =lotdate.Month.ToString("0#");
        //                s = logdetails.CurrentCompanyCode + "/" + lotdate.Day.ToString("0#") + lotdate.Month.ToString("0#") + lotdate.Year.ToString().Substring(2, 2) + "/" + s1 + "/00".Substring(0, (4 - s.Length)) + s;
        //                break;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //      //  MessageBox.Show(Ex.Message);
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
