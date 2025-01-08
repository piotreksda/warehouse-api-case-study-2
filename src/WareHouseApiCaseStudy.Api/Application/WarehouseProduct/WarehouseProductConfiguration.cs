using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WareHouseApiCaseStudy.Api.Application.WarehouseProduct;

public class WarehouseProductConfiguration : IEntityTypeConfiguration<WarehouseProduct>
{
    public void Configure(EntityTypeBuilder<WarehouseProduct> builder)
    {
        builder.HasKey(wp => new { wp.WarehouseId, wp.ProductId }); // Composite key

        builder.HasOne(wp => wp.Warehouse)
            .WithMany(w => w.WarehouseProducts)
            .HasForeignKey(wp => wp.WarehouseId);

        builder.HasOne(wp => wp.Product)
            .WithMany(p => p.WarehouseProducts)
            .HasForeignKey(wp => wp.ProductId);
    }
}