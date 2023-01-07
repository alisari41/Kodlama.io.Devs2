using AutoMapper;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetByIdOperationClaim;

public class GetByIdOperationClaimQuery : IRequest<OperationClaimGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimGetByIdDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimRules _operationClaimRules;

        public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimRules = operationClaimRules;
        }

        public async Task<OperationClaimGetByIdDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
        {
            var result = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);

            _operationClaimRules.OperationClaimShouldExistWhenRequested(result);

            var operationClaimGetByIdDto=_mapper.Map<OperationClaimGetByIdDto>(result);
            return operationClaimGetByIdDto;
        }
    }
}
