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

public class FetchEthMainSnapshotHandlerTests
{
    [Fact]
    public async Task Handle_WhenApiReturnsSuccess_ShouldPersistSnapshot()
    {
        var client = new Mock<IBlockCypherClient>();
        client.Setup(x => x.GetEthMainAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(new EthMainResponseDto()));

        var repo = new Mock<IBlockchainSnapshotRepository>();
        var uow = new Mock<IUnitOfWork>();
        uow.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new FetchEthMainSnapshotHandler(
            client.Object,
            repo.Object,
            NullLogger<FetchEthMainSnapshotHandler>.Instance, 
            uow.Object);

        var result = await handler.Handle(new FetchEthMainSnapshotCommand(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        repo.Verify(r => r.AddAsync(It.IsAny<BlockchainSnapshot>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
