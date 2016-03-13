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
    public class PatientBillDetailsController : ApiController
    {
       
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<PatientBillDetails> patientBillDetails = new List<PatientBillDetails>(); 
        public bool AddPatientBill(PatientBillDetails patientBillDetails) 
        {
            return du.AddRow(@"insert into PatientBillDetails(BillNo,PatientBillDetailID ,FromDate, ToDate ,Rate ,Quantity,Amount, Discount,NetAmount)
            values(" + patientBillDetails.BillNo + ",'" + patientBillDetails.PatientBillDetailID + "', '" + patientBillDetails.FromDate + "', " + patientBillDetails.ToDate + ", '" + patientBillDetails.Rate + "', '" + patientBillDetails.Quantity + "', " + patientBillDetails.Amount + ",  '" + patientBillDetails.Discount + "', '" + patientBillDetails.NetAmount
            + "')");
        }

        public bool UpdatePatientBillDetails(PatientBillDetails patientBillDetails) 
        {
            return du.AddRow(@"update PatientBillDetails set FromDate = " + patientBillDetails.FromDate + ",ToDate = " + patientBillDetails.ToDate + ",Quantity = " + patientBillDetails.Quantity + ",Amount = " + patientBillDetails.Amount + ",Discount = " + patientBillDetails.Discount + ",NetAmount = " + patientBillDetails.NetAmount + ")");
        }

        public bool DeletePatientBillDetails(PatientBillDetails patientBillDetails) 
        {
            return du.DeleteRow(@"update PatientBillDetails set DeleteFlag = 1 where PatientBillDetailID ='" + patientBillDetails.PatientBillDetailID + "')");
        }
        public IEnumerable<PatientBillDetails> GetAllPatientBillDetails()
        {
            return patientBillDetails;
        }

        public object GetPatientBillDetails(int patientBillDetailID) 
        {
            var PatientBill = patientBillDetails.FirstOrDefault((p) => p.PatientBillDetailID == patientBillDetailID);
            if (PatientBill == null)
            {
                return "NotFound()";
            }
            return PatientBill;
        }

    }
}
