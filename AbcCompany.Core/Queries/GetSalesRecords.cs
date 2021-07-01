using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace AbcCompany.Core.Queries
{
    public static class GetSalesRecords
    {
        public class Query : IRequest<QueryResult>
        {
            private int pageSize = 10;
            private readonly int maxAmount = 100;

            public int Page { get; set; } = 1;
            

            public int PageSize
            {
                get { return pageSize; }
                set
                {
                    if(value > 0)
                    pageSize = (value > maxAmount) ? maxAmount : value;
                }
            }

            public string CountryCode { get; set; }
            public string RegionCode { get; set; }
            public int? CityCode { get; set; }
            public DateTime? DateOfSale { get; set; }


            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IDbConnection _dbConnection;

                public Handler(IDbConnection dbConnection)
                {
                    _dbConnection = dbConnection;
                }

                public async Task<QueryResult> Handle(Query query, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var procedure = "[dbo].[spSearch_Sales_Records]";
                    var dapperResult = await _dbConnection
                        .QueryMultipleAsync(procedure,
                        new { offset = (query.Page - 1) * query.PageSize, pageSize = query.PageSize, countryCode = query.CountryCode, regionCode = query.RegionCode, cityCode = query.CityCode, dateOfSale = query.DateOfSale  }, 
                        commandType: CommandType.StoredProcedure);

                    var records = dapperResult.Read<SalesRecordDto>().AsList();

                    result.Data.AddRange(records);

                    result.TotalCount = dapperResult.ReadFirst<int>();
                    //result.TotalCount = 500;

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public int TotalCount { get; set; }
            public List<SalesRecordDto> Data { get; } = new List<SalesRecordDto>();
        }

        public class SalesRecordDto
        {
            public string ProductName { get; set; }
            public string CustomerName { get; set; }
            public DateTime DateOfSale { get; set; }
            public int Quantity { get; set; }
            public decimal Total { get; set; }

            public decimal UnitPrice => Total / Quantity;
        }
    }
}
