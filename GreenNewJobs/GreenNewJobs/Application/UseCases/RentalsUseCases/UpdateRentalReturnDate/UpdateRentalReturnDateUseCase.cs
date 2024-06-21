using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRentalReturnDate;
using FluentValidation.Results;
using GreenNewJobs.Application.UseCases;

public class UpdateRentalReturnDateUseCase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IRentalPlanRepository _rentalPlanRepository;
    private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;
    private readonly IDeliveryPersonRepository _driverRepository;

    public UpdateRentalReturnDateUseCase(
        IRentalRepository rentalRepository,
        IRentalPlanRepository rentalPlanRepository,
        IMotorcycleGreenRepository motorcycleGreenRepository,
        IDeliveryPersonRepository driverRepository)
    {
        _rentalRepository = rentalRepository;
        _rentalPlanRepository = rentalPlanRepository;
        _motorcycleGreenRepository = motorcycleGreenRepository;
        _driverRepository = driverRepository;
    }

    public async Task<Result<decimal>> ExecuteAsync(UpdateRentalReturnDateInput command, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(command.RentalId);
        if (rental == null)
        {
            return Result<decimal>.Failure(new List<ValidationFailure>
            {
                new ValidationFailure("RentalId", "Rental not found")
            });
        }

        var rentalPlan = await _rentalPlanRepository.GetByIdAsync(rental.PlanId);
        if (rentalPlan == null)
        {
            return Result<decimal>.Failure(new List<ValidationFailure>
            {
                new ValidationFailure("PlanId", "Invalid rental plan")
            });
        }

        // Inicializa o custo total com o custo original do aluguel
        decimal totalCost = rental.Cost;

        // Verifica se a data de término informada é anterior à data de término prevista
        if (command.EndDate < rental.ExpectedEndDate)
        {
            // Calcula os dias restantes
            var remainingDays = (rental.ExpectedEndDate.Date - command.EndDate.Date).Days;

            // Determina a taxa de penalidade com base na duração do plano
            decimal penaltyRate = rentalPlan.DurationDays switch
            {
                7 => 0.20m,  // 20% para planos de 7 dias
                15 => 0.40m, // 40% para planos de 15 dias
                30 => 0.60m, // 60% para planos de 30 dias
                _ => 0m      // Nenhuma penalidade para outros casos
            };

            // Subtrai o valor das diárias não efetivadas do custo total
            totalCost -= remainingDays * rentalPlan.CostPerDay;

            // Adiciona a multa ao custo total
            totalCost += remainingDays * rentalPlan.CostPerDay * penaltyRate;
        }
        // Verifica se a data de término informada é posterior à data de término prevista
        else if (command.EndDate > rental.ExpectedEndDate)
        {
            // Calcula os dias adicionais
            var additionalDays = (command.EndDate - rental.ExpectedEndDate).Days;

            // Adiciona um valor fixo por cada diária adicional ao custo total
            totalCost += additionalDays * 50m;
        }


        rental.UpdateReturnDate(command.EndDate, totalCost);
        await _rentalRepository.UpdateAsync(rental);

        var motorcycleGreen = await _motorcycleGreenRepository.GetByIdAsync(rental.MotorcycleGreenId);
        motorcycleGreen.SetAvailability(true);
        await _motorcycleGreenRepository.UpdateAsync(motorcycleGreen);

        var driver = await _driverRepository.GetByIdAsync(rental.DeliveryPersonId);
        if (driver != null)
        {
            driver.EndRental();
            await _driverRepository.UpdateAsync(driver);
        }

        return Result<decimal>.SuccessResponse(totalCost);
    }
}
