using Blockchain.Application.Features.BlockchainSnapshots.Queries;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Blockchain.UnitTests;

public class GetBlockchainHistoryQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenSnapshotsExist_ReturnsPayloads()
    {
        var repo = new Mock<IBlockchainSnapshotRepository>();

        repo.Setup(r => r.GetHistoryAsync(BlockchainType.EthMain, 2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<BlockchainSnapshot>
            {
                new() { Id = 2, PayloadJson = "{eth:2}" },
                new() { Id = 1, PayloadJson = "{eth:1}" }
            });

        var handler = new GetBlockchainHistoryQueryHandler(repo.Object, NullLogger<GetBlockchainHistoryQueryHandler>.Instance);

        var result = await handler.Handle(
            new GetBlockchainHistoryQuery(BlockchainType.EthMain, 2),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().ContainInOrder("{eth:2}", "{eth:1}");
    }
}
