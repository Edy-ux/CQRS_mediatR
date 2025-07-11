
using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Application.Errors;
using MediatR;
using OneOf;
namespace CQRS_mediatR.Application.Features.Player.commands;

public record CreatePlayerCommand(CreateGamePlayerRequest dto) : IRequest<OneOf<Guid, CreationPlayerError>>;


