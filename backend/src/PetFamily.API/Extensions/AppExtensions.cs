using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure.DbContexts;



namespace PetFamily.API.Extensions
{
    public static class AppExtensions
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            //применить миграции (update)
            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
