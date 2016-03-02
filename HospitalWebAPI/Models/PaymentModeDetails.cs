using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PaymentModeDetails 
    {
        public string PaymentReceiptNo { get; set; } 
        public string ChequeCCNo { get; set; } 
        public string BankName { get; set; }  
        public string AccountHolderName{get;set;} 
    }
} 