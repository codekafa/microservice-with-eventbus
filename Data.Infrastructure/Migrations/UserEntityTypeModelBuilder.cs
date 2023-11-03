using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Infrastructure.Migrations
{
    public class UserEntityTypeModelBuilder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(200);
        }
    }
}
