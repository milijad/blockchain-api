using FluentResults;
using MediatR;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

public sealed record FetchLtcMainSnapshotCommand() : IRequest<Result>;