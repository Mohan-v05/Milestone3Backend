using System.Drawing;

namespace GYM_MILESTONETHREE.ResponseModels
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }

        public string PayeeName { get; set; }

        public string PayerName { get; set; }

        public DateTime PaymentDate { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public decimal Balance {  get; set; }
    }
}
