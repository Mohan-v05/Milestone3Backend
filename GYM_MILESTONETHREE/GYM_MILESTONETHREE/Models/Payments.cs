using GYM_MILESTONETHREE.Enums;
using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Payments
    {
        [Key]
        public Guid Id { get; set; }

        public Users? Payer { get; set; }

        public int? PayerId { get; set; }

        public Users Payee { get; set; }

        public int  PayeeId { get; set; }

        public DateTime dateTime { get; set; }

        public int Quantity { get; set; }

        public Decimal Amount {  get; set; }

        public PaymentType PaymentType { get; set; }

        public string? Description {  get; set; }
      
    }
}
