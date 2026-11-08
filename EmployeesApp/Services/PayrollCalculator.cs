using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.Services
{
    public static class PayrollCalculator
    {
        // Расчёт налога 13%
        public static decimal CalculateTax(decimal salary)
        {
            if (salary < 0)
            {
                throw new ArgumentException(
                    "Зарплата не может быть отрицательной.");
            }

            return Math.Round(
                salary * 0.13m,
                2,
                MidpointRounding.AwayFromZero);
        }

        // Расчёт зарплаты после вычета налога
        public static decimal CalculateNetSalary(decimal salary)
        {
            return salary - CalculateTax(salary);
        }

        // Расчёт полного количества лет стажа
        public static int GetExperienceYears(
            DateTime hireDate,
            DateTime today)
        {
            if (hireDate.Date > today.Date)
            {
                throw new ArgumentException(
                    "Дата приёма не может быть в будущем.");
            }

            int years = today.Year - hireDate.Year;

            if (hireDate.Date > today.Date.AddYears(-years))
            {
                years--;
            }

            return years;
        }

        // Расчёт премии в зависимости от стажа
        public static decimal CalculateBonus(
            decimal salary,
            int years)
        {
            if (salary < 0)
            {
                throw new ArgumentException(
                    "Зарплата не может быть отрицательной.");
            }

            if (years < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(years),
                    "Стаж не может быть отрицательным.");
            }

            decimal percent;

            if (years < 1)
            {
                percent = 0m;
            }
            else if (years <= 5)
            {
                percent = 0.05m;
            }
            else
            {
                percent = 0.10m;
            }

            return Math.Round(
                salary * percent,
                2,
                MidpointRounding.AwayFromZero);
        }
    }
}
