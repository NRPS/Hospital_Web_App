using System;
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
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/PatientBill/5
        public void Delete(int id)
        {

        }

        #endregion





        private void GetPatientBillList(DataSet PatientBillDS)
        {
            try
            {
                PatientBills = PatientBillDS.Tables[0].AsEnumerable().Select(r =>
                new PatientBill
                {
                    BillNo = r.Field<string>("BillNo"),
                    BillDate = r.Field<DateTime>("BillDate"),
                    PatientID = r.Field<string>("PatientID"),
                    Remarks = r.Field<string>("Remarks"),
                    ID = r.Field<Int32>("ID")
                }).ToList();

                PatientBillDetails = PatientBillDS.Tables[1].AsEnumerable().Select(r =>
                new PatientBillDetails
                {
                    ID = r.Field<Int16>("ID"),
                    BillNo = r.Field<string>("BillNo"),
                    Amount = r.Field<decimal>("Amount"),
                    Discount = r.Field<decimal>("Discount"),
                    Rate = r.Field<decimal>("Rate"),
                    Quantity = r.Field<Int16>("Quantity"),
                    NetAmount = r.Field<decimal>("NetAmount"),
                    PatientBillDetailID = r.Field<Int16>("PatientBillDetailID"),
                    FromDate = r.Field<DateTime>("FromDate"),
                    ToDate = r.Field<DateTime>("ToDate")

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
            return du.AddRow(@"insert into PatientBill(BillNo,PatientID ,BillDate, UserID ,AddDate ,ModifiyDate,IsDeleted, Remarks)
            values(" + patientBill.BillNo + ",'" + patientBill.PatientID + "', '" + patientBill.BillDate + "', " + patientBill.UserID + ", '" + patientBill.AddDate
            + "', '" + patientBill.ModifiyDate + "', " + patientBill.IsDeleted + ",  '" + patientBill.Remarks
            + "')");
        }

        private bool UpdatePatientBill(PatientBill patientBill)
        {
            return du.AddRow(@"update PatientBill set PatientID = " + patientBill.PatientID + ",BillDate = " + patientBill.BillDate + ",UserID = " + patientBill.UserID + ",ModifiyDate = " + patientBill.ModifiyDate + ",Remarks = " + patientBill.Remarks + ")");
        }

        private bool DeletePatientBill(PatientBill patientBill)
        {
            return du.DeleteRow(@"update PatientBill set DeleteFlag = 1 where BillNo ='" + patientBill.BillNo + "')");
        }

    }
}
