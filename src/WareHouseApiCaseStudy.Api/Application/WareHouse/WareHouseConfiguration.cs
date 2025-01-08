using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WareHouseApiCaseStudy.Api.Application.WareHouse;

public class WareHouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(w => w.WarehouseProducts)
            .WithOne(wp => wp.Warehouse)
            .HasForeignKey(wp => wp.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}