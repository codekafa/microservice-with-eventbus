using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Infrastructure.Migrations
{
    public class ModelEntityTypeModelBuilder : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Brand).WithOne().HasForeignKey<Brand>(x=> x.Id); 
            builder.Property(x => x.ModelName).IsRequired().HasMaxLength(200);
        }
    }
}
