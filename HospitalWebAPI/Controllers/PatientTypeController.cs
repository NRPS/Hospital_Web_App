using CommanUtilities;
using HospitalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HospitalWebAPI.Controllers
{
    public class PatientTypeController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<PatientType> PatientTypes = new List<PatientType>();
        string TableName = "PatientType";


        #region  CURD


        // GET: api/PatientType
        public IEnumerable<PatientType> Get()
        {
            GetPatientTypeList(GetPatientType(TableName, 0));
            return PatientTypes;
        }

        // GET: api/PatientType/5
        public IHttpActionResult Get(int id)
        {
            GetPatientTypeList(GetPatientType(TableName, id));

            var PatientType = PatientTypes.Where(x => x.ID == id).FirstOrDefault();

            if (PatientType == null)
                return NotFound();

            return Ok(PatientType);
        }

        // POST: api/PatientType
        public IHttpActionResult Post([FromBody]PatientType PatientType)
        {
            if (AddPatientType(PatientType) == true)
                return Ok();
            else
                return BadRequest();
        }


        #endregion

        #region Priavte

        private void GetPatientTypeList(DataSet PatientsDS)
        {
            try
            {
                PatientTypes = PatientsDS.Tables[0].AsEnumerable().Select(r =>
                new PatientType
                {
                    Type = r.Field<string>("Type"),
                    Remarks = r.Field<string>("Remark"),
                    ID = r.Field<Int16>("ID")
                }).ToList();
            }
            catch (Exception Ex)
            {

            }
        }


        private DataSet GetPatientType(string TableName, int id)
        {
            try
            {
                String condition = id <= 0 ? "" : " ID = " + id;
                return du.GetTable(TableName, condition);
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        private bool AddPatientType(PatientType patientType)
        {
            try
            {
                Basic basic = new Basic();

                patientType.ID = basic.GetMax("PatientType", "ID") + 1;

                return du.AddRow(@"insert into PatientType(  ID ,    Type ,   Remark)
                values(" + patientType.ID + ", '" + patientType.Type + "', '" + patientType.Remarks + "')");
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        #endregion
    }
}
