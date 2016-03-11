using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class PatientType
    {

        public int ID { get; set; }
        public string Type { get; set; }
        public string Remarks { get; set; }
    }
}