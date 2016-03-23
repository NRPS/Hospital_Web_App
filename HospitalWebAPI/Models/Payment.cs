using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class Payment : CommanProperties
    {
        public string PaymentReceiptNo { get; set; } 
        public string PatientID { get; set; }
        public DateTime PaymentDate { get; set; }  
        public decimal Amount{get;set;}
        public string PaymentMode{get;set;}
        public string BillNo{get;set;}
        public string RegistratonNo{get;set;}
        public string Remarks { get; set; }
    }
} 