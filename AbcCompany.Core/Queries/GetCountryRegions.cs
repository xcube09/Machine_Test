using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AbcCompany.Core.Queries
{
    public static class GetCountryRegions
    {
        public class Query : IRequest<QueryResult>
        {
            public Query(string countryCode)
            {
                CountryCode = countryCode;
            }

            public string CountryCode { get; }

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

                    var procedure = "[dbo].[spGet_Regions_By_Country_Code]";
                    var dapperResult = await _dbConnection.QueryAsync<RegionDto>(procedure,
                        new { countryCode = request.CountryCode },
                        commandType: CommandType.StoredProcedure);

                    var regions = dapperResult.AsList();

                    result.Regions.AddRange(regions);

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<RegionDto> Regions { get; } = new List<RegionDto>();
        }

        public class RegionDto
        {
            public string RegionCode { get; set; }
            public string CountryCode { get; set; }
            public string RegionName { get; set; }
        }
    }
}
