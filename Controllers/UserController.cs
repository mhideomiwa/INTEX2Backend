using Intex2Backend.UserData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    // POST: users/user/login
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        // Authenticate user
        var user = _repository.GetUserByCredentials(model.UserName, model.Password);

        if (user == null)
            // Unauthorized - invalid credentials
            return Unauthorized();

        // Generate JWT token or any other authentication mechanism here
        // For simplicity, let's just return the user info for now
        return Ok(user);
    }

    // POST: user/register
    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] User newUser)
    {
        // Check if the user already exists
        var existingUser = _repository.GetUserByCredentials(newUser.UserName, newUser.Password);

        if (existingUser != null)
            // User already exists
            return Conflict("User already exists.");

        // Create the new user
        _repository.CreateUser(newUser);

        // Return success response
        return Ok("User created successfully.");
    }
}

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}