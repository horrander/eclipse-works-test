using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public abstract class BaseMap<T> where T : BaseModel
{
    protected void ConfigureBaseMap(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("TIMESTAMP")
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .HasColumnType("TIMESTAMP")
            .HasColumnName("modified_at")
            .IsRequired(false);

        builder.Property(x => x.RemovedAt)
            .HasColumnType("TIMESTAMP")
            .HasColumnName("removed_at")
            .IsRequired(false);
    }
}
