using AutoMapper;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules;
using Kodlama.io.Devs2.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs2.Application.Features.Technologies.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    // Bir tanede Handlerımız var yani böyle bir command sıraya koyulursa hangi Handler çalışacak onu IRequestHandler olduğunu belirtiyoruz. Hem çalışacağımız command'i hemde dönüş tipimizi belirtiyoruz.

    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyRules _technologyRules;
        private readonly ProgrammingLanguageRules _programmingLanguageRules;

        public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyRules technologyRules, ProgrammingLanguageRules programmingLanguageRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyRules = technologyRules;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyRules.TechnologyNameCanNotBeDuplicatedWhenIserted(request.Name);

            await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);


            var mappedTechnology = _mapper.Map<Technology>(request);
            var createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
            var createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
            return createdTechnologyDto;
        }
    }


}
