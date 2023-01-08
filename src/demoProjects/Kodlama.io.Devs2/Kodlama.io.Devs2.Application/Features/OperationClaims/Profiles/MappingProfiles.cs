using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Models;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef

        #region Get List
        CreateMap<IPaginate<OperationClaim>, OperationClaimsListModel>().ReverseMap();  // OperationClaimsListModel sınıfı IPaginate sınıfıyla Maplenir
        CreateMap<OperationClaim, OperationClaimDto>().ReverseMap(); // ReverseMap() iki türlüde mapleme yapmayı 
        #endregion

        #region Get By Id
        CreateMap<OperationClaim, OperationClaimGetByIdDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
        CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<OperationClaim, DeletedOperationClaimDto>().ReverseMap();
        CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
        #endregion

    }

}
