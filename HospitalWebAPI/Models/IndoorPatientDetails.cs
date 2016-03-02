using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class IndoorPatientDetails 
    {
        public int IPDID { get; set; }
        public int SRNO { get; set; }
        public string Roomnumber { get; set; }
        public int Bedno { get; set; } 
        public int UserID { get; set; }
        public DateTime FromDate { get; set; } 
        public DateTime ToDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
    }
    








}