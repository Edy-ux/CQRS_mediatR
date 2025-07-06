using CQRS_mediatR.Application.DTOs;
using CSharpFunctionalExtensions;
using CQRS_mediatR.Domain;
using MediatR;

namespace CQRS_mediatR.Application.Features.Player.Queries
{
    public record GetActivePlayersQuery() : IRequest<Result<IEnumerable<GamePlayerDetailResponse>>>;
}