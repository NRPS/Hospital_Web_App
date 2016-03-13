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
    public class DepartmentController : ApiController

    {
        MSAccessDataUtility du = new MSAccessDataUtility();
        List<Department> DepartmentList = new List<Department>();
        string TableName = "Department";

        DepartmentController()
        {
            GetDepartmentList();
        }

        #region  CURD


        // GET: api/Department
        public IEnumerable<Department> Get()
        {
            return DepartmentList;
        }

        // GET: api/Department/5
        public IHttpActionResult Get(int id)
        {
            var Department = DepartmentList.Where(x => x.ID == id).FirstOrDefault();

            if (Department == null)
                return NotFound();

            return Ok(Department);
        }

        // POST: api/Department
        public IHttpActionResult Post([FromBody]Department Department)
        {
            if (AddRefBy(Department) == true)
                return Ok();
            else
                return BadRequest();
        }

        // PUT: api/Department/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Department/5
        public void Delete(int id)
        {

        }

        #endregion

        #region Priavte

        private void GetDepartmentList()
        {


            DepartmentList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new Department
            {
                Name = r.Field<string>("Name"),
                Remarks = r.Field<string>("Remarks"),
                ID = r.Field<Int16>("ID")
            }).ToList();
        }

        private bool AddRefBy(Department Department)
        {
            return du.AddRow(@"insert into Department(  ID ,    Type ,   Remarks)
            values(" + Department.ID + ", '" + Department.Name + "', '" + Department.Remarks + "')");
        }

        #endregion
    }

}
