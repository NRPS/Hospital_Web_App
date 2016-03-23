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
    public class ChargeController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<Charges> chargesList = new List<Charges>();
        string TableName = "Charges";

        ChargeController()
        {
            GetChargesList();
        }

        #region  CURD


        // GET: api/Charges
        public IEnumerable<Charges> Get()
        {
            return chargesList;
        }

        // GET: api/Charges/5
        public IHttpActionResult Get(string id)
        {
            var Charges = chargesList.Where(x => x.Code == id).FirstOrDefault();

            if (Charges == null)
                return NotFound();

            return Ok(Charges);
        }

        // POST: api/Charges
        public IHttpActionResult Post([FromBody]Charges charges)
        {
            if (AddRefBy(charges) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/Charges/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Charges/5
        public void Delete(int id)
        {

        }

        #endregion

        #region Priavte

        private void GetChargesList()
        {
            chargesList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new Charges
            {    ChargeID = r.Field<Int16>("ChargeID"),
                ChargeDescription = r.Field<string>("Description"),
                Code = r.Field<string>("Code"),
                Rate = r.Field<decimal>("Rate")
            }).ToList();
        }

        private bool AddRefBy(Charges charges)
        {
            return du.AddRow(@"insert into Charges( ChargeID  , Code ,    Description ,   Rate)
            values(" + charges.ChargeID + ", '" + charges.Code + "', '" + charges.ChargeDescription + "'," + charges.ChargeDescription+ ")");
        }

        #endregion
    }
}
