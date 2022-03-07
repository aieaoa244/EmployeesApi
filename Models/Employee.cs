using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models;

/// <summary>
/// Represents an employee
/// </summary>
public class Employee
{
    /// <summary>
    /// Employee's unique identifier
    /// </summary>
    /// <example>5</example>
    public int EmployeeID { get; set; }

    /// <summary>
    /// Employee's name
    /// </summary>
    /// <example>Diego</example>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Employee is still an employee
    /// </summary>
    /// <example>True</example>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Employee's department identifier
    /// </summary>
    /// <example>2</example>
    [Required]
    public int? DepartmentID { get; set; }

    /// <summary>
    /// Employee's department
    /// </summary>
    /// <example>Tech</example>
    public string Department { get; set; } = string.Empty;
}
