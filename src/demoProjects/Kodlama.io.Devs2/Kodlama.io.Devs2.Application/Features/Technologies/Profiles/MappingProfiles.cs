using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.Technologies.Models;
using Kodlama.io.Devs2.Domain.Entities;
using Kodlama.io.Devs2.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs2.Application.Features.Technologies.Commands.CreateTechnology;
using Kodlama.io.Devs2.Application.Features.Technologies.Commands.UpdateTechnology;
using Kodlama.io.Devs2.Application.Features.Technologies.Commands.DeleteTecnology;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef

        #region İlişkili Tabloları map işlemi yapılması gerekir

        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        // ModelListDto içerisindeki BrandName değişkenini Model sınıfı içersinde Brand'in içindeki Name'den oku
        CreateMap<Technology, TechnologyListDto>()
                        .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))
                        // .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))     Mesela Başka alanlarıda bu şekilde MAPleyebiliriz
                        .ReverseMap(); // ProgrammingLanguageName'i map işlemi yapamayacağı için biz verdik
        #endregion

        CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
        CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
        CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
        CreateMap<Technology, DeleteTecnologyCommand>().ReverseMap();
        #endregion

    }
}
