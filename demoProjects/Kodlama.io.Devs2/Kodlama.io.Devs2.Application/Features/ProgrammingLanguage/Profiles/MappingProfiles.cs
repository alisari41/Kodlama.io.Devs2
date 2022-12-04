using AutoMapper;
//using _ProgrammingLanguage = Kodlama.io.Devs2.Domain.Entities.ProgrammingLanguage; // Porje Klasörü adı ile aynı olduğu için uzun uzun yazmak yerine bir değişkene atadım
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Profiles
{
    public class MappingProfiles : Profile
    {
        // AutoMapper'in Profile sınıfından kalıtım alınır.

        // Mapleme profilleri yazılır
        public MappingProfiles()
        {
            // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef
            CreateMap<Domain.Entities.ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap(); // ReverseMap() iki türlüde mapleme yapmayı 
            CreateMap<Domain.Entities.ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();

        }

    }
}
