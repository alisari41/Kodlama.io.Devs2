using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Kodlama.io.Devs2.Application.Features.Technologies.Commands.CreateTechnology;
using Kodlama.io.Devs2.Application.Features.Technologies.Queries.GetListTechnologyByDynamic;
using Kodlama.io.Devs2.Application.Features.Technologies.Queries.GetListTechnolojy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpGet("get-list")]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTecnologyQuery = new() { PageRequest = pageRequest }; // Bu yeni kullanımdır eski hali aşağıdaki gibidir.
                                                                                                // GetListBrandQuery getListBrandQuery = new GetListBrandQuery();
                                                                                                // getListBrandQuery.PageRequest = pageRequest;

            var result = await Mediator.Send(getListTecnologyQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı
        {
            GetListTechnologyByDynamicQuery getListTechnologyByDynamicQuery= new() { PageRequest = pageRequest, Dynamic = dynamic };

            var result = await Mediator.Send(getListTechnologyByDynamicQuery);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            var result = await Mediator.Send(createTechnologyCommand);
            return Created("", result);
        }
    }
}
