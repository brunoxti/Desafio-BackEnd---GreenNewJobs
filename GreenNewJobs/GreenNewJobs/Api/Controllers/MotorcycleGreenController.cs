using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.CreateMotorcycle;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.UpdateMotorcycle;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.DeleteMotocycle;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotocycleById;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotorcycleByPlate;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetAllMotorcycle;

namespace GreenNewJobs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleGreenController : ControllerBase
    {
        private readonly CreateMotorcycleGreenUseCase _createMotorcycleGreenUseCase;
        private readonly GetMotorcycleGreenByIdUseCase _getMotorcycleGreenByIdUseCase;
        private readonly GetAllMotorcycleGreenUseCase _getAllMotorcycleGreenUseCase;
        private readonly GetMotorcycleGreenByPlateUseCase _getMotorcycleGreenByPlateUseCase;
        private readonly UpdateMotorcycleGreenUseCase _updateMotorcycleGreenUseCase;
        private readonly DeleteMotorcycleGreenUseCase _deleteMotorcycleGreenUseCase;
        private readonly IValidator<CreateMotorcycleGreenInput> _createMotorcycleGreenValidator;
        private readonly IValidator<UpdateMotorcycleGreenInput> _updateMotorcycleGreenValidator;

        public MotorcycleGreenController(
            CreateMotorcycleGreenUseCase createMotorcycleGreenUseCase,
            GetMotorcycleGreenByIdUseCase getMotorcycleGreenByIdUseCase,
            GetAllMotorcycleGreenUseCase getAllMotorcycleGreenUseCase,
            GetMotorcycleGreenByPlateUseCase getMotorcycleGreenByPlateUseCase,
            UpdateMotorcycleGreenUseCase updateMotorcycleGreenUseCase,
            DeleteMotorcycleGreenUseCase deleteMotorcycleGreenUseCase,
            IValidator<CreateMotorcycleGreenInput> createMotorcycleGreenValidator,
            IValidator<UpdateMotorcycleGreenInput> updateMotorcycleGreenValidator)
        {
            _createMotorcycleGreenUseCase = createMotorcycleGreenUseCase;
            _getMotorcycleGreenByIdUseCase = getMotorcycleGreenByIdUseCase;
            _getAllMotorcycleGreenUseCase = getAllMotorcycleGreenUseCase;
            _getMotorcycleGreenByPlateUseCase = getMotorcycleGreenByPlateUseCase;
            _updateMotorcycleGreenUseCase = updateMotorcycleGreenUseCase;
            _deleteMotorcycleGreenUseCase = deleteMotorcycleGreenUseCase;
            _createMotorcycleGreenValidator = createMotorcycleGreenValidator;
            _updateMotorcycleGreenValidator = updateMotorcycleGreenValidator;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateMotorcycleGreenInput input)
        {
            var result = await _createMotorcycleGreenUseCase.ExecuteAsync(input);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
            }

            return BadRequest(new { result.Errors });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateMotorcycleGreenInput input, CancellationToken cancellationToken)
        {
            var validationResult = await _updateMotorcycleGreenValidator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            var result = await _updateMotorcycleGreenUseCase.ExecuteAsync(id, input, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { Errors = result.Errors });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _deleteMotorcycleGreenUseCase.ExecuteAsync(new DeleteMotorcycleGreenInput { Id = id }, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { result.Errors });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _getMotorcycleGreenByIdUseCase.ExecuteAsync(new GetMotorcycleGreenByIdInput { Id = id }, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(new { Errors = result.Errors });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getAllMotorcycleGreenUseCase.ExecuteAsync(cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { Errors = result.Errors });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetByPlate([FromQuery] string plate, CancellationToken cancellationToken)
        {
            var result = await _getMotorcycleGreenByPlateUseCase.ExecuteAsync(new GetMotorcycleGreenByPlateInput { Plate = plate }, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { Errors = result.Errors });
        }
    }
}
