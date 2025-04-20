using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public class TaskItemMap : BaseMap<TaskItem>, IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        ConfigureBaseMap(builder);

        builder.ToTable("tasks");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("title")
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(500)");

        builder.Property(x => x.DueDate)
            .IsRequired()
            .HasColumnName("due_date")
            .HasColumnType("timestamp");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnName("status")
            .HasColumnType("integer");

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasColumnName("priority")
            .HasColumnType("integer");

        builder.Property(x => x.ProjectId)
            .IsRequired()
            .HasColumnName("project_id")
            .HasColumnType("uuid");
    }
}
