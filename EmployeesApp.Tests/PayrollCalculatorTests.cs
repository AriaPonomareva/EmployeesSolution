using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeesApp.Services;

namespace EmployeesApp.Tests
{
    [TestClass]
    public class PayrollCalculatorTests
    {
        
        [TestMethod]
        public void CalculateTax_Salary50000_Returns6500() // Проверка налога с зарплаты 50000
        {
                
                decimal salary = 50000m; //входные данные

              
                decimal result =
                    PayrollCalculator.CalculateTax(salary); //вызов метода и сохранение результата

                
                Assert.AreEqual(6500m, result);
            }

           
            [TestMethod]
            public void CalculateTax_Zero_ReturnsZero()  // Налог с нулевой зарплаты
        {
               
                decimal salary = 0m; //проверка границ

                
                decimal result =
                    PayrollCalculator.CalculateTax(salary); 

                
                Assert.AreEqual(0m, result);
            }

            
            [TestMethod]
            public void CalculateTax_NegativeSalary_ThrowsException() // Отрицательная зарплата, негативный тест
        {
                
                decimal salary = -1000m;

                
                Assert.ThrowsException<ArgumentException>(
                    () => PayrollCalculator.CalculateTax(salary)); // Вызов исключения определенного типа + проверка
            }

            
            [TestMethod]
            public void CalculateNetSalary_50000_Returns43500() // Расчёт зарплаты после вычета налога
        {
                
                decimal salary = 50000m; 

                
                decimal result =
                    PayrollCalculator.CalculateNetSalary(salary);

               
                Assert.AreEqual(43500m, result);
            }

            
            [TestMethod]
            public void GetExperienceYears_ExactlyOneYear_ReturnsOne() // Стаж ровно один год
        {
                
                DateTime today = new DateTime(2026, 10, 20);
                DateTime hireDate = new DateTime(2025, 10, 20);

               
                int result =
                    PayrollCalculator.GetExperienceYears(hireDate, today); // Вычисление стажа

              
                Assert.AreEqual(1, result);
            }

            
            [TestMethod]
            public void GetExperienceYears_OneDayBeforeYear_ReturnsZero() //Граничный тест
            {
                
                DateTime today = new DateTime(2026, 10, 20);
                DateTime hireDate = new DateTime(2025, 10, 21);

                
                int result =
                    PayrollCalculator.GetExperienceYears(hireDate, today);

                
                Assert.AreEqual(0, result);
            }

            
            [TestMethod]
            public void GetExperienceYears_FutureHireDate_ThrowsException() // Дата приёма в будущем
        {
                
                DateTime today = new DateTime(2026, 10, 20);
                DateTime hireDate = new DateTime(2027, 1, 1);

                
                Assert.ThrowsException<ArgumentException>(
                    () => PayrollCalculator.GetExperienceYears(
                        hireDate, today));
            }

            
            [DataTestMethod]
            [DataRow(0, 50000.0, 0.0)]
            [DataRow(1, 50000.0, 2500.0)]
            [DataRow(5, 50000.0, 2500.0)]
            [DataRow(6, 50000.0, 5000.0)]
            public void CalculateBonus_DifferentExperience_ReturnsExpectedBonus(  //  Проверка премии при разном стаже
                int years,
                double salaryValue,
                double expectedValue) //параметры метода значений
            {
                
                decimal salary = (decimal)salaryValue;
                decimal expected = (decimal)expectedValue; //работа с денежным типом

                
                decimal result =
                    PayrollCalculator.CalculateBonus(salary, years);

                
                Assert.AreEqual(expected, result);
            }

        
        [TestMethod]
        public void CalculateTax_Rounding_ReturnsTwoDecimalPlaces() // Проверка округления налога до двух знаков
        {
            
            decimal salary = 0.50m;

            
            decimal result = PayrollCalculator.CalculateTax(salary);


            Assert.AreEqual(0.07m, result);
        }
    }
}
