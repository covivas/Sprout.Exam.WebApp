using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> Create(CreateEmployeeDto employee);
        Task<int> Update(EditEmployeeDto employee);
        Task<int> Delete(int id);
        Task<EmployeeDto> GetById(int id);
        Task<IEnumerable<EmployeeDto>> GetAll();

    }
}
