using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Payments.Business;

namespace WebStore.Payments.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CardName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.CardNumber)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(p => p.CardExpirationDate)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(p => p.CardVerificationCode)
                .IsRequired()
                .HasColumnType("varchar(4)");

            //1:1 -> Payment:Transaction
            builder.HasOne(p => p.Transaction)
                .WithOne(p => p.Payment);

            builder.ToTable("Payment");
        }
    }
}
