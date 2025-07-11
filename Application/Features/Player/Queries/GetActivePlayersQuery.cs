using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Application.Errors;
using OneOf;
using MediatR;

namespace CQRS_mediatR.Application.Features.Player.Queries
{
    public record GetActivePlayersQuery : IRequest<OneOf<IEnumerable<GamePlayerDetailResponse>, GamePlayerNotFound>>;
}