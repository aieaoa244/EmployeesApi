using System.Data.SqlClient;
using EmployeesAPI.Models;
using EmployeesAPI.Utils;

namespace EmployeesAPI.Data;

public class UserRepository : IUserRepository
{
    private readonly string connectionString;
    public UserRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("EmployeesConnection");
    }

    public async Task<ExecResponse<int>> AddUser(User user)
    {
        var name = new SqlParameter("@name", user.Name);
        var hashedPassword = JwtUtils.HashPassword(user.Password);
        var password = new SqlParameter("@password", hashedPassword);

        var response = await SqlUtils.GetData<ExecResponse<int>>(connectionString,
            "spAddUser",
            Map.ToExecResponseInt,
            name, password);

        var result = response.FirstOrDefault();
        if (result == null)
            throw new Exception("Query has not returned result");
        return result;
    }

    public async Task<User?> GetUser(User user)
    {
        var name = new SqlParameter("@name", user.Name);

        var response = await SqlUtils.GetData<User>(connectionString,
            "spGetUser",
            Map.ToUser,
            name);
        
        return response.FirstOrDefault();
    }
}
