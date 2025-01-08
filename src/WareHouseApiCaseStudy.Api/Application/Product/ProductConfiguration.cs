using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WareHouseApiCaseStudy.Api.Application.Product;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(p => p.WarehouseProducts)
            .WithOne(wp => wp.Product)
            .HasForeignKey(wp => wp.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}