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
    public class BalanceAmountController : ApiController
    {

        MSAccessDataUtility du = new MSAccessDataUtility();
        List<PatientBalanceAmount> PatientBalanceAmountList = new List<PatientBalanceAmount>();

        BalanceAmountController()
        {
        }

        #region  CURD

        
        // GET: api/Department/5
        public IHttpActionResult Get(string id)
        {
            GetDepartmentList(id);
            var PatientBalanceAmounts = PatientBalanceAmountList.Where(x => x.PatientID == id).FirstOrDefault();

            if (PatientBalanceAmounts == null)
                return NotFound();

            return Ok(PatientBalanceAmounts);
        }

        #endregion CURD


        #region Priavte

        private void GetDepartmentList(string PatientID)
        {
            PatientBalanceAmount BA = new PatientBalanceAmount();

            BA.PatientID = PatientID;
            BA.TotalBillAmount = du.GetScalarValueDecimal("Select sum(BillTotal) from PatientBill  where   patientid='" + PatientID + "' and IsDeleted=0 ");
            BA.TotalPayAmount = du.GetScalarValueDecimal("Select sum(Amount) from Payment where (registrationno is null or registrationno='') and patientid='" + PatientID + "' and IsDeleted=0 ");
            BA.Balance = BA.TotalBillAmount - BA.TotalPayAmount;
            PatientBalanceAmountList.Add(BA);
        }

     
        #endregion

    }
}
