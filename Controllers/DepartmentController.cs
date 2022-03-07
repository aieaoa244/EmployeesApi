using EmployeesAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _repository;
    public DepartmentController(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Returns the list of departments. Requires authorization
    /// </summary>
    /// <response code="200">Returns the list of departments</response>
    /// <response code="401">Not authenticated. Request must have auth header</response>
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetDepartmentList()
    {
        return Ok(await _repository.GetDepartments());
    }
}