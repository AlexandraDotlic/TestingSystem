using Authentication.Domain.Entities;
using Common.EfCoreDataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Infrastructure.DataAccess.EfCoreDataAccess
{
    public class AuthenticationEfCoreDbContext : IdentityDbContext<User>
    {
        public AuthenticationEfCoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
