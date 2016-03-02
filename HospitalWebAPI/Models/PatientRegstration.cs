using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public string PatientID { get; set; }
        public string Name { get; set; }
        public string AttendentName { get; set; }
        public string Sex { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int RefDrID { get; set; }
        public string Type { get; set; }
        public bool IsFeeFree { get; set; }
        public string ConsultantName { get; set; }
        public int DepartmentID { get; set; }
        public decimal ConsultantFee { get; set; }
        public DateTime RegDate { get; set; }
        public string RegTime { get; set; }
        public int UserID { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiyDate { get; set; }
        public int IsDeleted { get; set; }
        public int Fyear { get; set; }
        public string CompanyCode { get; set; }
        public string Remarks { get; set; }
        public bool IsPaymentPaid { get; set; }
    }
}