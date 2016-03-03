
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
        private void GetPatientList()
        {

            var PatientList = du.GetTable("PatientRegstration").Tables[0].AsEnumerable().Select(r =>
            new Patient
            {
               // ID = r.Field<int>("ID"),
                PatientID = r.Field<string>("PatientID"),
                Name = r.Field<string>("name"),
                Address = r.Field<string>("Address"),
                AttendentName = r.Field<string>("AttendentName"),
                ConsultantName = r.Field<string>("ConsultantName"),
               // ConsultantFee = r.Field<int>("ConsultantFee"),
                Email = r.Field<string>("Email"),
                Sex = r.Field<string>("Sex"),
                ContactNumber1 = r.Field<string>("ContactNumber1"),
                ContactNumber2 = r.Field<string>("ContactNumber2"),
               // DepartmentID = r.Field<int>("DepartmentID"),
              //  IsFeeFree = r.Field<bool>("IsFeeFree"),
              //  RefDrID = r.Field<int>("RefDrID"),
              //  RegDate = r.Field<DateTime>("RegDate"),
                RegTime = r.Field<string>("RegTime"),
                Remarks = r.Field<string>("Remarks")
            }).ToList();

            foreach (Patient patient in PatientList)
            {
                Patients.Add(patient);
            }
        }
       
        public DataSet GetPatient(string TableName, string PatientID)
        {
            String condition = PatientID == "" ? "" : " PatientID='" + PatientID + "'";
            return du.GetTable(TableName, condition);
        }
        public bool AddPatient(Patient patient)
        {
            return du.AddRow(@"insert into PatientRegstration(  ID ,   PatientID ,   Name ,   AttendentName ,   Sex ,  
                                ContactNumber1 ,   ContactNumber2 ,  Email ,   Address ,   RefDrID ,   Type ,   IsFeeFree ,   ConsultantName ,   DepartmentID ,  
                                ConsultantFee ,   RegDate ,   RegTime ,   UserID ,   AddDate ,   ModifiyDate ,   IsDeleted ,   Fyear ,  
                                CompanyCode ,   Remarks ,   IsPaymentPaid ) 
            values(" + patient.ID + ",'" + patient.PatientID + "', '" + patient.Name + "', '" + patient.AttendentName + "', '" + patient.Sex
            + "', '" + patient.ContactNumber1 + "', '" + patient.ContactNumber2 + "', '" + patient.Email + "', '" + patient.Address + "', " + patient.RefDrID
            + ", '" + patient.Type + "', " + patient.IsFeeFree + ", '" + patient.ConsultantName + "', " + patient.DepartmentID
            + ", " + patient.ConsultantFee + ", '" + patient.RegDate + "', '" + patient.RegTime + "', " + patient.UserID + ", '" + patient.AddDate
            + "', '" + patient.ModifiyDate + "', " + patient.IsDeleted + ", " + patient.Fyear + ", '" + patient.CompanyCode + "', '" + patient.Remarks
            + "', " + patient.IsPaymentPaid + ")");
        }

        public bool UpdatePatient(Patient patient)
        {
            return du.AddRow(@"update PatientRegstration set Name= '" + patient.Name + "' where PatientID = '" +patient.PatientID  + "')");
        }

        public bool DeletePatient(Patient patient)
        {
            return du.DeleteRow(@"update PatientRegstration set DeleteFlag = 1 where PatientID ='" + patient.PatientID + "')");
        }

        public IEnumerable<Patient> GetAllPatient()
        {
            return Patients;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetPatient(int PatientID)
        {
            var patient = Patients.FirstOrDefault((p) => p.ID == PatientID);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
    }
}
