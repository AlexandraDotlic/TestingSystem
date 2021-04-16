using Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.EntityConfigurations
{
    public class StudentTestConfiguration : IEntityTypeConfiguration<StudentTest>
    {
        public void Configure(EntityTypeBuilder<StudentTest> builder)
        {
            builder.HasKey(st => new { st.StudentId, st.TestId });
            builder.HasOne(st => st.Student)
                .WithMany(s => s.StudentTests)
                .HasForeignKey(st => st.StudentId);
            builder.HasOne(st => st.Test)
                .WithMany(t => t.StudentTests)
                .HasForeignKey(st => st.TestId);
        }
    }
}
