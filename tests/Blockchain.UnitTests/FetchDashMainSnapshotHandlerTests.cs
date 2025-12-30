using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Features.BlockchainSnapshots.Commands;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Blockchain.UnitTests;

public class FetchDashMainSnapshotHandlerTests
{
    [Fact]
    public async Task Handle_WhenApiReturnsSuccess_ShouldPersistSnapshot()
    {
        var client = new Mock<IBlockCypherClient>();
        client.Setup(x => x.GetDashMainAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(new DashMainResponseDto()));

        var repo = new Mock<IBlockchainSnapshotRepository>();
        var uow = new Mock<IUnitOfWork>();
        uow.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new FetchDashMainSnapshotHandler(
            client.Object,
            repo.Object,
            uow.Object,
            NullLogger<FetchDashMainSnapshotHandler>.Instance
        );

        var result = await handler.Handle(new FetchDashMainSnapshotCommand(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        client.Verify(c => c.GetDashMainAsync(It.IsAny<CancellationToken>()), Times.Once);
        repo.Verify(r => r.AddAsync(It.IsAny<BlockchainSnapshot>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}