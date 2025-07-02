using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApplicationsController : ControllerBase
    {
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanApplicationsController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanApplicationDto>>> GetLoanApplications()
        {
            var loanApplications = await _loanApplicationService.GetAllLoanApplicationsAsync();
            return Ok(loanApplications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanApplicationDto>> GetLoanApplication(int id)
        {
            var loanApplication = await _loanApplicationService.GetLoanApplicationByIdAsync(id);
            if (loanApplication == null) return NotFound();
            return Ok(loanApplication);
        }

        [HttpPost]
        public async Task<IActionResult> AddLoanApplication([FromBody] LoanApplicationDto loanApplicationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _loanApplicationService.AddLoanApplicationAsync(loanApplicationDto);
            return CreatedAtAction(nameof(GetLoanApplication), new { id = loanApplicationDto.ApplicationID }, loanApplicationDto);
        }
    }
}
