using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{
    public class SampleDbContextFactory : IDesignTimeDbContextFactory<CoreEfCoreDbContext>
    {
        public CoreEfCoreDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<CoreEfCoreDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=test;Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options;

            return new CoreEfCoreDbContext(options);
        }
    }

}
