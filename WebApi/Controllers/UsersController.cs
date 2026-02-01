using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.Register;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/v{version:apiVersion}/users")]
[ApiController]
[ApiVersion(1)]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("login")]
    public async Task<IActionResult> Login(
    [FromBody] LoginUserCommand body,
    CancellationToken cancellationToken)
    {
        var response = await mediator.Send(body, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [Route("register")]
    public async Task<IActionResult> Register(
    [FromBody] RegisterUserCommand body,
    CancellationToken cancellationToken)
    {
        var response = await mediator.Send(body, cancellationToken);

        return Ok(response);
    }
}
