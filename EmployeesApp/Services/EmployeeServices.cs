using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EmployeesApp.Models;

namespace EmployeesApp.Services
{
    public class EmployeeServices
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeServices(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Наём сотрудника
        public int Hire(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(
                    nameof(employee));
            }

            if (string.IsNullOrWhiteSpace(employee.FullName))
            {
                throw new ArgumentException(
                    "ФИО сотрудника не может быть пустым.");
            }

            if (string.IsNullOrWhiteSpace(employee.Position))
            {
                throw new ArgumentException(
                    "Должность не может быть пустой.");
            }

            if (employee.Salary < 0)
            {
                throw new ArgumentException(
                    "Зарплата не может быть отрицательной.");
            }

            return _repository.Add(employee);
        }

        // Увольнение сотрудника
        public void Fire(int id)
        {
            bool deleted = _repository.Delete(id);

            if (!deleted)
            {
                throw new InvalidOperationException(
                    "Сотрудник не найден.");
            }
        }
    }
}
