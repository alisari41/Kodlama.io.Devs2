using AutoMapper;
using Kodlama.io.Devs2.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs2.Application.Features.Technologies.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Commands.DeleteTecnology;

public class DeleteTecnologyCommand : IRequest<DeletedTechnologyDto>
{
    public int Id { get; set; }

    public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTecnologyCommand, DeletedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyRules _technologyRules;

        public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyRules technologyRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyRules = technologyRules;
        }

        public async Task<DeletedTechnologyDto> Handle(DeleteTecnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyRules.TechnologyShouldExistWhenRequested(request.Id);

            var mappedTechnology = _mapper.Map<Technology>(request); // Command'i Entity'e maple
            var deletedTechnology = await _technologyRepository.DeleteAsync(mappedTechnology);
            var deleteTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);

            return deleteTechnologyDto;
        }
    }
}
