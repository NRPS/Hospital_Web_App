using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PatientBillDetails
    { 
       public Int32 BillID { get; set; }
        public string BillNo { get; set; }
        public string LabCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Rate { get; set; }
        public Int32 Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string Remarks { get; set; }


    }
}