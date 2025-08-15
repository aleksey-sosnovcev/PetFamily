using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Providers;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IVolunteerRepository, VolunteersRepository>();

            services.AddMinio(configuration);

            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMinio(options =>
            {
                services.Configure<MinioOptions>(
                    configuration.GetSection(MinioOptions.MINIO));

                var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                                   ?? throw new ApplicationException("Missing minio configuration");

                options.WithEndpoint(minioOptions.Endpoint);

                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSSL);
            });

            services.AddScoped<IFileProvider, MinioProvider>();

            return services;
        }
    }
}
