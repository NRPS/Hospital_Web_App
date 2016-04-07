
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommanUtilities;
using HospitalWebAPI.Models;
using System.Web.Http.Cors;
using System.Data;
using Newtonsoft.Json;

namespace HospitalWebAPI.Controllers
{

    public class PatientRegstrationController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<Patient> Patients = new List<Patient>();
        string TableName = "PatientRegstration";
        PaymentLocal paymentLocal = new PaymentLocal();
        Payment payment;

        PatientRegstrationController()
        {

        }

        #region  CURD

       
        public IHttpActionResult Post([FromBody]Patient patient)
        {
            if (AddPatient(patient) == true)
                return Ok();
            else
                return BadRequest();
        }


        [AcceptVerbs("PUT")]
        public IHttpActionResult Put([FromBody]Patient patient)
        {
            if (UpdatedPatient(patient) == true)
                return Ok();
            else
                return BadRequest();
        }
        public IHttpActionResult Delete(string ID)
        {
            if (DeletePatient(ID) == true)
                return Ok();
            else
                return BadRequest();
        }

        public IEnumerable<Patient> Get()
        {
            GetPatientList(GetPatient(TableName, ""));
            return Patients;
        }

        //public Patient Get(string ID)
        //{
        //    Patient patient = Patients.FirstOrDefault((p) => p.PatientID == ID);
        //    return patient;
        //}

        public IHttpActionResult Get(string ID)
        {
            GetPatientList(GetPatient(TableName, ID));

            var patient = Patients.FirstOrDefault((p) => p.PatientID == ID);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        #endregion

        #region Priavte

        private void GetPatientList(DataSet PatientsDS)
        {

            try
            {
                Patients = PatientsDS.Tables[0].AsEnumerable().Select(r =>
                new Patient
                {
                    ID = r.Field<Int32>("ID"),
                    PatientID = r.Field<string>("PatientID"),
                    Name = r.Field<string>("name"),
                    Address = r.Field<string>("Address"),
                    AttendentName = r.Field<string>("AttendentName"),
                    ConsultantName = r.Field<string>("ConsultantName"),
                    ConsultantFee = r.Field<Decimal?>("ConsultantFee"),
                    Email = r.Field<string>("Email"),
                    Sex = r.Field<string>("Sex"),
                    Age = r.Field<Int16?>("Age"),
                    ContactNumber1 = r.Field<string>("ContactNumber1"),
                    ContactNumber2 = r.Field<string>("ContactNumber2"),
                    DepartmentID = r.Field<Int16?>("DepartmentID"),
                    IsFeeFree = r.Field<Boolean?>("IsFeeFree"),
                    RefByID = r.Field<Int16?>("RefByID"),
                    RegDate = r.Field<DateTime?>("RegDate"),
                    TypeID = r.Field<Int16?>("TypeID"),
                    RegTime = r.Field<string>("RegTime"),
                    Remarks = r.Field<string>("Remarks")
                }).ToList();
            }
            catch (Exception ex)
            {

            }
        }

        private DataSet GetPatient(string TableName, string PatientID)
        {
            try
            {
                String condition = PatientID == "" ? "" : " PatientID='" + PatientID + "'";
                return du.GetTable(TableName, condition);
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        private bool AddPatient(Patient patient)
        {

            Basic basic = new Basic();

            try
            {

                patient.AddDate = DateTime.Now.Date;
                patient.ModifiyDate = DateTime.Now.Date;
                patient.IsDeleted = LogDetails.DeletedFalse;
                patient.Fyear = LogDetails.CurrentFinancialYear;
                patient.UserID = LogDetails.UserId;
                patient.CompanyCode = LogDetails.CurrentCompanyCode;
                patient.TypeID = patient.TypeID == null ? 1 : patient.TypeID;
                patient.ID = basic.GetMax("PatientRegstration", "ID") + 1;
                patient.PatientID = basic.GetKey(patient.ID, 'P', true, true);
                patient.IsFeeFree = patient.IsFeeFree == null ?  false : patient.IsFeeFree;
                patient.RefByID = patient.RefByID==null? 0: patient.RefByID;
                patient.IsPaymentPaid = patient.IsPaymentPaid == null ? false : patient.IsPaymentPaid;
                patient.Sex = patient.Sex == null ? "F" : patient.Sex;

                du.AddRow(@"insert into PatientRegstration(  ID ,   PatientID ,   Name ,   AttendentName ,   Sex ,  
                                    ContactNumber1 ,   ContactNumber2 ,  Email ,   Address ,   RefByID ,   TypeID ,   IsFeeFree ,   ConsultantName ,   DepartmentID ,  
                                    ConsultantFee ,   RegDate ,   RegTime , PaymentMode,  UserID ,   AddDate ,   ModifiyDate ,   IsDeleted ,   Fyear ,  
                                    CompanyCode ,   Remarks ,   IsPaymentPaid,Age ) 
                values(" + patient.ID + ",'" + patient.PatientID + "', '" + patient.Name + "', '" + patient.AttendentName + "', '" + patient.Sex
               + "', '" + patient.ContactNumber1 + "', '" + patient.ContactNumber2 + "', '" + patient.Email + "', '" + patient.Address + "', " + patient.RefByID
               + ", " + patient.TypeID + ", " + patient.IsFeeFree + ", '" + patient.ConsultantName + "', " + patient.DepartmentID
               + ", " + patient.ConsultantFee + ", '" + patient.RegDate + "', '" + patient.RegTime + "','C', " + patient.UserID + ", '" + patient.AddDate
               + "', '" + patient.ModifiyDate + "', " + patient.IsDeleted + ", " + patient.Fyear + ", '" + patient.CompanyCode + "', '" + patient.Remarks
               + "', " + patient.IsPaymentPaid + ", " + patient.Age + ")");


                payment = new Payment();

                payment.RegistrationNo = patient.PatientID;
                payment.PatientID= patient.PatientID;
                payment.Amount = patient.ConsultantFee;
                payment.Remarks = "Regsitration Fee";
                payment.PaymentDate = patient.RegDate;
                payment.PaymentMode = "C";

                paymentLocal.AddPayment(payment);

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        private bool UpdatedPatient(Patient patient)
        {
            try
            {
                patient.ModifiyDate = DateTime.Now.Date;
                patient.UserID = LogDetails.UserId;
                patient.TypeID = patient.TypeID == null ? 1 : patient.TypeID;
                patient.IsFeeFree = patient.IsFeeFree == null ? false : patient.IsFeeFree;
                patient.RefByID = patient.RefByID == null ? 0 : patient.RefByID;
                patient.IsPaymentPaid = patient.IsPaymentPaid == null ? false : patient.IsPaymentPaid;
                patient.Sex = patient.Sex == null ? "F" : patient.Sex;


                du.AddRow(@"update " + TableName + " set Name= '" + patient.Name + "', AttendentName= '" + patient.AttendentName + "', Sex= '" + patient.Sex +
                    "', ContactNumber1 = '" + patient.ContactNumber1 + "', ContactNumber2 = '" + patient.ContactNumber2 + "', Email = '" + patient.Email +
                    "', Address = '" + patient.Address + "', RefByID = " + patient.RefByID + ", TypeID = " + patient.TypeID + ", ConsultantFee = " + patient.ConsultantFee +
                    ", IsFeeFree = " + patient.IsFeeFree + ", ConsultantName = '" + patient.ConsultantName + "', DepartmentID = " + patient.DepartmentID +
                    ", RegDate = '" + patient.RegDate + "', RegTime = '" + patient.RegTime + "', UserID = " + patient.UserID + 
                    ", ModifiyDate = '" + patient.ModifiyDate + "', Remarks = '" + patient.Remarks + "', IsPaymentPaid = " + patient.IsPaymentPaid + " where PatientID = '" + patient.PatientID + "'");

                string paymentReceiptNo = du.GetScalarValueString("select PaymentReceiptNo from Payment where RegistrationNo ='" + patient.PatientID + "'");

                payment = new Payment();

                payment.PaymentReceiptNo= paymentReceiptNo;
                payment.Amount = patient.ConsultantFee;
                payment.Remarks = "Regsitration Fee";
                payment.PaymentDate = patient.RegDate;
                payment.PaymentMode = "C";

                paymentLocal.UpdatePayment(payment);

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        private bool DeletePatient(string ID)
        {
            try
            {
                string paymentReceiptNo = du.GetScalarValueString("select PaymentReceiptNo from Payment where RegistrationNo ='" + ID + "'");

                du.DeleteRow(@"update PatientRegstration set IsDeleted = 1 where PatientID ='" + ID + "'");

                paymentLocal.DeletePayment(paymentReceiptNo);

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
