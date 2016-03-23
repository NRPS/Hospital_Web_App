using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PatientBalanceAmount
    {
        public string PatientID { get; set; }

        public Decimal TotalBillAmount { get; set; }

        public Decimal TotalPayAmount { get; set; }

        public Decimal Balance { get; set; }
    }
}