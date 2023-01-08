using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task OperationClaimNameCanNotBeDuplacatedWhenInserted(string name)
    {
        var result = await _operationClaimRepository.Query().Where(x => x.Name == name).AnyAsync();
        if (result) throw new BusinessException("Bu Rol Mevcuttur.");
    }
}
