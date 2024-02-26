using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Services
{

    public class ContractualEmployee : IEmployee
    {
        public decimal DailySalary  { get; set; }
        public decimal WorkDays { get; set; }

        public ContractualEmployee(decimal dailySalary, decimal workDays)
        {
            DailySalary = dailySalary;
            WorkDays = workDays;
        }

        public decimal CalculateSalary()
        {
            return DailySalary * WorkDays;
        }
    }

    public class RegularEmployee : IEmployee
    {
        public decimal AbsentDays { get; set; }
        public decimal MonthlySalary { get; set; }

        public RegularEmployee(decimal monthlySalary, decimal absentDays)
        {
            MonthlySalary = monthlySalary;
            AbsentDays = absentDays;
        }

        public decimal CalculateSalary()
        {
            return MonthlySalary - (AbsentDays * (MonthlySalary / 22)) - (MonthlySalary * 0.12m);
        }
    }
}
