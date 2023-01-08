using AutoMapper;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>
{
    public int Id { get; set; }

    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimRules _operationClaimRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimRules = operationClaimRules;
        }

        public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            //var operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id); // Silmek istediğim verinin bütün bilgilerine ihtiyacım olmadığı için kullanmadım

            await _operationClaimRules.OperationClaimShouldExistWhenRequested(request.Id);

            var mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            var deletedOperationClaim = await _operationClaimRepository.DeleteAsync(mappedOperationClaim);
            var deletedOperationClaimDto = _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);

            return deletedOperationClaimDto;
        }
    }
}
