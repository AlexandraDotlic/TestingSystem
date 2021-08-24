using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class TestRepository : EfCoreRepository<Test>, ITestRepository
    {
        public TestRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
