using System.Data.SqlClient;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Paging;
using EmployeesAPI.Utils;

namespace EmployeesAPI.Data;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string connectionString;
    public EmployeeRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("EmployeesConnection");
    }

    public async Task<ExecResponse<int>> SaveEmployee(Employee employee)
    {
        var EmployeeID = new SqlParameter("@id", employee.EmployeeID);
        var Name = new SqlParameter("@name", employee.Name);
        var IsActive = new SqlParameter("@isActive", employee.IsActive);
        var DepartmentID = new SqlParameter("@departmentId", employee.DepartmentID);

        var response = await SqlUtils.GetData<ExecResponse<int>>(connectionString,
            "spSaveEmployee",
            Map.ToExecResponseInt,
            EmployeeID, Name, IsActive, DepartmentID);

        var result = response.FirstOrDefault();
        if (result == null)
            throw new Exception("Query has not returned result");
        return result;
    }

    public async Task<Employee?> GetEmployeeById(int id)
    {
        var EmployeeID = new SqlParameter("@id", id);

        var response = await SqlUtils.GetData<Employee>(connectionString,
            "spGetEmployeeById",
            Map.ToEmployee,
            EmployeeID);

        return response.FirstOrDefault();
    }

    public async Task<ExecResponse<int>> DeleteEmployee(int id)
    {
        var EmployeeID = new SqlParameter("@id", id);

        var response = await SqlUtils.GetData<ExecResponse<int>>(connectionString,
            "spDeleteEmployee",
            Map.ToExecResponseInt,
            EmployeeID);

        var result = response.FirstOrDefault();
        if (result == null)
            throw new Exception("Query has not returned result");
        return result;
    }

    public async Task<PagedResult<Employee>?> GetEmployees(PagingParameters parameters)
    {
        var page = new SqlParameter("@page", parameters.Page);
        var size = new SqlParameter("@size", parameters.Size);
        var search = new SqlParameter("@search", parameters.Search);
        
        var cntResponse = await SqlUtils.GetData<ExecResponse<int>>(connectionString,
            "spGetEmployeesPagedCount",
            Map.ToExecResponseInt,
            search);
        var count = cntResponse.FirstOrDefault()?.Data;
        if (count == null)
            return null;

        var items = await SqlUtils.GetData<Employee>(connectionString,
            "spGetEmployeesPaged",
            Map.ToEmployee,
            page, size, search);
            
        return new PagedResult<Employee>(items,
            parameters.Page,
            parameters.Size,
            (int)count);
    }
}
