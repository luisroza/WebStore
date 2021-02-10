using System;
using WebStore.Core.DomainObjects;

namespace WebStore.Payments.Business
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        //EF Relation
        public Payment Payment { get; set; }
    }
}
