using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class AnswerOptionRepository : EfCoreRepository<AnswerOption>, IAnswerOptionRepository
    {
        public AnswerOptionRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
