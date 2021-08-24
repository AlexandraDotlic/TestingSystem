using Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.EntityConfigurations
{
    public class StudentTestQuestionResponseConfiguration : IEntityTypeConfiguration<StudentTestQuestionResponse>
    {
        public void Configure(EntityTypeBuilder<StudentTestQuestionResponse> builder)
        {
            builder.Property(stqr => stqr.Response).HasMaxLength(256);
        }
    }
}
