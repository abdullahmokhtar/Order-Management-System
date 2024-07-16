namespace RouteSummitTask.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        => Ok(await _unitOfWork.ProductRepository.GetAllAsync());


        [HttpGet("{productId:int}")]
        public async Task<ActionResult> GetById(int productId)
        {
            if (productId < 1)
                return BadRequest("please provide valid id");
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null)
                return NotFound("There is no product with this id");
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddProduct(CreateProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            };
            await _unitOfWork.ProductRepository.Add(product);
            if (await _unitOfWork.CompleteAsync() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong please try again");
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{productId:int}")]
        public async Task<ActionResult> UpdateProduct(int productId, Product product)
        {
            if (productId != product.Id)
                return BadRequest("Product ID mismatch");
            if (productId < 1 || !ModelState.IsValid)
                return BadRequest(ModelState);
            var productDb = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (productDb == null)
                return NotFound("There is no product with this id");
            productDb.Name = product.Name;
            productDb.Price = product.Price;
            productDb.Stock = product.Stock;
            if (await _unitOfWork.CompleteAsync() == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong please try again");
            return Ok(product);
        }
    }
}
