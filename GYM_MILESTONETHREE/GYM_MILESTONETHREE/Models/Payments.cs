using GYM_MILESTONETHREE.Enums;
using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Payments
    {
        [Key]
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }

        public DateTime dateTime { get; set; }
        public Decimal Amount {  get; set; }

        public PaymentType PaymentType { get; set; }

        //payment Description If others 
        public string? Description {  get; set; }
       
       // public DateTime? ExpiryDate { get; set; }
        public int receiverId {  get; set; }


    }
}
