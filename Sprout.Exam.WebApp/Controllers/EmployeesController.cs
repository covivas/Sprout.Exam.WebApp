using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Entities;
using Sprout.Exam.Business.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sprout.Exam.WebApp.Models;
using System.Net;
using Sprout.Exam.Business.Services;
using Sprout.Exam.Common.Extensions;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of employees that are active</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await Task.FromResult(await _employeeService.GetAll());
            return Ok(employees);
        }

        /// <summary>
        /// Accepts Employee id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await Task.FromResult(await _employeeService.GetById(id));
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        /// <summary>
        /// Accepts employee Id
        /// </summary>
        /// <returns>Updated employee record</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            if (ModelState.IsValid)
            {
                var employee = await Task.FromResult(await _employeeService.Update(input));

                if (employee == 0) return NotFound();

                return Ok(employee);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();

                return BadRequest(errors);
            }
        }

        /// <summary>
        /// Accepts employee record
        /// </summary>
        /// <returns>New Employee ID created</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            if (ModelState.IsValid)
            {
                var id = await Task.FromResult(await _employeeService.Create(input));
                return Created($"/api/employees/{id}", id);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();

                return BadRequest(errors);
            }
        }


        /// <summary>
        /// Soft Delete existing Employee
        /// </summary>
        /// <returns>
        /// Not Found - when employee Id not exists
        /// Success - when deleted successfully
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await Task.FromResult(await _employeeService.Delete(id));
            if (employee == 0) return NotFound();
            return Ok(id);
        }



        /// <summary>
        /// Calculates employee salary based on absent and worked days and employee type
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(SalaryDto salary)
        {
            var employee = await Task.FromResult(await _employeeService.GetById(salary.Id));
            if (employee == null) return NotFound();
            var type = (EmployeeType)employee.TypeId;

            return type switch
            {
                EmployeeType.Regular =>
                    Ok(new RegularEmployee(20000, salary.AbsentDays).CalculateSalary().ToFormat()),
                EmployeeType.Contractual =>
                    Ok(new ContractualEmployee(500, salary.WorkedDays).CalculateSalary().ToFormat()),
                _ => NotFound("Employee Type not found")
            };

        }

    }
}
