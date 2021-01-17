using Common.EfCoreDataAccess;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.EfCoreDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess
{
    public class CoreEfCoreUnitOfWork : EfCoreUnitOfWork, ICoreUnitOfWork
    {
        public CoreEfCoreUnitOfWork(CoreEfCoreDbContext context) : base(context)
        {
            ExaminerRepository = new ExaminerRepository(context);
            StudentRepository = new StudentRepository(context);
            GroupRepository = new GroupRepository(context);
            TestRepository = new TestRepository(context);
            QuestionRepository = new QuestionRepository(context);
        }

        public IExaminerRepository ExaminerRepository { get; }

        public IStudentRepository StudentRepository { get; }

        public IGroupRepository GroupRepository { get; }

        public ITestRepository TestRepository { get; }

        public IQuestionRepository QuestionRepository { get; }
    }
}
