using EmployeesAPI.Data;
using EmployeesAPI.Models;
using EmployeesAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;
    public AuthController(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    /// <summary>
    /// Returns authentication token
    /// </summary>
    /// <response code="404">User not exists</response>
    /// <response code="400">Username or password is wrong</response>
    /// <response code="200">Returns authentication token</response>
    [HttpPost]
    [Route("token")]
    public async Task<IActionResult> Authenticate(User user)
    {
        var userFromDb = await _repository.GetUser(user);

        if (userFromDb == null)
            return NotFound(user);
        else if (!JwtUtils.CheckHashed(user.Password,
            userFromDb.Password))
            return BadRequest(user);

        var jwt = JwtUtils.GetToken(userFromDb,
            _configuration["Jwt:Key"],
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"]);

        return Ok(new {
            user = userFromDb.Name,
            token = jwt
        });
    }

    /// <summary>
    /// User registration endpoint
    /// </summary>
    /// <response code="400">User with this name already exists</response>
    /// <response code="200">Registration was successful</response>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(User user)
    {
        var response = await _repository.AddUser(user);

        if (response.Status == 400)
            return BadRequest(response);

        return Ok(response);
    }
}