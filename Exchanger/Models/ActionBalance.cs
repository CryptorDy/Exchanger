namespace Exchanger.Models
{
    public class ActionBalance
    {
        public Guid UserId { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Amount { get; set; }
    }
}
