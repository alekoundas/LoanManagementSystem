using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("loan/{loanId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByLoanId(int loanId)
        {
            var payments = await _paymentService.GetPaymentsByLoanIdAsync(loanId);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _paymentService.AddPaymentAsync(paymentDto);
            return CreatedAtAction(nameof(GetPaymentsByLoanId), new { loanId = paymentDto.LoanID }, paymentDto);
        }
    }
}
