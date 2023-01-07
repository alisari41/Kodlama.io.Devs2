﻿using Core.Application.Requests;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetByIdOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet("getList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest }; // Bu yeni kullanımdır eski hali aşağıdaki gibidir.
                                                                                                         // GetListBrandQuery getListBrandQuery = new GetListBrandQuery();
                                                                                                         // getListBrandQuery.PageRequest = pageRequest;

            var result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }

        [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
        public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQuery getByIdOperationClaimQuery) // route'daki Id ile GetByIdProgrammingLanguageQuery Id işlemini mapleme yapacak. Id yazılımları aynı olmak zorunda 
        {
            var result = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(result);
        }
    }
}
