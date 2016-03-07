using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalWebAPI.Models;
using CommanUtilities;
using System.Data;
using System.Web.Http;

namespace MvcApplication3.Controllers
{
    public class MedicineController : ApiController
    {
       
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<Medicine> medicines = new List<Medicine>();
        public bool AddMedicine(Medicine medicine)  
        {
            return du.AddRow(@"insert into Medicine(ID,MedicineName ,Code, GenericName,CompanyID)
            values(" + medicine.ID + ",'" + medicine.MedicineName + "', '" + medicine.Code + "', " + medicine.GenericName + ", '" + medicine.CompanyID + "')");
        }

        public bool UpdateMedicine(Medicine medicine) 
        {
            return du.AddRow(@"update Medicine set ID = " + medicine.ID + ")");
        }

        public bool DeleteMedicine(Medicine medicine)
        {
            return du.DeleteRow(@"update Medicine set DeleteFlag = 1 where ID ='" + medicine.ID + "')");
        }
        public IEnumerable<Medicine> GetAllMedicine()
        {
            return medicines;
        }

        public object GetMedicine(int ID) 
        {
            var medicine = medicines.FirstOrDefault((p) => p.ID == ID);
            if (medicine == null)
            {
                return "NotFound()";
            } 
            return medicine; 
        } 
    }
}
