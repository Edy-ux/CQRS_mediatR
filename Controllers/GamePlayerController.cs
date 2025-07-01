using Microsoft.AspNetCore.Mvc;

using GamePlayerCQRS.Models;
using MediatR;
using GamePlayerCQRS.Models.DTOs;
using GamePlayerCQRS.Features.Player.commands;
using GamePlayerCQRS.Models.ValueObjects;
using GamePlayerCQRS.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlayerCQRS.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class GamePlayerController : ControllerBase
{
    private readonly ILogger<GamePlayerController> _logger;
    private readonly ISender _sender;
    private readonly GamePlayerDbContext _context;
    public GamePlayerController(ILogger<GamePlayerController> logger, ISender sender, GamePlayerDbContext context)
    {
        _logger = logger;
        _sender = sender;
        _context = context;
    }
    [HttpGet("all")]
    public IActionResult GetAllPlayers() // TODO: Implementar com MediatR Query
    {
        throw new NotImplementedException();
    }
    [HttpGet("active")]
    public async Task<IActionResult> GetActivePlayers()
    {
        try
        {
            var activePlayers = await _context.GamePlayers
              .Where(g => g.Status == PlayerStatus.Active)// Or g => EF.Property<PlayerStatus>(g, "Status")
              .ToListAsync();

            return Ok(activePlayers);
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
    public async Task<IActionResult> CreatePlayer(CreateGamePlayerRequest request) // TODO: Implementar com MediatR Command
    {

        try
        {
            var player = await _sender.Send(new CreatePlayerCommand(request));

            return CreatedAtAction(nameof(CreatePlayer), new GamePlayerResponse
            {
                Id = player.Value,
                Name = request.Name,
                Email = request.Email,
                Role = request.Role ?? "Player",
                Status = PlayerStatus.Active.Value,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
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
