using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.Sales.Domain;

namespace WebStore.Sales.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .HasDefaultValueSql("NEXT VALUE FOR OrderSequence");

            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.OrderLines)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder.ToTable("Order");
        }
    }
}
