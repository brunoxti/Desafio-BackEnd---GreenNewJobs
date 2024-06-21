using GreenNewJobs.Application.UseCases;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.CreateDeliveryPerson;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.DeleteDeliveryPerson;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetAllDeliveryPersons;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetDeliveryPersonById;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.UpdateDeliveryPerson;
using GreenNewJobs.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenNewJobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonsController : ControllerBase
    {
        private readonly CreateDeliveryPersonUseCase _createDeliveryPersonUseCase;
        private readonly GetDeliveryPersonByIdUseCase _getDeliveryPersonByIdUseCase;
        private readonly GetAllDeliveryPersonsUseCase _getAllDeliveryPersonsUseCase;
        private readonly UpdateDeliveryPersonUseCase _updateDeliveryPersonUseCase;
        private readonly DeleteDeliveryPersonUseCase _deleteDeliveryPersonUseCase;
        private readonly DeliveryPersonService _driverService;

        public DeliveryPersonsController(
            CreateDeliveryPersonUseCase createDeliveryPersonUseCase,
            GetDeliveryPersonByIdUseCase getDeliveryPersonByIdUseCase,
            GetAllDeliveryPersonsUseCase getAllDeliveryPersonsUseCase,
            UpdateDeliveryPersonUseCase updateDeliveryPersonUseCase,
            DeleteDeliveryPersonUseCase deleteDeliveryPersonUseCase,
            DeliveryPersonService driverService)
        {
            _createDeliveryPersonUseCase = createDeliveryPersonUseCase;
            _getDeliveryPersonByIdUseCase = getDeliveryPersonByIdUseCase;
            _getAllDeliveryPersonsUseCase = getAllDeliveryPersonsUseCase;
            _updateDeliveryPersonUseCase = updateDeliveryPersonUseCase;
            _deleteDeliveryPersonUseCase = deleteDeliveryPersonUseCase;
            _driverService = driverService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryPersonInput command, CancellationToken cancellationToken)
        {
            var result = await _createDeliveryPersonUseCase.ExecuteAsync(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Result<GetDeliveryPersonByIdOutput>>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _getDeliveryPersonByIdUseCase.ExecuteAsync(new GetDeliveryPersonByIdInput { Id = id }, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(new { Errors = result.Errors });
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Result<IEnumerable<GetAllDeliveryPersonsOutput>>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getAllDeliveryPersonsUseCase.ExecuteAsync(cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { Errors = result.Errors });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDeliveryPersonInput input, CancellationToken cancellationToken)
        {
            input.Id = id;
            await _updateDeliveryPersonUseCase.ExecuteAsync(input, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _deleteDeliveryPersonUseCase.ExecuteAsync(new DeleteDeliveryPersonInput { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpPost("{id}/upload-cnh")]
        [Authorize]
        public async Task<IActionResult> UploadCNH(Guid id, [FromForm] IFormFile cnhImage, CancellationToken cancellationToken)
        {
            if (cnhImage == null || cnhImage.Length == 0)
            {
                return BadRequest(new { Errors = new[] { "CNH image is required." } });
            }

            var result = await _getDeliveryPersonByIdUseCase.ExecuteAsync(new GetDeliveryPersonByIdInput { Id = id }, cancellationToken);

            if (!result.IsSuccess)
            {
                return NotFound(new { Errors = result.Errors });
            }

            var deliveryPerson = result.Value;

            var cnhImagePath = await _driverService.UploadCNHImageAsync(cnhImage, deliveryPerson.CNHNumber);

            deliveryPerson.UpdateCNHImagePath(cnhImagePath);
            var updateResult = await _updateDeliveryPersonUseCase.ExecuteAsync(new UpdateDeliveryPersonInput
            {
                Id = id,
                Name = deliveryPerson.Name,
                CNPJ = deliveryPerson.CNPJ,
                BirthDate = deliveryPerson.BirthDate,
                CNHNumber = deliveryPerson.CNHNumber,
                CNHType = deliveryPerson.CNHType,
                CNHImagePath = cnhImagePath
            }, cancellationToken);

            if (!updateResult.IsSuccess)
            {
                return BadRequest(new { Errors = updateResult.Errors });
            }

            return Ok(new { Message = "CNH image uploaded successfully" });
        }
    }
}
