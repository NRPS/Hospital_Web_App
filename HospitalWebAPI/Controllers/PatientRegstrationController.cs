
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

        PatientRegstrationController()
        {
            GetPatientList();
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
            return Patients;
        }

        //public Patient Get(string ID)
        //{
        //    Patient patient = Patients.FirstOrDefault((p) => p.PatientID == ID);
        //    return patient;
        //}

        public IHttpActionResult Get(string ID)
        {
            var patient = Patients.FirstOrDefault((p) => p.PatientID == ID);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        #endregion

        #region Priavte

        private void GetPatientList()
        {

            Patients = du.GetTable("PatientRegstration").Tables[0].AsEnumerable().Select(r =>
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
               ContactNumber1 = r.Field<string>("ContactNumber1"),
               ContactNumber2 = r.Field<string>("ContactNumber2"),
               DepartmentID = r.Field<Int16>("DepartmentID"),
               IsFeeFree = r.Field<Boolean>("IsFeeFree"),
               RefDrID = r.Field<Int16>("RefDrID"),
                // RegDate = r.Field<DateTime>("RegDate"),
                RegTime = r.Field<string>("RegTime"),
               Remarks = r.Field<string>("Remarks")
           }).ToList();
        }

        private DataSet GetPatient(string TableName, string PatientID)
        {
            String condition = PatientID == "" ? "" : " PatientID='" + PatientID + "'";
            return du.GetTable(TableName, condition);
        }
        private bool AddPatient(Patient patient)
        {

            Basic basic = new Basic();
            patient.ID = basic.GetMax("PatientRegstration", "ID");
            patient.PatientID = basic.GetKey(patient.ID);

            du.AddRow(@"insert into PatientRegstration(  ID ,   PatientID ,   Name ,   AttendentName ,   Sex ,  
                                ContactNumber1 ,   ContactNumber2 ,  Email ,   Address ,   RefDrID ,   Type ,   IsFeeFree ,   ConsultantName ,   DepartmentID ,  
                                ConsultantFee ,   RegDate ,   RegTime ,   UserID ,   AddDate ,   ModifiyDate ,   IsDeleted ,   Fyear ,  
                                CompanyCode ,   Remarks ,   IsPaymentPaid ) 
            values(" + patient.ID + ",'" + patient.PatientID + "', '" + patient.Name + "', '" + patient.AttendentName + "', '" + patient.Sex
           + "', '" + patient.ContactNumber1 + "', '" + patient.ContactNumber2 + "', '" + patient.Email + "', '" + patient.Address + "', " + patient.RefDrID
           + ", '" + patient.Type + "', " + patient.IsFeeFree + ", '" + patient.ConsultantName + "', " + patient.DepartmentID
           + ", " + patient.ConsultantFee + ", '" + patient.RegDate + "', '" + patient.RegTime + "', " + patient.UserID + ", '" + patient.AddDate
           + "', '" + patient.ModifiyDate + "', " + patient.IsDeleted + ", " + patient.Fyear + ", '" + patient.CompanyCode + "', '" + patient.Remarks
           + "', " + patient.IsPaymentPaid + ")");


            return true;



        }

        private bool UpdatedPatient(Patient patient)
        {
            du.AddRow(@"update PatientRegstration set Name= '" + patient.Name + "' where PatientID = '" + patient.PatientID + "')");

            return true;
        }

        private bool UpdatedPatient(string ID)
        {
            du.DeleteRow(@"update PatientRegstration set DeleteFlag = 1 where PatientID ='" + ID + "')");

            return true;
        }

        #endregion

    }
}
