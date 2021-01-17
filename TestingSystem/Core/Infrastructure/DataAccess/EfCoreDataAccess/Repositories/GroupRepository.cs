using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class GroupRepository : EfCoreRepository<Group>, IGroupRepository
    {
        public GroupRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
