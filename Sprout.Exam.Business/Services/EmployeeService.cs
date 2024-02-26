using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.DataAccess.Entities;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IRepository<Employee> _employeeRepository;
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Create(CreateEmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                else
                {
                    return await _employeeRepository.AddAsync(new Employee()
                    {
                        FullName = employee.FullName,
                        Birthdate = employee.Birthdate.ToString(),
                        Tin = employee.Tin,
                        EmployeeTypeId = employee.TypeId
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Update(EditEmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                else
                {
                    return await _employeeRepository.UpdateAsync(new Employee()
                    {
                        FullName = employee.FullName,
                        Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                        Tin = employee.Tin,
                        EmployeeTypeId = employee.TypeId,
                        Id = employee.Id
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                return await _employeeRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException(nameof(Employee));
                }
                else
                {
                    var employee = await _employeeRepository.GetByIdAsync(id);
                    return new EmployeeDto()
                    {
                        FullName = employee.FullName,
                        Birthdate = Convert.ToDateTime(employee.Birthdate).ToString("yyyy-MM-dd"),
                        Tin = employee.Tin,
                        TypeId = employee.EmployeeTypeId,
                        Id = employee.Id
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                List<EmployeeDto> employeeList = new List<EmployeeDto>();
               
                var employees = await _employeeRepository.GetAllAsync();

                if (employees.Count() > 0)
                {
                    //Convert Entity to DTO
                    foreach (Employee employee in employees)
                    {
                        employeeList.Add(new EmployeeDto()
                        {
                            FullName = employee.FullName,
                            Birthdate = Convert.ToDateTime(employee.Birthdate).ToString("yyyy-MM-dd"),
                            Tin = employee.Tin,
                            TypeId = employee.EmployeeTypeId,
                            Id = employee.Id

                        });
                    }
                }

                return employeeList;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
