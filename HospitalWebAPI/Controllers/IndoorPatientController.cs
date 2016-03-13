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
    public class IndoorPatientController : ApiController
    {
       
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<IndoorPatient> indoorPatients = new List<IndoorPatient>();
        public bool AddIndoorPatient(IndoorPatient indoorPatient)  
        {
            return du.AddRow(@"insert into IndoorPatient(IPDID,PatientID ,CheckINDate, CheckOutDate,UserID ,AddDate ,ModifiyDate, IsDeleted,IsDischarged )
            values(" + indoorPatient.IPDID + ",'" + indoorPatient.PatientID + "', '" + indoorPatient.CheckINDate + "', " + indoorPatient.CheckOutDate + ", '" + indoorPatient.UserID + "', '" + indoorPatient.AddDate + "', " + indoorPatient.ModifiyDate + ",  '" + indoorPatient.IsDeleted + "', '" + indoorPatient.IsDischarged
            + "')");
        }

        public bool UpdateIndoorPatient(IndoorPatient indoorPatients)  
        {
            return du.AddRow(@"update IndoorPatient set CheckINDate = " + indoorPatients.CheckINDate + ",CheckOutDate = " + indoorPatients.CheckOutDate + ",UserID = " + indoorPatients.UserID + ",ModifiyDate = " + indoorPatients.ModifiyDate + ",IsDischarged = " + indoorPatients.IsDischarged + ")");
        }

        public bool DeleteIndoorPatient(IndoorPatient indoorPatients) 
        {
            return du.DeleteRow(@"update IndoorPatient set DeleteFlag = 1 where IPDID ='" + indoorPatients.IPDID + "')");
        }
        public IEnumerable<IndoorPatient> GetAllPatientBillDetails()
        {
            return indoorPatients;
        }
         
        public object GetIndoorPatient(int IPDID)
        {
            var PatientBill = indoorPatients.FirstOrDefault((p) => p.IPDID == IPDID);
            if (PatientBill == null)
            {
                return "NotFound()";
            }
            return PatientBill;
        }


    }
}
