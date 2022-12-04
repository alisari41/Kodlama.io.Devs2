using AutoMapper;
using _ProgrammingLanguage = Kodlama.io.Devs2.Domain.Entities.ProgrammingLanguage; // Porje Klasörü adı ile aynı olduğu için uzun uzun yazmak yerine bir değişkene atadım
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Models;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.UpdateProgrammingLanguage;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Profiles
{
    public class MappingProfiles : Profile
    {
        // AutoMapper'in Profile sınıfından kalıtım alınır.

        // Mapleme profilleri yazılır
        public MappingProfiles()
        {
            // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef


            //_ProgrammingLanguage işlemini using te tanımladım

            #region Create
            CreateMap<_ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap(); // ReverseMap() iki türlüde mapleme yapmayı 
            CreateMap<_ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
            #endregion

            #region Get List
            CreateMap<IPaginate<_ProgrammingLanguage>, ProgrammingLanguageListModel>().ReverseMap(); // ProgrammingLanguageListModel sınıfı IPaginate sınıfıyla Maplenir
            CreateMap<_ProgrammingLanguage, ProgrammingLanguageDto>().ReverseMap();
            #endregion

            #region Get By Id
            CreateMap<_ProgrammingLanguage,ProgrammingLanguageGetByIdDto>().ReverseMap();
            #endregion

            #region Update
            CreateMap<_ProgrammingLanguage, UpdatedProgrammingLanguageDto>().ReverseMap(); 
            CreateMap<_ProgrammingLanguage, UpdateProgrammingLanguageCommand>().ReverseMap();
            #endregion

        }

    }
}
