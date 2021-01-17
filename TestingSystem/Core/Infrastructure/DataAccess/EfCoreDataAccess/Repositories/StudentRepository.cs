using Common.EfCoreDataAccess;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories
{
    public class StudentRepository : EfCoreRepository<Student>, IStudentRepository
    {
        public StudentRepository(EfCoreDbContext context) : base(context)
        {
        }
    }
}
