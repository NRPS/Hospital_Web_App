using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class CommanProperties
    {
        public int ID { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiyDate { get; set; }
        public int IsDeleted { get; set; }
        public int Fyear { get; set; }
        public string CompanyCode { get; set; }
        public int UserID { get; set; }

    }
}