using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace AbcCompany.Core.Queries
{
    public static class GetCountries
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

                    var procedure = "[dbo].[spGet_Countries]";
                    var dapperResult = await _dbConnection
                        .QueryAsync<CountryDto>(procedure, commandType: CommandType.StoredProcedure);

                    var countries = dapperResult.AsList();

                    result.Countries.AddRange(countries);

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<CountryDto> Countries { get; } = new List<CountryDto>();
        }

        public class CountryDto
        {
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
        }
    }
}
