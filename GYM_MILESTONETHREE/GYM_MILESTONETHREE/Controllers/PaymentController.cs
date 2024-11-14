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
        public async Task<IActionResult>Addpayment(PaymentsReq req)
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

    }
}
