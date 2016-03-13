using CommanUtilities;
using HospitalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HospitalWebAPI.Controllers
{
    public class PaymentController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<Payment> Payments = new List<Payment>();
        string TableName = "Payment";

        #region  CURD


        // GET: api/Payment
        public IEnumerable<Payment> Get()
        {
            GetPaymentList(GetPayment(TableName, ""));
            return Payments;
        }

        // GET: api/Payment/5
        public IHttpActionResult Get(string id)
        {
            GetPaymentList(GetPayment(TableName, id));

            var Payment = Payments.Where(x => x.PaymentReceiptNo == id).FirstOrDefault();

            if (Payment == null)
                return NotFound();

            return Ok(Payment);
        }

        // POST: api/Payment
        public IHttpActionResult Post([FromBody]Payment Payment)
        {
            if (AddPayment(Payment) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/Payment/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Payment/5
        public void Delete(int id)
        {

        }

        #endregion

        #region Priavte

        private void GetPaymentList(DataSet PatientsDS)
        {
            try
            {
                Payments = PatientsDS.Tables[0].AsEnumerable().Select(r =>
                new Payment
                {
                    ID = r.Field<Int32>("ID"),
                    PaymentReceiptNo = r.Field<string>("PaymentReceiptNo"),
                    PatientID = r.Field<string>("PatientID"),
                    Amount = r.Field<Decimal>("Amount"),
                    BillNo = r.Field<string>("BillNo"),
                    PaymentDate = r.Field<DateTime>("PaymentDate"),
                    PaymentMode = r.Field<string>("PaymentMode"),
                    RegistratonNo = r.Field<string>(""),
                    Remarks = r.Field<string>("Remark"),
                }).ToList();
            }
            catch (Exception Ex)
            {

            }
        }


        private DataSet GetPayment(string TableName, string paymentReceiptNo)
        {
            try
            {
                String condition = paymentReceiptNo == "" ? "" : " paymentReceiptNo = " + paymentReceiptNo;
                return du.GetTable(TableName, condition);
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        private bool AddPayment(Payment payment)
        {
            try
            {

                Basic basic = new Basic();


                Int32 id = basic.GetMax("Payment", "ID") + 1;
                string paymentReceiptNo = basic.GetKey(id, 'C');

                du.AddRow(@"insert into Payment(ID, PaymentReceiptNo, PatientID, PaymentDate, Amount, PaymentMode, UserID, AddDate, ModifiyDate, 
                        IsDeleted, BillNo, RegistratonNo, Remarks) 
            values(" + id + ",'" + paymentReceiptNo + "', '" + payment.PatientID + "', '" + payment.PaymentDate + "', " + payment.Amount
             + ", '" + payment.PaymentMode + "', " + payment.UserID + ", '" + payment.AddDate + "', '" + payment.ModifiyDate + "', " + payment.IsDeleted
             + ",'" + payment.BillNo + "', '" + payment.PatientID + "', '" + payment.Remarks + "')");

                return true;
            }

            catch (Exception Ex)
            {
                return false;
            }
        }

        #endregion

    }
}