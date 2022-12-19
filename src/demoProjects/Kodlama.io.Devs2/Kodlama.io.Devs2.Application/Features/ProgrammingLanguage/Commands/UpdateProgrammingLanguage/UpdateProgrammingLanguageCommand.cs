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

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        // Son kullanıcının bize göndereceği son dataları içeren yapı
        public int Id { get; set; }
        public string Name { get; set; }


        // Bir tanede Handlerımız var yani böyle bir command sıraya koyulursa hangi Handler çalışacak onu IRequestHandler olduğunu belirtiyoruz. Hem çalışacağımız command'i hemde dönüş tipimizi belirtiyoruz.

        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
        {
            // IRequestHandler implement edilmesi gerekir.

            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageRules _programmingLanguageRules;

            public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageRules programmingLanguageRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageRules = programmingLanguageRules;
            }

            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                var programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id); // id'yi ve bütün değişkenleri alır

                _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage); // yollanan id boş mu diye kontrol sağlaması lazım

                await _programmingLanguageRules.ProgrammingLanguageConNotBeDuplicatedWhenUpdated(request.Id, request.Name);



                _mapper.Map(request, programmingLanguage); // Request'in verilerini programmingLanguage1'e maple ( Benim verdiğim değişkenleri tablodaki verilerle maple )

                var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

                var updatedProgrammingLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

                return updatedProgrammingLanguageDto;
            }
        }
    }
}
