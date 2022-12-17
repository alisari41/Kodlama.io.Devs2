using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.Technologies.Models;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Queries.GetListTechnologyByDynamic;

public class GetListTechnologyByDynamicQuery : IRequest<TechnologyListModel>
{
    public Dynamic Dynamic { get; set; } // Dinamik sorgu yazmak için kullanacağız
    public PageRequest PageRequest { get; set; }

    public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechnologyListModel>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;

        public GetListTechnologyByDynamicQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }

        public async Task<TechnologyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Technology> technologies = await _technologyRepository.GetListByDynamicAsync( // Dinamik Sorgu
                                                        request.Dynamic,
                                                        include: x => x.Include(c => c.ProgrammingLanguage), // Hangi tablodan veri alınacaksa
                                                        index: request.PageRequest.Page,
                                                        size: request.PageRequest.PageSize);

            var mappedTechnologyListModel = _mapper.Map<TechnologyListModel>(technologies);

            return mappedTechnologyListModel;
        }
    }
}
