using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class StudentTestQuestionRepository : EfCoreRepository<StudentTestQuestion>, IStudentTestQuestionRepository
    {
        public StudentTestQuestionRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
