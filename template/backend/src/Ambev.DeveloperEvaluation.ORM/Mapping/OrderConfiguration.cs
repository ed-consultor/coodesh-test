using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(o => o.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(o => o.SaleDate).IsRequired();
            builder.Property(o => o.Customer).IsRequired().HasMaxLength(100);
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.Property(o => o.Branch).IsRequired().HasMaxLength(100);
            builder.Property(o => o.IsCancelled).IsRequired();

            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
