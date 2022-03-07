using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models;

/// <summary>
/// A user
/// </summary>
public class User
{
    internal int UserId { get; set; }

    /// <summary>
    /// The user's name
    /// </summary>
    /// <example>Antonio</example>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The user's password
    /// </summary>
    /// <example>Pa$$w0rd!</example>
    [Required]
    public string Password { get; set; } = string.Empty;
}
