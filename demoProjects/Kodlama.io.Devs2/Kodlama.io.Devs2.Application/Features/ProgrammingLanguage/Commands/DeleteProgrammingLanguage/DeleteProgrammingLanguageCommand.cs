using AutoMapper;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;
using _ProgrammingLanguage = Kodlama.io.Devs2.Domain.Entities.ProgrammingLanguage;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
    {
        // Son kullanıcının bize göndereceği son dataları içeren yapı
        public int Id { get; set; }

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageRules _programmingLanguageRules;

            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageRules programmingLanguageRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageRules = programmingLanguageRules;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {

                var programmingLanguage = await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.Id); // yollanan id boş mu diye kontrol sağlaması lazım

                #region Yukarıdaki satırda sorgulama yapıldıktan sonra neden MAPleme ("mappedProgrammingLanguage") yapılmadığının açıklaması
                //var mappedProgrammingLanguage = _mapper.Map<_ProgrammingLanguage>(request);
                // Git Hub Link: https://github.com/kodlamaio-projects/nArchitecture.RentACarProject/issues/118#issuecomment-1345567819
                #endregion

                var deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(programmingLanguage);

                var deleteProgrammingLanguageDto = _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

                return deleteProgrammingLanguageDto;

            }


        }
    }
}
