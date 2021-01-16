using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class QuestionRepository : EfCoreRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
