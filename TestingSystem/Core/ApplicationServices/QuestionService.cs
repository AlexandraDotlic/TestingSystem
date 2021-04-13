using Core.ApplicationServices.DTOs;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ICollection<QuestionDTO>> GetAllQuestionsForTest(short testId)
        {
            IReadOnlyCollection<Question> questions = await UnitOfWork.QuestionRepository.SearchByWithIncludes(q => q.TestId == testId, q => q.AnswerOptions);
           
            List<QuestionDTO> questionDTOs = questions == null || questions.Count == 0 
                ? null 
                : questions.Select(q => new QuestionDTO(q.Id, q.QuestionText, q.TestId, q.QuestionScore, q.AnswerOptions)).ToList();
            return questionDTOs;
        }
    }
}
