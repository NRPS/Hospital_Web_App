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
            var referedBy = referedByList.Where(x => x.ID == id).FirstOrDefault();

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

        #endregion

        #region Priavte

        private void GetReferedByList()
        {


            referedByList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new ReferedBy
            {
                ID = r.Field<Int16>("ID"),
                Name = r.Field<string>("name"),
                ContactNumber = r.Field<string>("ContactNumber")
            }).ToList();
        }

        private bool AddRefBy(ReferedBy referedBy)
        {
            Basic basic = new Basic();

            referedBy.ID = basic.GetMax("ReferedBy", "ID") + 1;

            return du.AddRow(@"insert into ReferedBy(  ID ,    Name ,   ContactNumber)
            values(" + referedBy.ID + ", '" + referedBy.Name + "', '" + referedBy.ContactNumber + "')");
        }

        #endregion
    }
}
