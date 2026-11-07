using EmployeesApp.Models;
using EmployeesApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EmployeesApp.Tests
{
    [TestClass]
    public class IEmployeeRepositoryTests
    {
        private FakeEmployeeRepository _repository;

        [TestInitialize]
        public void Init()
        {
            _repository = new FakeEmployeeRepository();
        }

        // Создание тестового сотрудника
        private Employee NewEmployee()
        {
            return new Employee
            {
                FullName = "Иванов Иван Иванович",
                Position = "Программист",
                Salary = 50000m,
                HireDate = new DateTime(2024, 5, 10)
            };
        }

        // 1. Проверка добавления сотрудника
        [TestMethod]
        public void Add_ValidEmployee_ReturnsPositiveId()
        {
            // Arrange
            Employee employee = NewEmployee();

            // Act
            int id = _repository.Add(employee);

            // Assert
            Assert.IsTrue(id > 0);
            Assert.AreEqual(1, _repository.Count());
        }

        // 2. Проверка получения сотрудника по ID
        [TestMethod]
        public void GetById_ExistingEmployee_ReturnsEmployee()
        {
            // Arrange
            Employee employee = NewEmployee();
            int id = _repository.Add(employee);

            // Act
            Employee result = _repository.GetById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employee.FullName, result.FullName);
            Assert.AreEqual(employee.Position, result.Position);
            Assert.AreEqual(employee.Salary, result.Salary);
            Assert.AreEqual(employee.HireDate, result.HireDate);
        }

        // 3. Проверка получения несуществующего сотрудника
        [TestMethod]
        public void GetById_UnknownId_ReturnsNull()
        {
            // Arrange
            int unknownId = -1;

            // Act
            Employee result = _repository.GetById(unknownId);

            // Assert
            Assert.IsNull(result);
        }

        // 4. Проверка удаления сотрудника
        [TestMethod]
        public void Delete_ExistingEmployee_ReturnsTrue()
        {
            // Arrange
            int id = _repository.Add(NewEmployee());

            // Act
            bool result = _repository.Delete(id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_repository.GetById(id));
            Assert.AreEqual(0, _repository.Count());
        }

        // 5. Проверка подсчёта сотрудников
        [TestMethod]
        public void Count_AfterAddingTwoEmployees_ReturnsTwo()
        {
            // Arrange
            _repository.Add(NewEmployee());

            Employee secondEmployee = NewEmployee();
            secondEmployee.FullName = "Петров Пётр Петрович";

            _repository.Add(secondEmployee);

            // Act
            int result = _repository.Count();

            // Assert
            Assert.AreEqual(2, result);
        }
    }

    // Заглушка репозитория без подключения к базе данных
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees =
            new List<Employee>();

        private int _nextId = 1;

        public int Add(Employee employee)
        {
            employee.Id = _nextId++;

            _employees.Add(employee);

            return employee.Id;
        }

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(
                employee => employee.Id == id);
        }

        public bool Delete(int id)
        {
            Employee employee = GetById(id);

            if (employee == null)
            {
                return false;
            }

            _employees.Remove(employee);

            return true;
        }

        public int Count()
        {
            return _employees.Count;
        }
    }
}
//        private FakeEmployeeRepository _repository;

//        [TestInitialize]
//        public void Init()
//        {
//            _repository = new FakeEmployeeRepository();
//        }

//        private Employee NewEmployee()
//        {
//            return new Employee
//            {
//                FullName = "Иванов Иван Иванович",
//                Position = "Программист",
//                Salary = 50000m,
//                HireDate = new DateTime(2024, 5, 10)
//            };
//        }

//        // Проверка добавления сотрудника
//        [TestMethod]
//        public void Add_ValidEmployee_ReturnsPositiveId()
//        {
//            Employee employee = NewEmployee();

//            int id = _repository.Add(employee);

//            Assert.IsTrue(id > 0);
//            Assert.AreEqual(1, _repository.Count());
//        }

//        // Проверка получения сотрудника по ID
//        [TestMethod]
//        public void GetById_ExistingEmployee_ReturnsEmployee()
//        {
//            Employee employee = NewEmployee();
//            int id = _repository.Add(employee);

//            Employee result = _repository.GetById(id);

//            Assert.IsNotNull(result);
//            Assert.AreEqual(employee.FullName, result.FullName);
//            Assert.AreEqual(employee.Position, result.Position);
//            Assert.AreEqual(employee.Salary, result.Salary);
//            Assert.AreEqual(employee.HireDate, result.HireDate);
//        }

//        // Проверка получения несуществующего сотрудника
//        [TestMethod]
//        public void GetById_UnknownId_ReturnsNull()
//        {
//            Employee result = _repository.GetById(-1);

//            Assert.IsNull(result);
//        }

//        // Проверка удаления сотрудника
//        [TestMethod]
//        public void Delete_ExistingEmployee_ReturnsTrue()
//        {
//            int id = _repository.Add(NewEmployee());

//            bool result = _repository.Delete(id);

//            Assert.IsTrue(result);
//            Assert.IsNull(_repository.GetById(id));
//            Assert.AreEqual(0, _repository.Count());
//        }

//        // Проверка удаления несуществующего сотрудника
//        [TestMethod]
//        public void Delete_UnknownId_ReturnsFalse()
//        {
//            bool result = _repository.Delete(-1);

//            Assert.IsFalse(result);
//        }

//        // Проверка подсчёта сотрудников
//        [TestMethod]
//        public void Count_AfterAddingTwoEmployees_ReturnsTwo()
//        {
//            _repository.Add(NewEmployee());

//            Employee secondEmployee = NewEmployee();
//            secondEmployee.FullName = "Петров Пётр Петрович";

//            _repository.Add(secondEmployee);

//            Assert.AreEqual(2, _repository.Count());
//        }
//    }

//    // Тестовая реализация интерфейса без подключения к базе данных
//    public class FakeEmployeeRepository : IEmployeeRepository
//    {
//        private readonly List<Employee> _employees =
//            new List<Employee>();

//        private int _nextId = 1;

//        public int Add(Employee employee)
//        {
//            employee.Id = _nextId++;
//            _employees.Add(employee);

//            return employee.Id;
//        }

//        public Employee GetById(int id)
//        {
//            return _employees.FirstOrDefault(
//                employee => employee.Id == id);
//        }

//        public bool Delete(int id)
//        {
//            Employee employee = GetById(id);

//            if (employee == null)
//            {
//                return false;
//            }

//            _employees.Remove(employee);
//            return true;
//        }

//        public int Count()
//        {
//            return _employees.Count;
//        }
//    }
//}
