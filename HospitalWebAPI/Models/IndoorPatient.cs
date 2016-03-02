using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class IndoorPatient
    {
        public int IPDID { get; set; }
        public string PatientID { get; set; }
        public DateTime CheckINDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int UserID { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiyDate { get; set; }
        public int IsDeleted { get; set; }
        public bool IsDischarged { get; set; }
    }
}