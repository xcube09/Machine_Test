using AbcCompany.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbcCompany.Web.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetCountries.QueryResult>> Get()
        {
            return await _mediator.Send(new GetCountries.Query());
        }


        [HttpGet("{countryCode}/regions")]
        public async Task<ActionResult<GetCountryRegions.QueryResult>> GetCountryRegions(string countryCode)
        {
            return await _mediator.Send(new GetCountryRegions.Query(countryCode));
        }
    }
}
