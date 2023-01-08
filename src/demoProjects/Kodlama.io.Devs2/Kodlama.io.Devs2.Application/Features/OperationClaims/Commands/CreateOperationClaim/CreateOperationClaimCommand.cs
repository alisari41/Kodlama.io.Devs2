using AutoMapper;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>
{
    public string Name { get; set; }

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimRules _operationClaimRules;

        public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimRules = operationClaimRules;
        }

        public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _operationClaimRules.OperationClaimNameCanNotBeDuplacatedWhenInserted(request.Name);

            var mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            var createOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
            var createdOperationClaimDto = _mapper.Map<CreatedOperationClaimDto>(createOperationClaim);

            return createdOperationClaimDto;
        }
    }
}
