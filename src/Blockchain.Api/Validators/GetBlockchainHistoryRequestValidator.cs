using Blockchain.Api.Contracts;
using Blockchain.Domain.Enums;
using FluentValidation;

namespace Blockchain.Api.Validators;

public sealed class GetBlockchainHistoryRequestValidator
    : AbstractValidator<GetBlockchainHistoryRequest>
{
    public GetBlockchainHistoryRequestValidator()
    {
        RuleFor(x => x.Type)
            .Must(BeValidBlockchain)
            .WithMessage("Invalid blockchain type");

        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(1000);
    }

    private bool BeValidBlockchain(string type)
        => Enum.TryParse<BlockchainType>(type, true, out _);
}