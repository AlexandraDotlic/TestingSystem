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

        public async Task<short> CreateTest(string title, DateTime startDate, DateTime? endDate = null)
        {
            Test test = new Test(title, startDate, endDate);
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

        public async Task AddQuestionToTest(short testId, string questionText, ICollection<Tuple<string, bool>> answerOptionTuples)
        {
            Test test = await UnitOfWork.TestRepository.GetFirstOrDefaultWithIncludes(t => t.Id == testId, t => t.Questions);
            if (test == null)
            {
                throw new ArgumentNullException($"{nameof(Test)} with Id {testId} not exist");
            }
            ICollection<AnswerOption> answerOptionsCollection = new List<AnswerOption>();
            if(answerOptionTuples == null || answerOptionTuples.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(answerOptionTuples)}");
            }
            foreach (var option in answerOptionTuples)
            {
                answerOptionsCollection.Add(new AnswerOption(option.Item1, option.Item2));
            }
            Question question = new Question(questionText, answerOptionsCollection);
            test.AddTestQuestion(question);
            await UnitOfWork.TestRepository.Update(test);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
