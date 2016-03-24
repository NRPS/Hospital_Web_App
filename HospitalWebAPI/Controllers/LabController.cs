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
    public class LabController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<Labs> labsList = new List<Labs>();
        string TableName = "Labs";

        LabController()
        {
            GetLabsList();
        }

        #region  CURD


        // GET: api/Labs
        public IEnumerable<Labs> Get()
        {
            return labsList;
        }

        // GET: api/Labs/5
        public IHttpActionResult Get(string id)
        {
            var labs = labsList.Where(x => x.Code == id).FirstOrDefault();

            if (labs == null)
                return NotFound();

            return Ok(labs);
        }

        // POST: api/Labs
        public IHttpActionResult Post([FromBody]Labs labs)
        {
            if (AddLab(labs) == true)
                return Ok();
            else
                return BadRequest();
        }

        #endregion

        #region Priavte

        private void GetLabsList()
        {
            labsList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new Labs
            {   ID = r.Field<Int16>("ID"),
                Name = r.Field<string>("Name"),
                Code = r.Field<string>("Code"),
                Address = r.Field<string>("Address")
            }).ToList();
        }

        private bool AddLab(Labs labs)
        {
            Basic basic = new Basic();

            labs.ID = basic.GetMax("Labs", "ID")  + 1;
            labs.Code = basic.GetKey(labs.ID, 'L', 4);

            return du.AddRow(@"insert into Labs( ID  , Code ,    Name ,   Address)
            values(" + labs.ID + ", '" + labs.Code + "', '" + labs.Name + "','" + labs.Address+ "')");
        }

        #endregion
    }
}
