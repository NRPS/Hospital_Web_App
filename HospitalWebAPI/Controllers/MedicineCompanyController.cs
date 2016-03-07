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
    public class MedicineCompanyController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<MedicineCompany> medicineCompanys = new List<MedicineCompany>();
        public bool AddMedicineCompany(MedicineCompany medicineCompany) 
        {
            return du.AddRow(@"insert into MedicineCompany(CompanyID,CompanyCode ,CompanyName, Address1,Address2,Address3,ContactNumber1,ContactNumber2,ContactPerson,Type,UserID,AddDate,ModifiyDate)
            values(" + medicineCompany.CompanyID + ",'" + medicineCompany.CompanyCode + "', '" + medicineCompany.CompanyName + "', " + medicineCompany.Address1 + ", '" + medicineCompany.Address2 + "', '" + medicineCompany.Address3 + "', '" + medicineCompany.ContactNumber1 + "', '" + medicineCompany.ContactNumber2 + "', '" + medicineCompany.ContactPerson + "', '" + medicineCompany.Type + "', '" + medicineCompany.UserID + "', '" + medicineCompany.AddDate + "', '" + medicineCompany.ModifiyDate + "')");
        }

        public bool UpdateMedicineCompany(MedicineCompany medicineCompany) 
        {
            return du.AddRow(@"update MedicineCompany set CompanyID = " + medicineCompany.CompanyID + ")");
        }

        public bool DeleteMedicineCompany(MedicineCompany medicineCompany)
        {
            return du.DeleteRow(@"update MedicineCompany set DeleteFlag = 1 where CompanyID ='" + medicineCompany.CompanyID + "')");
        }
        public IEnumerable<MedicineCompany> GetAllMedicine()
        {
            return medicineCompanys;
        }
         
        public object GetMedicinCompanye(int CompanyID)
        {
            var medicineCompany = medicineCompanys.FirstOrDefault((p) => p.CompanyID == CompanyID);
            if (medicineCompany == null)
            {
                return "NotFound()";
            }
            return medicineCompany;
        } 
    }
}
