using System.Data.SqlClient;
using EmployeesAPI.Models;

namespace EmployeesAPI.Utils;

public static class Map
{
    public static Employee ToEmployee(SqlDataReader reader)
    {
        return new Employee()
        {
            EmployeeID = reader.GetInt32(reader.GetOrdinal("empID")),
            Name = reader.GetString(reader.GetOrdinal("empName")),
            IsActive = reader.GetBoolean(reader.GetOrdinal("empActive")),
            DepartmentID = reader.GetInt32(reader.GetOrdinal("emp_dpID")),
            Department = reader.GetString(reader.GetOrdinal("dpName"))
        };
    }

    public static Department ToDepartment(SqlDataReader reader)
    {
        return new Department()
        {
            DepartmentID = reader.GetInt32(reader.GetOrdinal("dpID")),
            Name = reader.GetString(reader.GetOrdinal("dpName"))
        };
    }

    public static ExecResponse<int> ToExecResponseInt(SqlDataReader reader)
    {
        return new ExecResponse<int>()
        {
            Status = reader.GetInt32(reader.GetOrdinal("code")),
            Message = reader.GetString(reader.GetOrdinal("msg")),
            Data = reader.GetInt32(reader.GetOrdinal("data"))
        };
    }

    public static User ToUser(SqlDataReader reader)
    {
        return new User()
        {
            UserId = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Password = reader.GetString(reader.GetOrdinal("PasswordHash"))
        };
    }
}
