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

        PaymentController()
        { }
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
        public IHttpActionResult Post([FromBody]Payment payment)
        {
            PaymentLocal paymentLocal = new PaymentLocal();

            if (paymentLocal.AddPayment(payment) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/Payment/5
        public IHttpActionResult Put(int id, [FromBody]Payment payment)
        {
            PaymentLocal paymentLocal = new PaymentLocal();

            if (paymentLocal.UpdatePayment(payment) == true)
                return Ok();
            else
                return BadRequest();
        }

        // DELETE: api/Payment/5
        public IHttpActionResult Delete(string id)
        {
            PaymentLocal paymentLocal = new PaymentLocal();

            if (paymentLocal.DeletePayment(id) == true)
                return Ok();
            else
                return BadRequest();

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
                    RegistratonNo = r.Field<string>("RegistratonNo"),
                    Remarks = r.Field<string>("Remarks"),
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
                String condition = paymentReceiptNo == "" ? "" : " paymentReceiptNo = '" + paymentReceiptNo + "'";
                return du.GetTable(TableName, condition);
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
    #endregion

}


public class PaymentLocal
{
    MSAccessDataUtility du = new MSAccessDataUtility();

    public bool AddPayment(Payment payment)
    {
        try
        {

            if (payment.ID <= 0)
            {
                Basic basic = new Basic();
                payment.ID = basic.GetMax("Payment", "ID") + 1;
                payment.RegistratonNo = basic.GetKey(payment.ID, 'C', true, true);
            }

          return  du.AddRow(@"insert into Payment(ID, PaymentReceiptNo, PatientID, PaymentDate, Amount, PaymentMode, UserID, AddDate, ModifiyDate, 
                        IsDeleted, BillNo, RegistratonNo, Remarks) 
            values(" + payment.ID + ",'" + payment.PaymentReceiptNo + "', '" + payment.PatientID + "', '" + payment.PaymentDate + "', " + payment.Amount
         + ", '" + payment.PaymentMode + "', " + payment.UserID + ", '" + payment.AddDate + "', '" + payment.ModifiyDate + "', " + payment.IsDeleted
         + ",'" + payment.BillNo + "', '" + payment.PatientID + "', '" + payment.Remarks + "')");

        }

        catch (Exception Ex)
        {
            return false;
        }
    }
    public bool UpdatePayment(Payment payment)
    {
        try
        {

            payment.ModifiyDate = DateTime.Now.Date;
            payment.UserID = LogDetails.UserId;


           return du.AddRow(@"update Payment set PaymentDate = '" + payment.PaymentDate + "', Amount = " + payment.Amount + ", PaymentMode = '" + payment.PaymentMode
                + "', UserID = " + payment.UserID + ", ModifiyDate= '" + payment.ModifiyDate + "', BillNo = '" + payment.BillNo + "', RegistratonNo='"
                + payment.PatientID + "', Remarks = '" + payment.Remarks + "' where PaymentReceiptNo = '" + payment.PaymentReceiptNo + "'");

        }

        catch (Exception Ex)
        {
            return false;
        }
    }

    public bool DeletePayment(String paymentReceiptNo)
    {
        try
        {

           return du.AddRow(@"update Payment set  UserID = " + LogDetails.UserId + ", ModifiyDate= '" + DateTime.Now.Date
                + "',  IsDeleted = 1 where PaymentReceiptNo = '" + paymentReceiptNo + "'");

        }
        catch (Exception Ex)
        {
            return false;
        }

    }
}