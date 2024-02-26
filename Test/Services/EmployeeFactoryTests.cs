using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess.Entities;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class EmployeeFactoryTests
    {
        [TestMethod]
        public void ContractEmployee_CalculateSalary_ReturnsCorrectSalary()
        {
            // Arrange
            decimal dailySalary = 500;
            decimal workDays = 15.5m;
            var contractualEmployee = new ContractualEmployee(dailySalary, workDays);

            // Act
            decimal salary = contractualEmployee.CalculateSalary();

            // Assert
            Assert.AreEqual(7750, salary);
        }

        [TestMethod]
        public void RegularEmployee_CalculateSalary_ReturnsCorrectSalary()
        {
            // Arrange
            decimal monthlySalary = 20000;
            decimal absentDays = 1;
            var regularEmployee = new RegularEmployee(monthlySalary, absentDays);

            // Act
            decimal salary = regularEmployee.CalculateSalary();
            salary = decimal.Round(salary, 2, MidpointRounding.AwayFromZero);
            // Assert
            Assert.AreEqual(16690.91m, salary);
        }
    }
}
