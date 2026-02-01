using Application.Features.Auth.Commands.Refresh;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/v{version:apiVersion}/auth")]
[ApiController]
[ApiVersion(1)]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshAuthInfoCommand body,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(body, cancellationToken);

        return Ok(response);
    }
}
