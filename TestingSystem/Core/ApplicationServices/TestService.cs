using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class TestService
    {
        private readonly ICoreUnitOfWork UnitOfWork;
        public TestService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<short> CreateTest(string title)
        {
            Test test = new Test(title);
            await UnitOfWork.TestRepository.Insert(test);
            await UnitOfWork.SaveChangesAsync();
            return test.Id;
        }

        public async Task DeleteTest(short testId)
        {
            Test test = await UnitOfWork.TestRepository.GetById(testId);
            if(test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            await UnitOfWork.TestRepository.Delete(test);
            await UnitOfWork.SaveChangesAsync();

        }

        public async Task AddQuestionToTest(short testId, int questionId)
        {
            Test test = await UnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.TestQuestions);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            Question question = await UnitOfWork.QuestionRepository.GetById(questionId);
            TestQuestion testQuestion = new TestQuestion(test, question);
            test.AddTestQuestion(testQuestion);
            await UnitOfWork.TestRepository.Update(test);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
