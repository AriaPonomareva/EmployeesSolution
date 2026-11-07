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
        // Проверка налога с зарплаты 50000
        [TestMethod]
        public void CalculateTax_Salary50000_Returns6500()
        {
                // Arrange
                decimal salary = 50000m;

                // Act
                decimal result =
                    PayrollCalculator.CalculateTax(salary);

                // Assert
                Assert.AreEqual(6500m, result);
            }

            // 2. Налог с нулевой зарплаты
            [TestMethod]
            public void CalculateTax_Zero_ReturnsZero()
            {
                // Arrange
                decimal salary = 0m;

                // Act
                decimal result =
                    PayrollCalculator.CalculateTax(salary);

                // Assert
                Assert.AreEqual(0m, result);
            }

            // 3. Отрицательная зарплата
            [TestMethod]
            public void CalculateTax_NegativeSalary_ThrowsException()
            {
                // Arrange
                decimal salary = -1000m;

                // Act и Assert
                Assert.ThrowsException<ArgumentException>(
                    () => PayrollCalculator.CalculateTax(salary));
            }

            // 4. Расчёт зарплаты после вычета налога
            [TestMethod]
            public void CalculateNetSalary_50000_Returns43500()
            {
                // Arrange
                decimal salary = 50000m;

                // Act
                decimal result =
                    PayrollCalculator.CalculateNetSalary(salary);

                // Assert
                Assert.AreEqual(43500m, result);
            }

            // 5. Стаж ровно один год
            [TestMethod]
            public void GetExperienceYears_ExactlyOneYear_ReturnsOne()
            {
                // Arrange
                DateTime today = new DateTime(2026, 10, 20);
                DateTime hireDate = new DateTime(2025, 10, 20);

                // Act
                int result =
                    PayrollCalculator.GetExperienceYears(hireDate, today);

                // Assert
                Assert.AreEqual(1, result);
            }

            // 6. До полного года не хватает одного дня
            [TestMethod]
            public void GetExperienceYears_OneDayBeforeYear_ReturnsZero()
            {
                // Arrange
                DateTime today = new DateTime(2026, 10, 20);
                DateTime hireDate = new DateTime(2025, 10, 21);

                // Act
                int result =
                    PayrollCalculator.GetExperienceYears(hireDate, today);

                // Assert
                Assert.AreEqual(0, result);
            }

            // 7. Дата приёма в будущем
            [TestMethod]
            public void GetExperienceYears_FutureHireDate_ThrowsException()
            {
                // Arrange
                DateTime today = new DateTime(2026, 10, 20);
                DateTime hireDate = new DateTime(2027, 1, 1);

                // Act и Assert
                Assert.ThrowsException<ArgumentException>(
                    () => PayrollCalculator.GetExperienceYears(
                        hireDate, today));
            }

            // 8–11. Проверка премии при разном стаже
            [DataTestMethod]
            [DataRow(0, 50000.0, 0.0)]
            [DataRow(1, 50000.0, 2500.0)]
            [DataRow(5, 50000.0, 2500.0)]
            [DataRow(6, 50000.0, 5000.0)]
            public void CalculateBonus_DifferentExperience_ReturnsExpectedBonus(
                int years,
                double salaryValue,
                double expectedValue)
            {
                // Arrange
                decimal salary = (decimal)salaryValue;
                decimal expected = (decimal)expectedValue;

                // Act
                decimal result =
                    PayrollCalculator.CalculateBonus(salary, years);

                // Assert
                Assert.AreEqual(expected, result);
            }

        // 12. Проверка округления налога до двух знаков
        [TestMethod]
        public void CalculateTax_Rounding_ReturnsTwoDecimalPlaces()
        {
            // Arrange
            decimal salary = 0.50m;

            // Act
            decimal result = PayrollCalculator.CalculateTax(salary);

            // Assert
            Assert.AreEqual(0.07m, result);
        }
    }
}
