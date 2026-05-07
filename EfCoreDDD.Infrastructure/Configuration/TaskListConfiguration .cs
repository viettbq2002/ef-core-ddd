using EfCoreDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Infrastructure.Configuration
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Tasks)
                .WithOne(x => x.TaskList)
                .HasForeignKey(x => x.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Workspace)
                .WithMany(x => x.Lists)
                .HasForeignKey(x => x.WorkspaceId);
        }

    }
}
