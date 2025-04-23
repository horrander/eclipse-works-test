using System;
using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.DbAdapter.Mappings;

public class ProjectMap : BaseMap<Project>, IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        ConfigureBaseMap(builder);

        builder.ToTable("projects");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("title")
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("description")
            .HasColumnType("varchar(500)");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("user_id")
            .HasColumnType("uuid");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Tasks)
            .WithOne()
            .HasForeignKey(x => x.ProjectId);
    }
}
