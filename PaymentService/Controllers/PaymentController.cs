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
            var success = Random.Shared.Next(0, 2) == 1;

            if (!success)
                return BadRequest(new { success = false, message = "Payment failed" });

            return Ok(new { success = true, message = "Payment successful" });
        }
    }
}
