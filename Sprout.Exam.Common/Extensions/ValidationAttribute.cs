using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sprout.Exam.Common.Extensions
{
    #region Age Range Attribute
    public class AgeRangeAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        public AgeRangeAttribute(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                int age = CalculateAge(birthDate);

                if (age >= _minAge && age <= _maxAge)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Age must be between {_minAge} and {_maxAge} years.");
                }
            }

            return new ValidationResult("Invalid birth date");
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }

    #endregion
}
