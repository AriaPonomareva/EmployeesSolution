using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesApp.Models;

namespace EmployeesApp.Services
{
    public interface IEmployeeRepository
    {
        int Add(Employee employee);

        Employee GetById(int id);

        bool Delete(int id);

        int Count();
    }
}
