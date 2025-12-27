using Blockchain.Application.DTOs.BlockCypher;
using FluentResults;

namespace Blockchain.Application.Interfaces.Clients;

public interface IBlockCypherClient
{
    Task<Result<EthMainResponseDto>> GetEthMainAsync(CancellationToken cancellationToken);
}