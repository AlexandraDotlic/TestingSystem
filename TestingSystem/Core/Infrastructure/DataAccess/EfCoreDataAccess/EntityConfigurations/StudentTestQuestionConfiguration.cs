using Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.EntityConfigurations
{
    public class StudentTestQuestionConfiguration : IEntityTypeConfiguration<StudentTestQuestion>
    {
        public void Configure(EntityTypeBuilder<StudentTestQuestion> builder)
        {

          
            
        }
    }
}
