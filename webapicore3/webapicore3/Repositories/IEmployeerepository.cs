using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapicore3.Repositories
{
    public interface IEmployeerepository
    {
        object GetEmployeeList();

        object GetEmployeeDetails(int empId);
        List<User> Getallinf();
        List<User> GetEmployeeBy(string name, string pwd);

        bool RegisterUsersby(string name, string pwd);
    }
}
