using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public class TaskCommentMap : BaseMap<TaskComment>, IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        ConfigureBaseMap(builder);

        builder.ToTable("task_comments");

        builder.Property(x => x.Comment)
            .IsRequired()
            .HasColumnName("comment")
            .HasColumnType("varchar(500)");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("user_id")
            .HasColumnType("uuid");

        builder.Property(x => x.TaskItemId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uuid");
    }
}
