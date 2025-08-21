using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.VolunteerOperations;
using PetFamily.Application.VolunteerOperations.Create;
using PetFamily.Application.VolunteerOperations.Delete.HardDelete;
using PetFamily.Application.VolunteerOperations.Delete.SoftDelete;
using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;
using PetFamily.Application.VolunteerOperations.Update.MainInfo;
using PetFamily.Application.VolunteerOperations.Update.SocialNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add;
using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Delete;
using PetFamily.Application.VolunteerOperations.PetOperations.Add;
using PetFamily.Application.VolunteerOperations.PetOperations.Move;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateDetailsHandler>();
            services.AddScoped<UpdateSocialNetworksHendler>();
            services.AddScoped<HardDeleteVolunteerHandler>();
            services.AddScoped<SoftDeleteVolunteerHandler>();
            services.AddScoped<AddPetHandler>();
            services.AddScoped<DeletePetFileHandler>();
            services.AddScoped<AddPetFileHandler>();
            services.AddScoped<MovePetHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
