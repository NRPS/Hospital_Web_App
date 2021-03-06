﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalWebAPI.Models;
using CommanUtilities;
using System.Data;
using System.Web.Http;

namespace HospitalWebAPI.Controllers
{
    public class PatientBillController : ApiController
    {

        MSAccessDataUtility du = new MSAccessDataUtility();
        List<PatientBill> PatientBills = new List<PatientBill>();
        List<PatientBillDetails> PatientBillDetails = new List<PatientBillDetails>();

        PaymentLocal paymentLocal = new PaymentLocal();
        Payment payment;


        string TableName = "PatientBill";
        string TableName1 = "PatientBillDetails";

        #region  CURD


        // GET: api/PatientBills
        public IEnumerable<PatientBill> Get()
        {
            GetPatientBillList(GetPatientBills(TableName, TableName1, ""));
            return PatientBills;
        }

        // GET: api/PatientBills/5
        public IHttpActionResult Get(string id)
        {
            GetPatientBillList(GetPatientBills(TableName, TableName1, id));

            var PatientBill = PatientBills.Where(x => x.BillNo == id).FirstOrDefault();

            if (PatientBillDetails.Count > 0)
                PatientBill.BillDetails = PatientBillDetails.Where(x => x.BillNo == id).ToList();

            if (PatientBill == null)
                return NotFound();

            return Ok(PatientBill);
        }

        // POST: api/PatientBill
        public IHttpActionResult Post([FromBody]PatientBill patientBill)
        {
            if (AddPatientBill(patientBill) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/PatientBill/5
        public IHttpActionResult Put([FromBody]PatientBill patientBill)
        {
            if (UpdatePatientBill(patientBill) == true)
                return Ok();
            else
                return BadRequest();

        }

        // DELETE: api/PatientBill/5
        public IHttpActionResult Delete(string ID)
        {
            if (DeletePatientBill(ID) == true)
                return Ok();
            else
                return BadRequest();
        }

        #endregion


        private void GetPatientBillList(DataSet PatientBillDS)
        {
            try
            {
                PatientBills = PatientBillDS.Tables[0].AsEnumerable().Select(r =>
                new PatientBill
                {
                    ID = r.Field<Int32>("ID"),
                    BillNo = r.Field<string>("BillNo"),
                    BillDate = r.Field<DateTime?>("BillDate"),
                    PatientID = r.Field<string>("PatientID"),
                    BillTotal = r.Field<decimal?>("BillTotal"),
                    AmountPaid = r.Field<decimal?>("AmountPaid"),
                    Remarks = r.Field<string>("Remarks")
                }).ToList();

                PatientBillDetails = PatientBillDS.Tables[1].AsEnumerable().Select(r =>
                new PatientBillDetails
                {
                    BillID = r.Field<Int16>("BillID"),
                    BillNo = r.Field<string>("BillNo"),
                    ItemCode = r.Field<string>("ItemCode"),
                    LabCode = r.Field<string>("LabCode"),
                    Amount = r.Field<decimal?>("Amount"),
                    Discount = r.Field<decimal?>("Discount"),
                    Rate = r.Field<decimal?>("Rate"),
                    Quantity = r.Field<Int16?>("Quantity"),
                    NetAmount = r.Field<decimal?>("NetAmount"),
                    FromDate = r.Field<DateTime?>("FromDate"),
                    ToDate = r.Field<DateTime?>("ToDate")

                }).ToList();

            }
            catch (Exception Ex)
            {

            }
        }

        private DataSet GetPatientBills(string TableName, string TableName1, string BillNo)
        {
            try
            {
                DataSet DS = new DataSet();
                DataTable dt = new DataTable();

                String condition = BillNo == "" ? "" : " BillNo = '" + BillNo + "'";

                dt = du.GetTableValue(TableName, condition);

                DS.Tables.Add(dt);

                dt = new DataTable();

                dt = du.GetTableValue(TableName1, condition);

                DS.Tables.Add(dt);

                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        private bool AddPatientBill(PatientBill patientBill)
        {
            Basic basic = new Basic();

            try
            {

                patientBill.ID = basic.GetMax("PatientBill", "ID") + 1;
                patientBill.BillNo = basic.GetKey(patientBill.ID, 'B', true, true);

                patientBill.AddDate = DateTime.Now.Date;
                patientBill.ModifiyDate = DateTime.Now.Date;
                patientBill.IsDeleted = LogDetails.DeletedFalse;
                patientBill.Fyear = LogDetails.CurrentFinancialYear;
                patientBill.UserID = LogDetails.UserId;
                patientBill.CompanyCode = LogDetails.CurrentCompanyCode;


                du.AddRow(@"insert into PatientBill(ID,BillNo,PatientID ,BillDate, BillTotal, AmountPaid,PaymentMode, Remarks, UserID, CompanyCode, Fyear ,AddDate ,ModifiyDate,IsDeleted)
                values(" + patientBill.ID + ",'" + patientBill.BillNo + "','" + patientBill.PatientID + "', '" + patientBill.BillDate + "', " + patientBill.BillTotal
                + "," + patientBill.AmountPaid + ",'" + patientBill.PaymentMode + "','" + patientBill.Remarks + "' ," + patientBill.UserID + ", '" + patientBill.CompanyCode + "', " + patientBill.Fyear + ", '" + patientBill.AddDate + "', '"
                + patientBill.ModifiyDate + "', " + patientBill.IsDeleted + ")");


                //  List<PatientBillDetails> patientBillDetails = new List<PatientBillDetails>();

                int X = basic.GetMax("PatientBillDetails", "BillID", " billno = '" + patientBill.BillNo + "'") + 1;

                foreach (PatientBillDetails patientBillDetails in patientBill.BillDetails)
                {
                    patientBillDetails.BillID = X++;
                    patientBillDetails.BillNo = patientBill.BillNo;
                    patientBillDetails.FromDate = DateTime.Now.Date;
                    patientBillDetails.ToDate = DateTime.Now.Date;

                    du.AddRow(@"insert into PatientBillDetails(BillID,BillNo,ItemCode, LabCode,Amount ,Discount, Remarks, Rate, Quantity, NetAmount ,FromDate ,ToDate)
                values(" + patientBillDetails.BillID + ",'" + patientBillDetails.BillNo + "','" + patientBillDetails.ItemCode + "','" + patientBillDetails.LabCode + "',"
                    + patientBillDetails.Amount + ", " + patientBillDetails.Discount + ", '"
                    + patientBillDetails.Remarks + "', " + patientBillDetails.Rate + "," + patientBillDetails.Quantity + "," + patientBillDetails.NetAmount + ",'"
                    + patientBillDetails.FromDate + "', '" + patientBillDetails.ToDate + "')");
                }

                payment = new Payment();

                payment.BillNo = patientBill.BillNo;
                payment.PatientID = patientBill.PatientID;
                payment.Amount = patientBill.AmountPaid;
                payment.Remarks = patientBill.Remarks;
                payment.PaymentDate = patientBill.BillDate;
                payment.PaymentMode = patientBill.PaymentMode;

                paymentLocal.AddPayment(payment);

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }

        private bool UpdatePatientBill(PatientBill patientBill)
        {
            patientBill.UserID = LogDetails.UserId;
            patientBill.ModifiyDate = DateTime.Now.Date;

            du.AddRow(@"update PatientBill set BillTotal = " + patientBill.BillTotal + ",  AmountPaid = " + patientBill.AmountPaid + ", BillDate = '"
               + patientBill.BillDate + "', PaymentMode = '" + patientBill.PaymentMode + "', Remarks = '" + patientBill.Remarks + "', UserID = "
               + patientBill.UserID + ", ModifiyDate = '" + patientBill.ModifiyDate + "' where BillNo='" + patientBill.BillNo + "'");


            PatientBillDetails patientBillDetails = new PatientBillDetails();

            patientBillDetails = patientBill.BillDetails[0];
            patientBillDetails.FromDate = DateTime.Now.Date;
            patientBillDetails.ToDate = DateTime.Now.Date;
            patientBillDetails.BillID = 1;

            du.AddRow(@"update PatientBillDetails set Amount = " + patientBillDetails.Amount + ", Discount=" + patientBillDetails.Discount + ", Remarks='"
                + patientBillDetails.Remarks + "', ItemCode ='" + patientBillDetails.ItemCode + "', LabCode ='" + patientBillDetails.LabCode + "', Rate= "
                + patientBillDetails.Rate + ", Quantity = "
                + patientBillDetails.Quantity + ", NetAmount =" + patientBillDetails.NetAmount + ", FromDate ='" + patientBillDetails.FromDate
                + "', ToDate = '" + patientBillDetails.ToDate + "' where BillNo='" + patientBill.BillNo + "' and BillID=" + patientBillDetails.BillID);

            payment = new Payment();


            string paymentReceiptNo = du.GetScalarValueString("select PaymentReceiptNo from Payment where BillNo ='" + patientBill.BillNo + "'");

            payment.PaymentReceiptNo = paymentReceiptNo;
            payment.BillNo = patientBill.BillNo;
            payment.PatientID = patientBill.PatientID;
            payment.Amount = patientBill.AmountPaid;
            payment.Remarks = patientBill.Remarks;
            payment.PaymentDate = patientBill.BillDate;
            payment.PaymentMode = patientBill.PaymentMode;

            paymentLocal.UpdatePayment(payment);

            return true;

        }

        private bool DeletePatientBill(string BillNo)
        {
            string paymentReceiptNo = du.GetScalarValueString("select PaymentReceiptNo from Payment where BillNo ='" + BillNo + "'");

            du.DeleteRow(@"update PatientBill set IsDeleted = 1 where BillNo ='" + BillNo + "'");

            paymentLocal.DeletePayment(paymentReceiptNo);

            return true;
        }

    }
}
