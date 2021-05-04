using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class TestStatisticRepository : EfCoreRepository<TestStatistic>, ITestStatisticRepository
    {
        public TestStatisticRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
