using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PatientBill :CommanProperties
    {
        public string BillNo { get; set; }
        public string PatientID { get; set; }
        public DateTime BillDate { get; set; }       
        public string Remarks { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BillTotal { get; set; }
        public string PaymentMode { get; set; }

        public List<PatientBillDetails> BillDetails { get; set; }
    }
} 