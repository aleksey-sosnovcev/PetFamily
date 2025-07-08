<<<<<<< Updated upstream
=======
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers;
using PetFamily.Application;
using PetFamily.Infrastructure.Repositories;
using PetFamily.Infrastructure;


>>>>>>> Stashed changes
namespace PetFamily.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

<<<<<<< Updated upstream
=======
            builder.Services
                .AddInfrastructure()
                .AddApplication();
>>>>>>> Stashed changes

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
