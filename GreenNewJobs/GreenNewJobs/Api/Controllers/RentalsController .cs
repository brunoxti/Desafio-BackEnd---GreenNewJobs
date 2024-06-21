using GreenNewJobs.Application.UseCases.RentalsUseCases.CreateRental;
using GreenNewJobs.Application.UseCases.RentalsUseCases.GetRentalById;
using GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRental;
using GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRentalReturnDate;
using Microsoft.AspNetCore.Mvc;
using GreenNewJobs.Application.UseCases.RentalsUseCases.GetAllRentals;
using Microsoft.AspNetCore.Authorization;

namespace GreenNewJobs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly CreateRentalUseCase _createUseCase;
        private readonly GetRentalByIdUseCase _getByIdUseCase;
        private readonly GetAllRentalsUseCase _getAllUseCase;
        private readonly UpdateRentalUseCase _updateUseCase;
        private readonly UpdateRentalReturnDateUseCase _updateReturnDateUseCase;

        public RentalsController(
            CreateRentalUseCase createUseCase,
            GetRentalByIdUseCase getByIdUseCase,
            GetAllRentalsUseCase getAllUseCase,
            UpdateRentalUseCase updateUseCase,
            UpdateRentalReturnDateUseCase updateReturnDateUseCase)
        {
            _createUseCase = createUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _updateReturnDateUseCase = updateReturnDateUseCase;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateRentalInput command, CancellationToken cancellationToken)
        {
            var result = await _createUseCase.ExecuteAsync(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _getByIdUseCase.ExecuteAsync(new GetRentalByIdInput { Id = id }, cancellationToken);
            if (!result.IsSuccess)
            {
                return NotFound(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getAllUseCase.ExecuteAsync(cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateRentalInput command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Errors = new[] { "Id in URL does not match Id in command" } });
            }

            var result = await _updateUseCase.ExecuteAsync(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return NoContent();
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> UpdateReturnDate(Guid id, [FromBody] UpdateRentalReturnDateInput command,CancellationToken cancellationToken)
        {
            if (id != command.RentalId)
            {
                return BadRequest(new { Errors = new[] { "Id in URL does not match RentalId in command" } });
            }

            var result = await _updateReturnDateUseCase.ExecuteAsync(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(new { TotalCost = result.Value });
        }
    }
}
