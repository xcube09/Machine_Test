using AbcCompany.Core.Commands;
using AbcCompany.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompany.Web.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("search")]
        public async Task<ActionResult<GetSalesRecords.QueryResult>> Search([FromQuery]int? page, [FromQuery]int? pageSize, [FromBody]GetSalesRecords.Query query)
        {
            if (page.HasValue)
                query.Page = page.Value;

            if (pageSize.HasValue)
                query.PageSize = pageSize.Value;

            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<CreateSalesRecord.CommandResult>> Post([FromBody]CreateSalesRecord.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("post-get")]
        public async Task<ActionResult<GetCreateSalesRecordPostData.QueryResult>> PostGet()
        {
            return await _mediator.Send(new GetCreateSalesRecordPostData.Query());
        }
    }
}
