using Core.Application.Requests;
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
    }
}
