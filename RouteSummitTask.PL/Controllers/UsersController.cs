namespace RouteSummitTask.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UsersController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
                return BadRequest("This user is already registered");
            AppUser newUser = new() { UserName = model.UserName };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong please try again");

            return Ok($"{model.UserName} has sucessfully registered");
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return Unauthorized("Email or password is invalid");
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Email or password is invalid");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return Ok(
                _tokenService.GenerateToken(user, isAdmin)
            );
        }
    }
}
