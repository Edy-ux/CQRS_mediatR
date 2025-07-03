using Microsoft.AspNetCore.Mvc;

using CQRS_mediatR.Domain;
using MediatR;
using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Application.Features.Player.commands;
using CQRS_mediatR.Domain.ValueObjects;
using CQRS_mediatR.Application.Features.Player.Queries;

namespace CQRS_mediatR.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class GamePlayerController : ControllerBase
{
    private readonly ILogger<GamePlayerController> _logger;
    private readonly IMediator _sender;
    public GamePlayerController(ILogger<GamePlayerController> logger, IMediator sender)
    {
        _logger = logger;
        _sender = sender;

    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllPlayers() // TODO: Implementar com MediatR Query
    {
        throw new NotImplementedException();
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActivePlayers()
    {
        try
        {
            var result = await _sender.Send(new GetActivePlayersQuery());
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar jogadores ativos");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Acorreu um erro interno do servidor" });
        }
    }
    [HttpGet("{id}")]
    public IActionResult GetPlayerById(string id) // TODO: Implementar com MediatR Query
    {
        throw new NotImplementedException();

    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePlayer(CreateGamePlayerRequest request)
    {
        try
        {
            var response = await _sender.Send(new CreatePlayerCommand(request));

            if (response.IsFailure)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Erro de validação",
                    Detail = response.Error
                });
            }

            return CreatedAtAction(nameof(CreatePlayer), new GamePlayerResponse
            {
                Id = response.Value,
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                Status = PlayerStatus.Active.Value,
            });


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, new ProblemDetails { Title = "Error during player creation", Detail = ex.Message });
        }

    }

    [HttpPut("update/{id}")]
    public IActionResult UpdatePlayer(int id, GamePlayer game) // TODO: Implementar com MediatR Command 
    {
        return Ok("Game Updated");
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeletePlayer(int id) // TODO: Implementar com MediatR Command
    {
        return Ok("Game Deleted");
    }


}
