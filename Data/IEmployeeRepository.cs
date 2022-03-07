using EmployeesAPI.Models;
using EmployeesAPI.Models.Paging;

namespace EmployeesAPI.Data;

public interface IEmployeeRepository
{
    Task<ExecResponse<int>> SaveEmployee(Employee employee);

    Task<Employee?> GetEmployeeById(int id);

    Task<ExecResponse<int>> DeleteEmployee(int id);

    Task<PagedResult<Employee>?> GetEmployees(PagingParameters parameters);
}
