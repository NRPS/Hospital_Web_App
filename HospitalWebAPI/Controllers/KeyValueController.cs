using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommanUtilities;
using HospitalWebAPI.Models;
using System.Web.Http.Cors;
using System.Data;
using Newtonsoft.Json;

namespace HospitalWebAPI.Controllers
{
    public class NextKeyValueController : ApiController
    {
        MSAccessDataUtility du = new MSAccessDataUtility();

        NextKeyValueController()
        {

        }

        public IHttpActionResult Get(string TableName)
        {
            var nextKeyValue =  FindNextKeyValue( TableName);
            if(nextKeyValue == null)
            {
                return NotFound();
            }
            return Ok(nextKeyValue);
        }

        private NextKeyValue FindNextKeyValue(string TableName)
        {
            Basic basic = new Basic();
            NextKeyValue nextKeyValue = new NextKeyValue();

            nextKeyValue = null;

            switch (TableName)
            {
                case "REGISTRATION":

                    nextKeyValue.ID = basic.GetMax("PatientRegstration", "ID") + 1;
                    nextKeyValue.KeyValue = basic.GetKey(nextKeyValue.ID, 'P',  true, true);
                    break;
                case "BILL":
                    nextKeyValue.ID = basic.GetMax("PatientBill", "ID") + 1;
                    nextKeyValue.KeyValue = basic.GetKey(nextKeyValue.ID, 'B', true, true);
                    break;
                case "PAYMENT":
                    nextKeyValue.ID = basic.GetMax("Payment", "ID") + 1;
                    nextKeyValue.KeyValue = basic.GetKey(nextKeyValue.ID, 'C', true, true);
                    break;
            }

            return nextKeyValue;
        }
    }
}
