using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;

public class RentalPlanInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public RentalPlanInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var rentalPlanRepository = scope.ServiceProvider.GetRequiredService<IRentalPlanRepository>();

        var existingPlans = await rentalPlanRepository.GetAllAsync();
        if (existingPlans.Any()) return;

        var plans = new List<RentalPlan>
        {
            new RentalPlan("Green Seven Plan", 7, 30m),
            new RentalPlan("Green Fifteen Plan", 15, 28m),
            new RentalPlan("Green Thirty Plan", 30, 22m)
        };

        foreach (var plan in plans)
        {
            await rentalPlanRepository.AddAsync(plan);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
