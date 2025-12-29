using Blockchain.Application.DTOs.BlockCypher;
using FluentResults;

namespace Blockchain.Application.Interfaces.Clients;

public interface IBlockCypherClient
{
    Task<Result<EthMainResponseDto>> GetEthMainAsync(CancellationToken cancellationToken);
    
    Task<Result<DashMainResponseDto>> GetDashMainAsync(CancellationToken cancellationToken);
    
    Task<Result<BtcMainResponseDto>> GetBtcMainAsync(CancellationToken cancellationToken);
    
    Task<Result<BtcTest3ResponseDto>> GetBtcTest3Async(CancellationToken cancellationToken);
    
    Task<Result<LtcMainResponseDto>> GetLtcMainAsync(CancellationToken cancellationToken);
}