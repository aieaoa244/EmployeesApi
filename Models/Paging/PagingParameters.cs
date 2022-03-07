namespace EmployeesAPI.Models.Paging;

/// <summary>
/// Paging parameters.
/// </summary>
public class PagingParameters
{
    private int _page = 1;

    /// <summary>
    /// Page number
    /// </summary>
    /// <example>1</example>
    public int Page
    {
        get => _page;
        set => _page = value > 0 ? value : 1;
    }
    const int maxPageSize = 100;
    private int _pageSize = 20;

    /// <summary>
    /// Item count on one page
    /// </summary>
    /// <example>5</example>
    public int Size
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize || value < 1)
            ? maxPageSize
            : value;
    }

    /// <summary>
    /// Search string
    /// </summary>
    /// <example>Diego</example>
    public string? Search { get; set; }
}
