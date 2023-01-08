using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetByIdOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Queries.GetListOperationClaimByDynamic;
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

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı.
        {
            GetListOperationClaimByDynamicQuery getListOperationClaimByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };

            var result = await Mediator.Send(getListOperationClaimByDynamicQuery);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            var result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            var result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }
    }
}
