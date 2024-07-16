namespace RouteSummitTask.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InvoicesController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpGet("{invoiceId:int}")]
        public async Task<IActionResult> GetInvoiceDetails(int invoiceId)
        {
            if (invoiceId < 1)
                return BadRequest("Please provide a valid id");
            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(invoiceId);
            if (invoice == null)
                return NotFound($"There is no invoice with this id = {invoiceId}");
            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _unitOfWork.InvoiceRepository.GetAllAsync());

    }
}
