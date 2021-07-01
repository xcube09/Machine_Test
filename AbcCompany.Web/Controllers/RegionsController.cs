using AbcCompany.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbcCompany.Web.Controllers
{
    [ApiController]
    [Route("api/regions")]
    public class RegionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{regionCode}/cities")]
        public async Task<ActionResult<GetRegionCities.QueryResult>> GetRegionCities(string regionCode)
        {
            return await _mediator.Send(new GetRegionCities.Query(regionCode));
        }
    }
}
