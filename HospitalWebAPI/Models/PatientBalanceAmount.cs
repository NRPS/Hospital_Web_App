using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PatientBalanceAmount
    {
        public string PatientID { get; set; }

        public decimal? TotalBillAmount { get; set; }

        public decimal? TotalPayAmount { get; set; }

        public decimal? Balance { get; set; }
    }
}