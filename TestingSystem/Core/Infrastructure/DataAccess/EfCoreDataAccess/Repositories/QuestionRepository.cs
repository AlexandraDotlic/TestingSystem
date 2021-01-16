using Common.EfCoreDataAccess;
using Core.Domain.Entites.Questions;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class QuestionRepository : EfCoreRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
