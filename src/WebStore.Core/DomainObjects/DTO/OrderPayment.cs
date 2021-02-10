using System;

namespace WebStore.Core.DomainObjects.DTO
{
    public class OrderPayment
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardVerificationCode { get; set; }
    }
}
