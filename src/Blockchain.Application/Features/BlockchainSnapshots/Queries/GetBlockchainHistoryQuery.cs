using Blockchain.Domain.Enums;
using FluentResults;
using MediatR;

namespace Blockchain.Application.Features.BlockchainSnapshots.Queries;

public sealed record GetBlockchainHistoryQuery(
    BlockchainType Blockchain,
    int Limit) : IRequest<Result<IReadOnlyList<string>>>;