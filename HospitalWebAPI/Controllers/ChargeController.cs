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
            if (AddCharge(charges) == true)
                return Ok();
            else
                return BadRequest();
        }

        #endregion

        #region Priavte

        private void GetChargesList()
        {
            chargesList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new Charges
            {   ID = r.Field<Int16>("ID"),
                Description = r.Field<string>("Description"),
                Code = r.Field<string>("Code"),
                Rate = r.Field<decimal>("Rate")
            }).ToList();
        }

        private bool AddCharge(Charges charges)
        {
            Basic basic = new Basic();

            charges.ID = basic.GetMax("Charges", "ID")  + 1;
            charges.Code = charges.ID.ToString();

            return du.AddRow(@"insert into Charges( ID  , Code ,    Description ,   Rate)
            values(" + charges.ID + ", '" + charges.Code + "', '" + charges.Description + "'," + charges.Rate+ ")");
        }

        #endregion
    }
}
