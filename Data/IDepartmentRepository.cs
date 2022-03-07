using EmployeesAPI.Models;

namespace EmployeesAPI.Data;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetDepartments();
}
