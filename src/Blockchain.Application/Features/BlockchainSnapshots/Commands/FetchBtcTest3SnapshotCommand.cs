using FluentResults;
using MediatR;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

public sealed record FetchBtcTest3SnapshotCommand() : IRequest<Result>;