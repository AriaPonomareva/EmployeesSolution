using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeesApp.Models;
using EmployeesApp.Services;

namespace EmployeesApp.Tests
{

    public class TestEmployeeRepository : IEmployeeRepository // Заглушка репозитория вместо настоящей базы данных
    {

        private readonly List<Employee> _items =
            new List<Employee>();


        public int Add(Employee employee) // Добавление сотрудника
        {
            employee.Id = _items.Count + 1;

            _items.Add(employee);

            return employee.Id;
        }


        public Employee GetById(int id) // Поиск сотрудника по ID
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }


        public bool Delete(int id) // Удаление сотрудника
        {
            Employee employee = GetById(id);

            if (employee == null)
                return false;

            _items.Remove(employee);

            return true;
        }


        public int Count() // Количество сотрудников
        {
            return _items.Count;
        }
    }

    [TestClass]
    public class EmployeeServiceTests
    {
        private TestEmployeeRepository _repository;
        private EmployeeServices _service;


        [TestInitialize]
        public void Setup() //Выполняется перед каждым тестом
        {
            _repository = new TestEmployeeRepository();
            _service = new EmployeeServices(_repository);
        }


        private Employee NewEmployee( // Создание тестового сотрудника
            string name = "Петров Пётр Петрович",
            decimal salary = 60000m)
        {
            return new Employee
            {
                FullName = name,
                Position = "Бухгалтер",
                Salary = salary,
                HireDate = new DateTime(2025, 2, 1)
            };
        }


        [TestMethod]
        public void Hire_ValidEmployee_AddsEmployee() // Проверка успешного найма
        {
            Employee employee = NewEmployee();

            int id = _service.Hire(employee);

            Assert.AreEqual(1, _repository.Count());
            Assert.IsNotNull(_repository.GetById(id));
        }


        [TestMethod]
        public void Hire_EmptyName_ThrowsArgumentException() // Проверка найма сотрудника с пустым ФИО
        {
            Employee employee = NewEmployee(" ");

            Assert.ThrowsException<ArgumentException>(
                () => _service.Hire(employee));
        }


        [TestMethod]
        public void Hire_NegativeSalary_ThrowsArgumentException() // Проверка отрицательной зарплаты
        {
            Employee employee = NewEmployee(salary: -100m);

            Assert.ThrowsException<ArgumentException>(
                () => _service.Hire(employee));
        }


        [TestMethod]
        public void Fire_ExistingEmployee_RemovesEmployee() // Проверка увольнения существующего сотрудника
        {
            int id = _repository.Add(NewEmployee());

            _service.Fire(id);

            Assert.IsNull(_repository.GetById(id));
            Assert.AreEqual(0, _repository.Count());
        }


        [TestMethod]
        public void Fire_UnknownId_ThrowsInvalidOperationException() // Проверка увольнения несуществующего сотрудника
        {
            Assert.ThrowsException<InvalidOperationException>(
                () => _service.Fire(999));
        }
    }
}