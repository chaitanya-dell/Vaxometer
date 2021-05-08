using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vaxometer.MongoOperations.DbSettings;

namespace Vaxometer.MongoOperations.IocExtensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMongoOperations(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<VexoDatabaseSettings>(configuration.GetSection(nameof(VexoDatabaseSettings)));
            services.AddSingleton<IVexoDatabaseSettings>(x => x.GetRequiredService<IOptions<VexoDatabaseSettings>>().Value);
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            

            return services;
        }
    }
}
