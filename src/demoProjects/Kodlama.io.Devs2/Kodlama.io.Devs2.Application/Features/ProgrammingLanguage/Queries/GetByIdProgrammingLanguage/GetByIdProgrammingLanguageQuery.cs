using AutoMapper;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;
using _ProgrammingLanguage = Kodlama.io.Devs2.Domain.Entities.ProgrammingLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Queries.GetByIdProgrammingLanguage
{
    public class GetByIdProgrammingLanguageQuery : IRequest<ProgrammingLanguageGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdProgrammingLanguageHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageRules _programmingLanguageRules;

            public GetByIdProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageRules programmingLanguageRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageRules = programmingLanguageRules;
            }

            public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                var programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id);

                _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage); // yollanan id boş mu diye kontrol sağlaması lazım

                var programmingLanguageGetByIdDto = _mapper.Map<ProgrammingLanguageGetByIdDto>(programmingLanguage);
                return programmingLanguageGetByIdDto;
            }
        }
    }
}
