using EmployeesAPI.Data;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repository;
    public EmployeeController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Returns the employee by id. Requires autorization
    /// </summary>
    /// <param name="id"></param>
    /// <response code="404">Employee with id not exists</response>
    /// <response code="401">Not authenticated. Request must have auth header</response>
    /// <response code="200">Returns an employee</response>
    [HttpGet]
    [Route("{id:int}", Name = "GetEmployee")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _repository.GetEmployeeById(id);
        if (employee == null)
            return NotFound();
        return Ok(employee);
    }

    /// <summary>
    /// Returns the list of employees. Requires authorization
    /// </summary>
    /// <response code="401">Not authenticated. Request must have auth header</response>
    /// <response code="200">Returns paged and filtered employee list</response>
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetEmployeeList([FromQuery]PagingParameters parameters)
    {
        return Ok(await _repository.GetEmployees(parameters));
    }


    /// <summary>
    /// Add new or edit existing employee information. Requires autorization
    /// </summary>
    /// <response code="404">Employee with id not exists</response>
    /// <response code="400">Employee with that name already exists</response>
    /// <response code="201">Employee successfully added</response>
    /// <response code="401">Not authenticated. Request must have auth header</response>
    /// <response code="200">Edit data was saved</response>
    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> SaveEmployee([FromBody]Employee employee)
    {
        var response = await _repository.SaveEmployee(employee);

        if (response.Status == 404)
            return NotFound(response);
        else if (response.Status == 400)
            return BadRequest(response);
        else if (response.Status == 201)
            return CreatedAtRoute(nameof(GetEmployee),
                new {id = response.Data},
                new Employee {
                    EmployeeID = response.Data,
                    Name = employee.Name,
                    DepartmentID = employee.DepartmentID,
                    Department = employee.Department
                });

        return Ok(response);
    }

    /// <summary>
    /// Delete employee information by id. Requires autorization
    /// </summary>
    /// <param name="id"></param>
    /// <response code="404">Employee with id not exists</response>
    /// <response code="401">Not authenticated. Request must have auth header</response>
    /// <response code="200">Data successfully deleted</response>
    [HttpPost]
    [Route("{id:int}/delete")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var response = await _repository.DeleteEmployee(id);
        if (response.Status == 404)
            return NotFound(response);
        return Ok(response);
    }
}
