using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PatientBillDetails
    { 
        public string BillNo { get; set; }
        public int PatientBillDetailID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }

    }
}