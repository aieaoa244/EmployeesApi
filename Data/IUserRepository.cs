using EmployeesAPI.Models;

namespace EmployeesAPI.Data;

public interface IUserRepository
{
    Task<ExecResponse<int>> AddUser(User user);
    Task<User?> GetUser(User user);
}
