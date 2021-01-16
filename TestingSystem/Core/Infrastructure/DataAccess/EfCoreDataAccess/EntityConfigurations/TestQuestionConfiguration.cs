using Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.EntityConfigurations
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.HasKey(sg => new { sg.TestId, sg.QuestionId });       
            builder.HasOne(sg => sg.Question)
                .WithMany(s => s.TestQuestions)
                .HasForeignKey(sg => sg.QuestionId);
            builder.HasOne(sg => sg.Test)
                .WithMany(s => s.TestQuestions)
                .HasForeignKey(sg => sg.TestId);

        }
    }
}
