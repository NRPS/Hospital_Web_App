using CommanUtilities;
using HospitalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using Newtonsoft.Json;

namespace HospitalWebAPI.Controllers
{
    public class ReferedByController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<ReferedBy> referedByList = new List<ReferedBy>();
        string TableName = "ReferedBy";

        ReferedByController()
        {
            GetReferedByList();
        }

        #region  CURD


        // GET: api/ReferedBy
        public IEnumerable<ReferedBy> Get()
        {
            return referedByList;
        }

        // GET: api/ReferedBy/5
        public IHttpActionResult Get(int id)
        {
            var referedBy = referedByList.Where(x => x.RefID == id).FirstOrDefault();

            if (referedBy == null)
                return NotFound();

            return Ok(referedBy);
        }

        // POST: api/ReferedBy
        public IHttpActionResult Post([FromBody]ReferedBy referedBy)
        {
            if (AddRefBy(referedBy) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/ReferedBy/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/ReferedBy/5
        public void Delete(int id)
        {

        }

        #endregion

        #region Priavte

        private void GetReferedByList()
        {


            referedByList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new ReferedBy
            {
                Name = r.Field<string>("name"),
                ContactNumber = r.Field<string>("ContactNumber"),
                RefID = r.Field<Int16>("RefID")
            }).ToList();
        }

        private bool AddRefBy(ReferedBy referedBy)
        {
            return du.AddRow(@"insert into ReferedBy(  RefID ,    Name ,   ContactNumber)
            values(" + referedBy.RefID + ", '" + referedBy.Name + "', '" + referedBy.ContactNumber + "')");
        }

        #endregion
    }
}
