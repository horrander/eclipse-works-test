using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public class UserMap : BaseMap<User>, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureBaseMap(builder);

        builder.ToTable("User");

        builder.Property(x => x.Email);
    }
}
