using AutoMapper;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules;
using Kodlama.io.Devs2.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>
    {
        // Son kullanıcının bize göndereceği son dataları içeren yapı
        public string Name { get; set; }


        // Bir tanede Handlerımız var yani böyle bir command sıraya koyulursa hangi Handler çalışacak onu IRequestHandler olduğunu belirtiyoruz. Hem çalışacağımız command'i hemde dönüş tipimizi belirtiyoruz.
        public class CreateProgrammingLanguageHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {
            // IRequestHandler implement edilmesi gerekir.
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageRules _programmingLanguageRules;

            public CreateProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageRules programmingLanguageRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageRules = programmingLanguageRules;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageRules.ProgrammingLanguageConNotBeDuplicatedWhenInserted(request.Name);// BusinessRules lerin yazılıyor.

                Domain.Entities.ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<Domain.Entities.ProgrammingLanguage>(request); // mapper kullanarak Parametre olarak gelen "request"'i Brand nesnesine çevir.      
                //Dikkat!!! ProgrammingLanguage>(request); içerisinde (request.name) yazılmaz


                Domain.Entities.ProgrammingLanguage createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage); // repository kullanarak ekleme işlemini gerçekleştirmem gerekiyor     (createdProgrammingLanguage veritabanından dönen ProgrammingLanguage)

                CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage); // Bizim veritabanından gelen nesneyi DTO ya çevirmemiz lazım

                // Mapper bu satır aşağıdaki satırlara eşittir isimlendirmeye göre kendi otomatik eşitler
                //CreatedBrandDto createdBrandDto=new CreatedBrandDto();
                //createdBrandDto.Id = mappedBrand.Id;
                //createdBrandDto.Name = mappedBrand.Name;


                // AutoMapper (2 nesneyi birbirine eşlemeyi sağlayan basit bir kütüphanedir)  bize dönüşüm yapmamızı sağlar mesela
                // Dto -> id, name
                // Brand -> id, name, x
                // Mapper ile isim benzerlikleri ile çevirmeyi sağlar bu sayede veritabanındaki bütün nesneleri kullanıcıya döndürmemiş oluruz

                return createdProgrammingLanguageDto;
            }
        }
    }
}
