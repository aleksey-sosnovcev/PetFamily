using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.VolunteerOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPagination;
using PetFamily.Application.VolunteerOperations.Commands.Create;
using PetFamily.Application.VolunteerOperations.Commands.Delete.HardDelete;
using PetFamily.Application.VolunteerOperations.Commands.Delete.SoftDelete;
using PetFamily.Application.VolunteerOperations.Commands.PetOperations.Add;
using PetFamily.Application.VolunteerOperations.Commands.PetOperations.Move;
using PetFamily.Application.VolunteerOperations.Commands.PetOperations.PetFiles.Add;
using PetFamily.Application.VolunteerOperations.Commands.PetOperations.PetFiles.Delete;
using PetFamily.Application.VolunteerOperations.Commands.Update.DetailsInfo;
using PetFamily.Application.VolunteerOperations.Commands.Update.MainInfo;
using PetFamily.Application.VolunteerOperations.Commands.Update.SocialNetworks;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddCommands()
                .AddQueries()
                .AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
                        .AddClasses(classes => classes
                        .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime());
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
                        .AddClasses(classes => classes
                        .AssignableTo(typeof(IQueryHandler<,>)))
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime());
        }
    }
}
