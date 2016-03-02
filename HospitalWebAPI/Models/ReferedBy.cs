using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class ReferedBy 
    { 
        public int RefID { get; set; } 
        public string Name { get; set; }
        public string ContactNumber { get; set; } 
    }
}