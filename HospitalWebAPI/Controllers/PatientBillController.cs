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
    public class PatientBillController : ApiController
    {
       //
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<PatientBill> patientBills = new List<PatientBill>();  
        public bool AddPatientBill(PatientBill patientBill)  
        {
            return du.AddRow(@"insert into PatientBill(BillNo,PatientID ,BillDate, UserID ,AddDate ,ModifiyDate,IsDeleted, Remarks)
            values(" + patientBill.BillNo + ",'" + patientBill.PatientID + "', '" + patientBill.BillDate + "', " + patientBill.UserID + ", '" + patientBill.AddDate
            + "', '" + patientBill.ModifiyDate + "', " + patientBill.IsDeleted + ",  '" + patientBill.Remarks
            + "')");
        }

        public bool UpdatePatientBill(PatientBill patientBill)
        {
            return du.AddRow(@"update PatientBill set PatientID = " + patientBill.PatientID + ",BillDate = " + patientBill.BillDate + ",UserID = " + patientBill.UserID + ",ModifiyDate = " + patientBill.ModifiyDate + ",Remarks = " + patientBill.Remarks + ")");
        }

        public bool DeletePatientBill(PatientBill patientBill)
        {
            return du.DeleteRow(@"update PatientBill set DeleteFlag = 1 where BillNo ='" + patientBill.BillNo + "')");
        }
        public IEnumerable<PatientBill> GetAllPatientBill()
        {
            return patientBills;
        }

        public object GetPatientBill(string BillNo) 
        {
            var PatientBill = patientBills.FirstOrDefault((p) => p.BillNo == BillNo);
            if (PatientBill == null)
            {
                return "NotFound()";
            }
            return PatientBill;
        }


    }
}
