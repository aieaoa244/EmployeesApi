namespace EmployeesAPI.Models;

/// <summary>
/// The department
/// </summary>
public class Department
{
    /// <summary>
    /// Unique department identifier
    /// </summary>
    /// <example>2</example>
    public int DepartmentID { get; set; }

    /// <summary>
    /// Department name
    /// </summary>
    /// <example>Tech</example>
    public string Name { get; set; } = string.Empty;
}
