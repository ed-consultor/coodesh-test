using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(oi => oi.Product).IsRequired().HasMaxLength(100);
            builder.Property(oi => oi.Quantity).IsRequired();
            builder.Property(oi => oi.UnitPrice).IsRequired();
            builder.Property(oi => oi.Discount).IsRequired();
            builder.Property(oi => oi.TotalItemAmount).IsRequired();
        }    
    }
}
