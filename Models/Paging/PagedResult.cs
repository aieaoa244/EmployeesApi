namespace EmployeesAPI.Models.Paging;

/// <summary>
/// Represents the list of query items and additional data used for paging.
/// </summary>
public class PagedResult<T>
{
    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total page count
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Total query rowcount
    /// </summary>
    public int TotalItems { get; set; }

    public bool HasPrev => Page > 1;
    public bool HasNext => Page < TotalPages;

    /// <summary>
    /// The list of returned items
    /// </summary>
    public List<T> Items { get; set; }

    public PagedResult(List<T> items, int page, int pageSize, int totalItems)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        TotalItems = totalItems;
    }
}
