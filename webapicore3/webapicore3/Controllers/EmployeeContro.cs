using webapicore3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Collections.Generic;

namespace webapicore3.Controllers
{
    [Route("api/User")]
    [ApiController]
    
    public class EmployeeController : Controller
    {
        IEmployeerepository employeeRepository;
        public EmployeeController(IEmployeerepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }
        [HttpGet]
        //url:https://localhost:5001/api/Employee
        public ActionResult GetEmployeeList()
        {
            List<User> result = employeeRepository.Getallinf();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{WantName}/{WantPwd}")]
        public ActionResult GetEmployeeDetails(string WantName, string WantPwd)
        {

            //DataTable dt = OracleHelper.ExcuteDataTable(constr);
            List<User> result = employeeRepository.GetEmployeeBy(WantName, WantPwd);
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public bool GetEmployeePost(object json)
        {
            JObject user = JObject.Parse(json.ToString());

            //DataTable dt = OracleHelper.ExcuteDataTable(constr);
            var result = employeeRepository.GetEmployeeBy(user["name"].ToString(),user["pwd"].ToString());
            if (result == null)
            {
                return false;
            }
            return true;
        }

        [HttpPost("register")]
        public bool Register(object json)
        {
            JObject user = JObject.Parse(json.ToString());
            
            return employeeRepository.RegisterUsersby(user["name"].ToString(), user["pwd"].ToString());
        }

    }
}