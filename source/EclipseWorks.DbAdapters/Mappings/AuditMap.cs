using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public class AuditMap : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("audits");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("id");

        builder.Property(x => x.ModifiedAt)
            .IsRequired()
            .HasColumnName("modified_at")
            .HasColumnType("timestamp");

        builder.Property(x => x.Property)
            .IsRequired()
            .HasColumnName("property")
            .HasColumnType("varchar(50)");

        builder.Property(x => x.OldValue)
            .IsRequired()
            .HasColumnName("old_value")
            .HasColumnType("varchar(500)");

        builder.Property(x => x.NewValue)
            .IsRequired()
            .HasColumnName("new_value")
            .HasColumnType("varchar(500)");

        builder.Property(x => x.TaskItemId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uuid");
    }
}
