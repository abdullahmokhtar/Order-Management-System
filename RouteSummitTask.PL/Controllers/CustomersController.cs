using AutoMapper;

namespace RouteSummitTask.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        public async Task<ActionResult> Register(CustomerRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customer = await _userManager.FindByNameAsync(model.Name);
            if (customer != null)
                return BadRequest($"{model.Name} is already exists");
            var appUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Name,
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (!result.Succeeded)
                return StatusCode(500, "Something went wrong while creating new customer");

            var roleResult = await _userManager.AddToRoleAsync(appUser, "Customer");

            if (!roleResult.Succeeded)
                return StatusCode(500, "Something went wrong");

            var newCustomer = new Customer
            {
                AppUserId = appUser.Id,
                Email = model.Email,
                Name = model.Name,
            };

            await _unitOfWork.CustomerRepository.Add(newCustomer);
            if (await _unitOfWork.CompleteAsync() < 1)
                return StatusCode(500, "Something went wrong");

            return Ok(_mapper.Map<CustomerDto>(newCustomer));
        }

        [Authorize]
        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetOrders(int customerId)
        {
            if (customerId < 1) return BadRequest();
            var customerOrders = await _unitOfWork.OrderRepository.GetAllCustomerOrder(customerId);

            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(customerOrders));
        }
    }
}
