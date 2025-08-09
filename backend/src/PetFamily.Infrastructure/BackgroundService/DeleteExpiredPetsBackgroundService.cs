using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.BackgroundServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.BackgroundService
{
    internal class DeleteExpiredPetsBackgroundService 
    {
        //private readonly ILogger<DeleteExpiredPetsBackgroundService> _logger;
        //private readonly IServiceScopeFactory _scopeFactory;

        //public DeleteExpiredPetsBackgroundService(
        //    ILogger<DeleteExpiredPetsBackgroundService> logger,
        //    IServiceScopeFactory scopeFactory)
        //{
        //    _logger = logger;
        //    _scopeFactory = scopeFactory;
        //}

        //protected override async Task ExecuteAsyne(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("DeleteExpiredPetsBackgroundService is started");

        //    while (!cancellationToken.IsCancellationRequested)
        //    {
        //        await using var scope = _scopeFactory.CreateAsyncScope();

        //        var deleteExpiredPetsService = scope.ServiceProvider
        //            .GetRequiredService<DeleteExpiredPetsService>();

        //        _logger.LogInformation("DeleteExpiredPetsBackgroundService is working");

        //        await deleteExpiredPetsService.Process(cancellationToken);

        //        await Task.Delay(
        //            TimeSpan.FromHours(Constants.DELETE_EXPIRED_PETS_SERVICE_REDUCTION_HOURS),
        //            cancellationToken);
        //    }
        //}
    }
}
