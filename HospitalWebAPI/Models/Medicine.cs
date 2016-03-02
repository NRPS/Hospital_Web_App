using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class Medicine
    { 
        public int ID { get; set; }
        public string MedicineName { get; set; }
        public string Code { get; set; }
        public string GenericName { get; set; }
        public int CompanyID { get; set; } 
    }
}