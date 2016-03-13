
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

        PatientRegstrationController()
        {

        }

        #region  CURD

        // POST: api/ReferedBy
        public IHttpActionResult Post([FromBody]Patient patient)
        {
            if (AddPatient(patient) == true)
                return Ok();
            else
                return BadRequest();
        }
        public IHttpActionResult Put([FromBody]Patient patient)
        {
            if (UpdatedPatient(patient) == true)
                return Ok();
            else
                return BadRequest();
        }
        public IHttpActionResult Delete(string ID)
        {
            if (UpdatedPatient(ID) == true)
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
                    ConsultantFee = r.Field<Decimal>("ConsultantFee"),
                    Email = r.Field<string>("Email"),
                    Sex = r.Field<string>("Sex"),
                    Age = r.Field<Int16>("Age"),
                    ContactNumber1 = r.Field<string>("ContactNumber1"),
                    ContactNumber2 = r.Field<string>("ContactNumber2"),
                    DepartmentID = r.Field<Int16>("DepartmentID"),
                    IsFeeFree = r.Field<Boolean>("IsFeeFree"),
                    RefDrID = r.Field<Int16>("RefDrID"),
                    RegDate = r.Field<DateTime>("RegDate"),
                    Type = r.Field<Int16>("Type"),
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
                

                patient.ID = basic.GetMax("PatientRegstration", "ID") + 1;
                patient.PatientID = basic.GetKey(patient.ID, 'P');

                du.AddRow(@"insert into PatientRegstration(  ID ,   PatientID ,   Name ,   AttendentName ,   Sex ,  
                                ContactNumber1 ,   ContactNumber2 ,  Email ,   Address ,   RefDrID ,   Type ,   IsFeeFree ,   ConsultantName ,   DepartmentID ,  
                                ConsultantFee ,   RegDate ,   RegTime ,   UserID ,   AddDate ,   ModifiyDate ,   IsDeleted ,   Fyear ,  
                                CompanyCode ,   Remarks ,   IsPaymentPaid ) 
            values(" + patient.ID + ",'" + patient.PatientID + "', '" + patient.Name + "', '" + patient.AttendentName + "', '" + patient.Sex
               + "', '" + patient.ContactNumber1 + "', '" + patient.ContactNumber2 + "', '" + patient.Email + "', '" + patient.Address + "', " + patient.RefDrID
               + ", " + patient.Type + ", " + patient.IsFeeFree + ", '" + patient.ConsultantName + "', " + patient.DepartmentID
               + ", " + patient.ConsultantFee + ", '" + patient.RegDate + "', '" + patient.RegTime + "', " + patient.UserID + ", '" + patient.AddDate
               + "', '" + patient.ModifiyDate + "', " + patient.IsDeleted + ", " + patient.Fyear + ", '" + patient.CompanyCode + "', '" + patient.Remarks
               + "', " + patient.IsPaymentPaid + ")");

                Int32 id = basic.GetMax("Payment", "ID") + 1;
                string paymentReceiptNo = basic.GetKey(id, 'C');

                du.AddRow(@"insert into Payment(ID, PaymentReceiptNo, PatientID, PaymentDate, Amount, PaymentMode, UserID, AddDate, ModifiyDate, 
                        IsDeleted, BillNo, RegistratonNo, Remarks) 
            values(" + id + ",'" + paymentReceiptNo + "', '" + patient.PatientID + "', '" + patient.RegDate + "', " + patient.ConsultantFee
             + ", 'C', " + patient.UserID + ", '" + patient.AddDate + "', '" + patient.ModifiyDate + "', " + patient.IsDeleted
             + ", '', '" + patient.PatientID + "', 'Regsitration Fee'" + ")");
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
                patient.CompanyCode = LogDetails.CurrentCompanyCode;

                du.AddRow(@"update " + TableName + " set Name= '" + patient.Name + "', AttendentName= '" + patient.AttendentName + "', Sex= '" + patient.Sex + 
                    "', ContactNumber1 = '" + patient.ContactNumber1 + "', ContactNumber2 = '" + patient.ContactNumber2 + "', Email = '" + patient.Email + 
                    "', Address = '" + patient.Address + "', RefDrID = " + patient.RefDrID + "', Type = " + patient.Type + 
                    ", IsFeeFree = " + patient.IsFeeFree + ", ConsultantName = '" + patient.ConsultantName + "', DepartmentID = " + patient.DepartmentID + 
                    "', RegDate = '" + patient.Name + "', RegTime = '" + patient.Name + "', UserID = '" + patient.Name + 
                    ", ModifiyDate = '" + patient.ModifiyDate + "', Remarks = '" + patient.Remarks + "', where PatientID = '" + patient.PatientID + "')");

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        private bool UpdatedPatient(string ID)
        {
            try
            {
                du.DeleteRow(@"update PatientRegstration set DeleteFlag = 1 where PatientID ='" + ID + "')");

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
