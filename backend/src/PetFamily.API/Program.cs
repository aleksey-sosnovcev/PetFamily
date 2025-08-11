using PetFamily.Application;
using PetFamily.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PetFamily.API.Validation;
using PetFamily.API.Extensions;
using PetFamily.API.Middlewares;
using Serilog;
using Serilog.Events;




namespace PetFamily.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .Enrich.WithThreadId()
                .Enrich.WithEnvironmentName()
                .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                                ?? throw new ArgumentNullException("Seq"))
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .CreateLogger();


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSerilog();

            builder.Services
                .AddInfrastructure()
                .AddApplication();

            //builder.Services.AddFluentValidationAutoValidation(configuration =>
            //{
            //    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
            //});

            var app = builder.Build();

            app.UseExceptionMiddleware();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                await app.ApplyMigration();
            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
