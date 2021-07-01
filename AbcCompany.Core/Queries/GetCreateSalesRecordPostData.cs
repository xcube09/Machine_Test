using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbcCompany.Core.Queries
{
    public static class GetCreateSalesRecordPostData
    {
        public class Query : IRequest<QueryResult>
        {
            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IDbConnection _dbConnection;

                public Handler(IDbConnection dbConnection)
                {
                    _dbConnection = dbConnection;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var procedure = "[dbo].[spGet_Products_And_Countries]";
                    var dapperResult = await _dbConnection
                        .QueryMultipleAsync(procedure, commandType: CommandType.StoredProcedure);

                    var products = dapperResult.Read<ProductDto>().AsList();
                    var countries = dapperResult.Read<CountryDto>().AsList();

                    result.Products.AddRange(products);
                    result.Countries.AddRange(countries);

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<ProductDto> Products { get; } = new List<ProductDto>();
            public List<CountryDto> Countries { get; } = new List<CountryDto>();
        }


        public class ProductDto
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
        }

        public class CountryDto
        {
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
        }
    }
}
