using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("check")]
        public IActionResult CheckPayment()
        {
            return Ok(new { success = true });
        }
    }
}
