using Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Repositories
{
    public interface ICoreUnitOfWork: IUnitOfWork
    {
        public IExaminerRepository ExaminerRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IGroupRepository GroupRepository { get; }
        public ITestRepository TestRepository { get; }
        public IQuestionRepository QuestionRepository { get; }

    }
}
