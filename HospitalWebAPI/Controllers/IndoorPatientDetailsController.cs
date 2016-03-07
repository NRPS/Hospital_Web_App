using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalWebAPI.Models;
using CommanUtilities;
using System.Data;
using System.Web.Http;

namespace MvcApplication3.Controllers
{
    public class IndoorPatientDetailsController : ApiController
    {
       
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<IndoorPatientDetails> indoorPatientDetails = new List<IndoorPatientDetails>();
        public bool AddIndoorPatientDetails(IndoorPatientDetails indoorPatientDetail)  
        {
            return du.AddRow(@"insert into IndoorPatientDetails(IPDID,SRNO ,Roomnumber, Bedno,UserID ,FromDate ,ToDate, InTime,OutTime )
            values(" + indoorPatientDetail.IPDID + ",'" + indoorPatientDetail.SRNO + "', '" + indoorPatientDetail.Roomnumber + "', " + indoorPatientDetail.Bedno + ", '" + indoorPatientDetail.UserID + "', '" + indoorPatientDetail.FromDate + "', " + indoorPatientDetail.ToDate + ",  '" + indoorPatientDetail.InTime + "', '" + indoorPatientDetail.OutTime
            + "')");
        }

        public bool UpdateIndoorPatientDetails(IndoorPatientDetails indoorPatientDetails)
        {
            return du.AddRow(@"update IndoorPatientDetails set Roomnumber = " + indoorPatientDetails.Roomnumber + ",Bedno = " + indoorPatientDetails.Bedno + ",UserID = " + indoorPatientDetails.UserID + ",FromDate = " + indoorPatientDetails.FromDate + ",ToDate = " + indoorPatientDetails.ToDate + ",InTime = " + indoorPatientDetails.InTime + ",OutTime = " + indoorPatientDetails.OutTime + ")");
        }

        public bool DeleteIndoorPatientDetails(IndoorPatientDetails indoorPatientDetails) 
        {
            return du.DeleteRow(@"update IndoorPatientDetails set DeleteFlag = 1 where SRNO ='" + indoorPatientDetails.SRNO + "')");
        }
        public IEnumerable<IndoorPatientDetails> GetAllPatientBillDetails()
        {
            return indoorPatientDetails;
        }

        public object GetIndoorPatientDetails(int SRNO)
        {
            var PatientBill = indoorPatientDetails.FirstOrDefault((p) => p.SRNO == SRNO);
            if (PatientBill == null)
            {
                return "NotFound()";
            }
            return PatientBill;
        }

    }
}
