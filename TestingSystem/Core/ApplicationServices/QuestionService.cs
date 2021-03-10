using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class QuestionService
    {
        private readonly ICoreUnitOfWork UnitOfWork;

        public QuestionService(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        
        public async Task<int> CreateQuestion(string questionText, string answer)
        {
            Question question = new Question(questionText, answer);
            await UnitOfWork.QuestionRepository.Insert(question);
            await UnitOfWork.SaveChangesAsync();
            return question.Id;
        }

        public async Task DeleteQuestion(int questionId)
        {
            Question question = await UnitOfWork.QuestionRepository.GetById(questionId);
            if (question == null)
            {
                throw new ArgumentNullException($"{nameof(Question)} with Id {questionId} not exist");
            }
            await UnitOfWork.QuestionRepository.Delete(question);
        }


    }

}
