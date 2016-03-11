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
        List<PatientType> PatientTypeList = new List<PatientType>();
        string TableName = "PatientType";

        PatientTypeController()
        {
            GetPatientTypeList();
        }

        #region  CURD


        // GET: api/PatientType
        public IEnumerable<PatientType> Get()
        {
            return PatientTypeList;
        }

        // GET: api/PatientType/5
        public IHttpActionResult Get(int id)
        {
            var PatientType = PatientTypeList.Where(x => x.ID == id).FirstOrDefault();

            if (PatientType == null)
                return NotFound();

            return Ok(PatientType);
        }

        // POST: api/PatientType
        public IHttpActionResult Post([FromBody]PatientType PatientType)
        {
            if (AddRefBy(PatientType) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/PatientType/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/PatientType/5
        public void Delete(int id)
        {

        }

        #endregion

        #region Priavte

        private void GetPatientTypeList()
        {


            PatientTypeList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new PatientType
            {
                Type = r.Field<string>("Type"),
                Remarks = r.Field<string>("Remark"),
                ID = r.Field<Int16>("ID")
            }).ToList();
        }

        private bool AddRefBy(PatientType PatientType)
        {
            return du.AddRow(@"insert into PatientType(  ID ,    Type ,   Remark)
            values(" + PatientType.ID + ", '" + PatientType.Type + "', '" + PatientType.Remarks + "')");
        }

        #endregion
    }
}
