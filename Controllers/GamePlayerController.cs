using Microsoft.AspNetCore.Mvc;
using MediatR;
using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Application.Features.Player.commands;
using CQRS_mediatR.Domain.ValueObjects;
using CQRS_mediatR.Application.Features.Player.Queries;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CQRS_mediatR.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GamePlayerController : ControllerBase
{
    private readonly ILogger<GamePlayerController> _logger;
    private readonly ISender _sender;

    public GamePlayerController(ILogger<GamePlayerController> logger, IMediator sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll() // TODO: Implementar com MediatR Query
    {
        throw new NotImplementedException();
    }

    [HttpGet("active")]
    public async Task<IResult> GetActives()
    {
        var result = await _sender.Send(new GetActivePlayersQuery());

        return result.Match(players => Results.Ok(players),
            error => Results.BadRequest(new { message = error.message })
        );
    }

    [HttpGet("{id}")]
    public IActionResult GetPlayerById(string id) // TODO: Implementar com MediatR Query
    {
        throw new NotImplementedException();
    }

    [HttpPost("create")]
    public async Task<Results<Created<GamePlayerResponse>, BadRequest<ProblemDetails>>> Create(
        CreateGamePlayerRequest request)
    {
        var response = await _sender.Send(new CreatePlayerCommand(request));

        return response.Match<Results<Created<GamePlayerResponse>, BadRequest<ProblemDetails>>>(
            playerId => TypedResults.Created($"/player/{playerId}", new GamePlayerResponse
            {
                Id = playerId,
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                Status = PlayerStatus.Active.ToString()
            }),
            error =>
            {
                _logger.LogError("Error during player creation");
                return TypedResults.BadRequest(new ProblemDetails
                {
                    Detail = error.message, Title = "Domain Errors", Type = error.errorType.ToString(),
                    Status = StatusCodes.Status400BadRequest
                });
            }
        );
    }

    [HttpPut("update/{id}")]
    public IActionResult Update(int id, CreateGamePlayerRequest game) // TODO: Implementar com MediatR Command 
    {
        throw new NotImplementedException();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeletePlayer(int id) // TODO: Implementar com MediatR Command
    {
        throw new NotImplementedException();
    }
}