using webapicore3.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using webapicore3.Repositories;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace webapicore3.Repositories
{
    public class EmployeeRepository : IEmployeerepository
    {
        IConfiguration configuration;
        public EmployeeRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public object GetEmployeeDetails(int empId)
        {
            object result = null;
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("EMP_ID", OracleDbType.Int32, ParameterDirection.Input, empId);
                dyParam.Add("EMP_DETAIL_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "USP_GETEMPLOYEEDETAILS";

                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
         
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public object GetEmployeeList()
        {
            object result = null;
            try
            {
                var dyParam = new OracleDynamicParameters();

                dyParam.Add("EMPCURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "USP_GETEMPLOYEES";
                    

                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<User> GetEmployeeBy(string name,string pwd)
        {
            List<User> result = null;
            try
            {
                
                string str = "select * from Users WHERE name= '" + name + "'AND pwd = '" + pwd+"'";
                
                var conn = this.GetConnection();
                
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    result = SqlMapper.Query<User>(conn, str).ToList();
               
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            return result;
        }

        public List<User> Getallinf()
        {
            List<User> result = null;
            try
            {

                string str = "select * from Users";

                var conn = this.GetConnection();


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    result = conn.Query<User>(str).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool RegisterUsersby(string name,string pwd)
        {
            List<User> result = null;
            try
            {

                string str = "select * from Users WHERE name= '" + name + "'";

                var conn = this.GetConnection();


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    result = conn.Query<User>(str).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            if(result.Count != 0)
            {
                return false;
            }

            try
            {

                 string str = "insert into Users values('" + name + "','" + pwd + "')";

                 var conn = this.GetConnection();


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    
                    conn.Execute(str);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }

        public IDbConnection GetConnection()
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }

    }
}

/*public IDbConnection GetConnection()
{
    var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
    var conn = new OracleConnection(connectionString);
    return conn;
}*/