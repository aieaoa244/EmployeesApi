using EmployeesAPI.Models;
using EmployeesAPI.Utils;

namespace EmployeesAPI.Data;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly string connectionString;
    public DepartmentRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("EmployeesConnection");
    }

    public async Task<IEnumerable<Department>> GetDepartments()
    {
        return await SqlUtils.GetData<Department>(connectionString,
            "spGetDepartments",
            Map.ToDepartment);
    }
}
