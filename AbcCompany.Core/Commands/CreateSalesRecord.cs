using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;

namespace AbcCompany.Core.Commands
{
    public static class CreateSalesRecord
    {
        public class Command : IRequest<CommandResult>
        {
            public string CustomerName { get; set; }
            public int CityCode { get; set; }
            public DateTime DateOfSale { get; set; }
            public int ProductID { get; set; }
            public int Quantity { get; set; }


            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IDbConnection _dbConnection;
                private readonly ILogger _logger;

                public Handler(IDbConnection dbConnection, ILogger<Handler> logger)
                {
                    _dbConnection = dbConnection;
                    _logger = logger;
                }

                public async Task<CommandResult> Handle(Command command, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    try
                    {
                        var procedure = "[dbo].[spCreate_Sales_Record]";
                        var dapperResult = await _dbConnection.ExecuteAsync(procedure, command, commandType: CommandType.StoredProcedure);

                        result.RecordId = dapperResult;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error while adding sales record!");
                        result.Errors.Add("An Error Occured while processing the request");
                    }
                    

                    return result;
                }
            }
        }

        public class CommandResult
        {
            public bool Success => Errors.Count == 0;
            public List<string> Errors { get; } = new List<string>();
            public int RecordId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.CityCode).NotEmpty();
                RuleFor(x => x.DateOfSale).NotEmpty();
                RuleFor(x => x.ProductID).NotEmpty();
                RuleFor(x => x.Quantity).GreaterThan(0);
            }
        }
    }
}
