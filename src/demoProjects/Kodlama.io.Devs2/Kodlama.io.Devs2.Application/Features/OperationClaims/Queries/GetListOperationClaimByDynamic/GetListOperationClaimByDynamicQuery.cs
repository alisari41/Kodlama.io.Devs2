using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Models;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetListOperationClaimByDynamic;

public class GetListOperationClaimByDynamicQuery : IRequest<OperationClaimsListModel>
{
    public Dynamic Dynamic { get; set; }// Dinamik sorgu yazmak için kullanacağız
    public PageRequest PageRequest { get; set; }

    public class GetListOperationClaimByDynamicQueryHandler : IRequestHandler<GetListOperationClaimByDynamicQuery, OperationClaimsListModel>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetListOperationClaimByDynamicQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<OperationClaimsListModel> Handle(GetListOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListByDynamicAsync(
                                                            request.Dynamic,
                                                            index: request.PageRequest.Page,
                                                            size: request.PageRequest.PageSize);

            var mappedOperationClaimListModel = _mapper.Map<OperationClaimsListModel>(operationClaims);

            return mappedOperationClaimListModel;
        }
    }
}
