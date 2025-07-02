using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanProductsController : ControllerBase
    {
        private readonly ILoanProductService _loanProductService;

        public LoanProductsController(ILoanProductService loanProductService)
        {
            _loanProductService = loanProductService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanProductDto>>> GetLoanProducts()
        {
            var loanProducts = await _loanProductService.GetAllLoanProductsAsync();
            return Ok(loanProducts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanProductDto>> GetLoanProduct(int id)
        {
            var loanProduct = await _loanProductService.GetLoanProductByIdAsync(id);
            if (loanProduct == null) return NotFound();
            return Ok(loanProduct);
        }
    }
}
