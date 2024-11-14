using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Addpayment(PaymentsReq req)
        {
            try
            {
                var data = await _paymentService.AddPayment(req);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
               
                var payments = await _paymentService.GetAllPaymentsAsync();

               
                if (payments == null || !payments.Any())
                {
                    return NotFound("No payments found.");
                }

               
                return Ok(payments);
            }
            catch (Exception ex)
            {
                
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(Guid paymentId)
        {
            try
            {
                var payment = await _paymentService.GetPaymentByIdAsync(paymentId);

                if (payment == null)
                {
                    return NotFound($"Payment with ID {paymentId} not found.");
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


    }
}
