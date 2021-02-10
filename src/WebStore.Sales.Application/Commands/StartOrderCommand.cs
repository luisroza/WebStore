using FluentValidation;
using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpirationDate { get; private set; }
        public string CardVerificationCode { get; private set; }

        public StartOrderCommand(Guid customerId, Guid orderId, decimal total, string cardName, string cardNumber, string cardExpirationDate, string cardVerificationCode)
        {
            CustomerId = customerId;
            OrderId = orderId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpirationDate = cardExpirationDate;
            CardVerificationCode = cardVerificationCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer ID");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid order ID");

            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage("Card name not informed");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Card number not informed");

            RuleFor(c => c.CardExpirationDate)
                .NotEmpty()
                .WithMessage("Card expiration date not informed");

            RuleFor(c => c.CardNumber)
                .Length(3, 4)
                .WithMessage("Card verification code incorrect");
        }
    }
}
