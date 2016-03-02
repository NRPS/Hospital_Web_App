using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class Charges  
    { 
        public int ChargeID { get; set; } 
        public string Code { get; set; }
        public string ChargeDescription { get; set; }
        public decimal Rate { get; set; }
    } 
}