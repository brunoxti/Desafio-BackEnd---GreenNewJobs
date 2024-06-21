using Microsoft.AspNetCore.Mvc;
using GreenNewJobs.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GreenNewJobs.Application.UseCases.OrdersUseCases.AcceptOrder;
using GreenNewJobs.Application.UseCases.OrdersUseCases.CreateOrder;
using GreenNewJobs.Application.UseCases.OrdersUseCases.DeliverOrder;
using GreenNewJobs.Application.UseCases.OrdersUseCases.UpdateOrder;
using GreenNewJobs.Application.UseCases.OrdersUseCases.GetAllOrders;
using GreenNewJobs.Application.UseCases.OrdersUseCases.GetOrderById;

namespace GreenNewJobs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        private readonly CreateOrderUseCase _createUseCase;
        private readonly GetOrderByIdUseCase _getByIdUseCase;
        private readonly GetAllOrdersUseCase _getAllUseCase;
        private readonly UpdateOrderUseCase _updateUseCase;
        private readonly AcceptOrderUseCase _acceptOrderUseCase;
        private readonly DeliverOrderUseCase _deliverOrderUseCase;
        private readonly IDeliveryPersonRepository _driverRepository;

        public OrdersController(
            CreateOrderUseCase createUseCase,
            GetOrderByIdUseCase getByIdUseCase,
            GetAllOrdersUseCase getAllUseCase,
            UpdateOrderUseCase updateUseCase,
            AcceptOrderUseCase acceptOrderUseCase,
            DeliverOrderUseCase deliverOrderUseCase,
            IDeliveryPersonRepository driverRepository)
        {
            _createUseCase = createUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _acceptOrderUseCase = acceptOrderUseCase;
            _deliverOrderUseCase = deliverOrderUseCase;
            _driverRepository = driverRepository;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderInput command, CancellationToken cancellationToken)
        {
            var result = await _createUseCase.ExecuteAsync(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _getByIdUseCase.ExecuteAsync(new GetOrderByIdInput { Id = id }, cancellationToken);
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
        public async Task<IActionResult> Put(Guid id, UpdateOrderInput command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Errors = new[] { "Order ID does not match" } });
            }

            var result = await _updateUseCase.ExecuteAsync(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return NoContent();
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptOrder(AcceptOrderInput command, CancellationToken cancellationToken)
        {
            var result = await _acceptOrderUseCase.ExecuteAsync(command, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }

        [HttpPost("deliver")]
        public async Task<IActionResult> DeliverOrder(DeliverOrderInput input, CancellationToken cancellationToken)
        {
            var result = await _deliverOrderUseCase.ExecuteAsync(input, cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("notified-drivers/{orderId}")]
        public async Task<IActionResult> GetNotifiedDeliveryPersons(Guid orderId, CancellationToken cancellationToken)
        {
            var drivers = await _driverRepository.GetDeliveryPersonsNotifiedForOrderAsync(orderId);
            if (drivers == null || !drivers.Any())
            {
                return NotFound(new { Errors = new[] { "No drivers were notified for this order" } });
            }
            return Ok(drivers);
        }
    }
}
