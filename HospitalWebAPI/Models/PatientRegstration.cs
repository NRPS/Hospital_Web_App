﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class Patient : CommanProperties
    {
      
        public string PatientID { get; set; }
        public string Name { get; set; }
        public string AttendentName { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? RefByID { get; set; }
        public int? TypeID { get; set; }
        public bool? IsFeeFree { get; set; }
        public string ConsultantName { get; set; }
        public int? DepartmentID { get; set; }
        public decimal? ConsultantFee { get; set; }
        public DateTime? RegDate { get; set; }
        public string RegTime { get; set; }
        public string Remarks { get; set; }
        public bool? IsPaymentPaid { get; set; }
    }
}