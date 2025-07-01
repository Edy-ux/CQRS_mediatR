
using CSharpFunctionalExtensions;
using GamePlayerCQRS.Models.DTOs;
using MediatR;

namespace GamePlayerCQRS.Features.Player.commands;

public record CreatePlayerCommand(CreateGamePlayerRequest dto)
       : IRequest<Result<Guid>>;


