using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class Charges  
    { 
        public int ID { get; set; } 
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    } 
}