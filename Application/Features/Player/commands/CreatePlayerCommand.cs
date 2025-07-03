
using CSharpFunctionalExtensions;
using CQRS_mediatR.Application.DTOs;
using MediatR;

namespace CQRS_mediatR.Application.Features.Player.commands;

public record CreatePlayerCommand(CreateGamePlayerRequest dto) : IRequest<Result<Guid>>;


