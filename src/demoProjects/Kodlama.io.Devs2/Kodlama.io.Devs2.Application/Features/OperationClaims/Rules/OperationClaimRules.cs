using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Rules;

public class OperationClaimRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }


    public void OperationClaimShouldExistWhenRequested(OperationClaim? operationClaim)
    {
        if (operationClaim == null) throw new BusinessException("Rol mevcut değildir.");
    }
}
