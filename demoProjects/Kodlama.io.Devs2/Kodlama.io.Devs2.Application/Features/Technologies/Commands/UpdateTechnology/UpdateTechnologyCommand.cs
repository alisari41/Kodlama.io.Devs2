using AutoMapper;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules;
using Kodlama.io.Devs2.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs2.Application.Features.Technologies.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int Id { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyRules _technologyRules;
        private readonly ProgrammingLanguageRules _programmingLanguageRules;

        public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyRules technologyRules, ProgrammingLanguageRules programmingLanguageRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyRules = technologyRules;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            var technology = await _technologyRepository.GetAsync(x => x.Id == request.Id); // Buna ilerde ihtiyacımız olabilir. Çünkü ilerde belirli alanları alır diğer alanları almazsam buradan dönmesini sağlayabilirim.

            await _technologyRules.TechnologyShouldExistWhenRequested(request.Id);
            await _technologyRules.TechnologyNameConNotBeDuplicatedWhenUpdated(request.Id, request.Name);
            await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            _mapper.Map(request,technology);
            var updatedTechnology = await _technologyRepository.UpdateAsync(technology);
            var updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

            return updatedTechnologyDto;
        }
    }
}
