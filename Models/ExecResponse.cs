namespace EmployeesAPI.Models;

/// <summary>
/// Represents query response
/// </summary>
public class ExecResponse<T>
{
    /// <summary>
    /// Response status code
    /// </summary>
    /// <example>200</example>
    public int Status { get; set; }

    /// <summary>
    /// Response message
    /// </summary>
    /// <example>All's good</example>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional data
    /// </summary>
    public T? Data { get; set; }
}
