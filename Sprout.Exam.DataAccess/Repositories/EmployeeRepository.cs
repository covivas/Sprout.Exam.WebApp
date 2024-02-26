using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Entities;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : IRepository <Employee>
    {
        private readonly WebAppContext _appContext;
        private readonly ILogger _logger;
        public EmployeeRepository(WebAppContext appContext, ILogger<Employee> logger)
        {
            _appContext = appContext;
            _logger = logger;
        }

        public async Task<int> AddAsync(Employee employee)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", employee.FullName);
                parameters.Add("Birthdate", employee.Birthdate);
                parameters.Add("TIN", employee.Tin);
                parameters.Add("EmployeeTypeId", employee.EmployeeTypeId);
                parameters.Add("IsDeleted", 0);

                using var connection = _appContext.CreateConnection();
                var affectedRows = await connection.ExecuteAsync(Constants.EmployeeSQL.Q_INSERT, parameters);

                return affectedRows;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                using var connection = _appContext.CreateConnection();
                var result = await connection.ExecuteAsync(Constants.EmployeeSQL.Q_DELETE, new { id });
                return result;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", employee.FullName);
                parameters.Add("Birthdate", employee.Birthdate);
                parameters.Add("TIN", employee.Tin);
                parameters.Add("EmployeeTypeId", employee.EmployeeTypeId);
                parameters.Add("Id", employee.Id);

                using var connection = _appContext.CreateConnection();
                var affectedRows = await connection.ExecuteAsync(Constants.EmployeeSQL.Q_UPDATE, parameters);

                return affectedRows;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                using var connection = _appContext.CreateConnection();
                var employees = await connection.QueryAsync<Employee>(Constants.EmployeeSQL.Q_SELECT_ALL);
                return employees;
            }
            catch(Exception ex)
            {
                return Enumerable.Empty<Employee>();
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                using var connection = _appContext.CreateConnection();
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(Constants.EmployeeSQL.Q_SELECT_BY_ID, new { id });
                return employee;
            }
            catch(Exception ex)
            {
                return new Employee();
            }
        }
    }
}
