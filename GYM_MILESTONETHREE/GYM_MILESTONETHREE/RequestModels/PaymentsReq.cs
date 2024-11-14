using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.Models;
using NuGet.Protocol.Plugins;

namespace GYM_MILESTONETHREE.RequestModels
{
    public class PaymentsReq
    {
        public int memberid { get; set; }
        public Decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        //payment Description If others 
        public string Description { get; set; }
        public int recievedBy {  get; set; }
        
        public int quantity {  get; set; }
    }
}
