using GreenNewJobs.Application.UseCases.GetAllRentalPlans;
using GreenNewJobs.Application.UseCases.RentalPlanUseCases.CreateRentalPlan;
using GreenNewJobs.Application.UseCases.RentalPlanUseCases.GetAllRentalPlans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenNewJobs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalPlansController : ControllerBase
    {
        private readonly CreateRentalPlanUseCase _createRentalPlanUseCase;
        private readonly GetAllRentalPlansUseCase _getAllRentalPlansUseCase;

        public RentalPlansController(
            CreateRentalPlanUseCase createRentalPlanUseCase,
            GetAllRentalPlansUseCase getAllRentalPlansUseCase)
        {
            _createRentalPlanUseCase = createRentalPlanUseCase;
            _getAllRentalPlansUseCase = getAllRentalPlansUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var output = await _getAllRentalPlansUseCase.ExecuteAsync(new GetAllRentalPlansInput(), cancellationToken);
            return Ok(output.RentalPlans);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Post(CreateRentalPlanInput input, CancellationToken cancellationToken)
        {
            var output = await _createRentalPlanUseCase.ExecuteAsync(input, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = output.Id }, output);
        }
    }
}
