using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public abstract class BaseMap<T> where T : BaseModel
{
    protected void ConfigureBaseMap(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("id");

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp")
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .HasColumnType("timestamp")
            .HasColumnName("modified_at")
            .IsRequired(false);

        builder.Property(x => x.RemovedAt)
            .HasColumnType("timestamp")
            .HasColumnName("removed_at")
            .IsRequired(false);
    }
}
