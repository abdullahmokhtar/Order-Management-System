using AutoMapper;
using RouteSummitTask.BLL.Dtos;
using RouteSummitTask.PL.Helper;
using System.Security.Claims;


namespace RouteSummitTask.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IOrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderService = orderService;
        }

        private string? GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<int?> GetCustomerId()
        {
            var userId = GetUserId();
            var customer = await _unitOfWork.CustomerRepository.GetByUserId(userId);
            return customer == null ? null : customer.Id;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerId = await GetCustomerId();

            Order order;
            try
            {
                order = await _orderService.CreateOrder(customerId.Value, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            if (order == null)
                return BadRequest();

            await _unitOfWork.OrderRepository.Add(order);
            if (await _unitOfWork.CompleteAsync() < 1)
                return StatusCode(500, "Something went wrong");

            Invoice invoice = new()
            {
                InvoiceDate = DateTime.Now,
                OrderId = order.Id,
                TotalAmount = order.TotalAmount
            };

            await _unitOfWork.InvoiceRepository.Add(invoice);

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            var customerId = await GetCustomerId();
            var order = await _unitOfWork.OrderRepository.
                GetCustomerOrderDetailsAsync(orderId, customerId);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllOrders()
            => Ok(await _unitOfWork.OrderRepository.GetAllAsync());

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId:int}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int orderId, OrderStatusDro orderStatus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (orderId < 1)
                return BadRequest("Please provide a valid id");
            if (!Enum.IsDefined(typeof(Status), orderStatus.OrderStatus))
                return BadRequest("Please provide a valid Status");
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null) return NotFound($"There is no order with this id = {orderId}");
            order.Status = orderStatus.OrderStatus;
            if (await _unitOfWork.CompleteAsync() < 1)
                return StatusCode(500, "Something went wrong while change the status of order");

            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(order.CustomerId);

            var email = new Email
            {
                Title = "Order Status",
                Body = $"Your Order Status changed to {order.Status}",
                To = customer.Email
            };

            EmailSettigns.SendEmail(email);
            return Ok(order);
        }
    }
}
