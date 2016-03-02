using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class MedicineCompany
    {
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string ContactPerson { get; set; }
        public string Type { get; set; }
        public int UserID { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiyDate { get; set; }
    }
}