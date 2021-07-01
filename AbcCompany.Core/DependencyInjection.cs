using AbcCompany.Core.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompany.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDbConnection>(x => new SqlConnection(config.GetConnectionString("DefaultConnection")));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
