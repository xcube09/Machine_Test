using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AbcCompany.Core.Queries
{
    public static class GetRegionCities
    {
        public class Query : IRequest<QueryResult>
        {
            public Query(string regionCode)
            {
                RegionCode = regionCode;
            }

            public string RegionCode { get; }

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

                    var procedure = "[dbo].[spGet_Cities_By_Region_Code]";
                    var dapperResult = await _dbConnection.QueryAsync<CityDto>(procedure,
                        new { regionCode = request.RegionCode },
                        commandType: CommandType.StoredProcedure);

                    var cities = dapperResult.AsList();

                    result.Cities.AddRange(cities);

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<CityDto> Cities { get; } = new List<CityDto>();
        }

        public class CityDto
        {
            public int CityCode { get; set; }
            public string RegionCode { get; set; }
            public string CityName { get; set; }
        }
    }
}
