using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Models;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Queries.GetListProgrammingLanguage
{
    public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
    {
        public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

        public class GetListProgrammingLanguageHandler : IRequestHandler<GetListProgrammingLanguageQuery, ProgrammingLanguageListModel>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;

            public GetListProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }
            public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Domain.Entities.ProgrammingLanguage> programmingLanguages = await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize); // Veri tabanından veriler gelir

                ProgrammingLanguageListModel mappedProgrammingLanguageListModel = _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages); // Dbden aldıklarını modeldaki Page kısmına atıyorum

                return mappedProgrammingLanguageListModel;
            }

        }
    }
}
