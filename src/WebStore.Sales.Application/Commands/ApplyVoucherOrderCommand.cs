using FluentValidation;
using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public string VoucherCode { get; private set; }

        public ApplyVoucherOrderCommand(Guid customerId, string voucherCode)
        {
            CustomerId = customerId;
            VoucherCode = voucherCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer ID");

            RuleFor(c => c.VoucherCode)
                .NotEmpty()
                .WithMessage("Voucher code cannot be empty");
        }
    }
}
