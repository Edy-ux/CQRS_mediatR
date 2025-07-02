
using CSharpFunctionalExtensions;
using GamePlayerCQRS.Models;
using MediatR;

namespace GamePlayerCQRS.Features.Player.Queries
{
    public record GetActivePlayersQuery() : IRequest<Result<IEnumerable<GamePlayer>>>;
}