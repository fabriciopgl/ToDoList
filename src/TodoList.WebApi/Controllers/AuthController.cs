using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Users.Commands;
using TodoList.Application.Users.Queries;
using TodoList.WebApi.Extensions;

namespace TodoList.WebApi.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]

public class AuthController(IMediator mediator, IUserQueries userQueries) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(new { UserId = result.Value, Message = "User registered successfully" });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsFailure)
            return Unauthorized(result.Error);

        return Ok(new { Token = result.Value, Message = "Login successful" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var user = await userQueries.GetByIdAsync(User.GetCurrentUserId(), cancellationToken);

        if (user is null)
            return NotFound("User not found");

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.CreatedAt
        });
    }
}