using Sprout.Exam.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public abstract class BaseSaveEmployeeDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Input valid TIN")]
        public string Tin { get; set; }

        [Required]
        [AgeRange(18, 100, ErrorMessage = "Age must be between 18 and 120 years old.")]
        public DateTime Birthdate { get; set; }

        [Required]
        public int TypeId { get; set; }
    }
}
