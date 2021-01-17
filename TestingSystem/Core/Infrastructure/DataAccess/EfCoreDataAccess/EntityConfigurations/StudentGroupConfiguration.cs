using Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.EntityConfigurations
{
    public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.HasKey(sg => new { sg.GroupId, sg.StudentId });
            builder.HasOne(sg => sg.Student)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(sg => sg.StudentId);
            builder.HasOne(sg => sg.Group)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(sg => sg.GroupId);
            builder.Property(sg => sg.StudentResponse)
                .HasMaxLength(10000);
        }
    }
}
