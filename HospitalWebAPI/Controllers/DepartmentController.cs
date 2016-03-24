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
        List<Department> departmentList = new List<Department>();
        string TableName = "Department";

        DepartmentController()
        {
            GetDepartmentList();
        }

        #region  CURD


        // GET: api/Department
        public IEnumerable<Department> Get()
        {
            return departmentList;
        }

        // GET: api/Department/5
        public IHttpActionResult Get(int id)
        {
            var Department = departmentList.Where(x => x.ID == id).FirstOrDefault();

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


            departmentList = du.GetTable(TableName).Tables[0].AsEnumerable().Select(r =>
            new Department
            {
                Name = r.Field<string>("Name"),
                Remarks = r.Field<string>("Remarks"),
                ID = r.Field<Int16>("ID")
            }).ToList();
        }

        private bool AddRefBy(Department department)
        {
            Basic basic = new Basic();

            department.ID = basic.GetMax("Department", "ID") + 1;

            return du.AddRow(@"insert into Department(  ID ,    Name ,   Remarks)
            values(" + department.ID + ", '" + department.Name + "', '" + department.Remarks + "')");
        }

        #endregion
    }

}
