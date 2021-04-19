using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entites
{
    public class StudentTestQuestion
    {
        public int Id { get; private set; }
        public int QuestionId { get; private set; }
        public StudentTest StudentTest { get; private set; }
        public ICollection<StudentTestQuestionResponse> Responses { get; private set; }
        public int Score { get; private set; }

        public StudentTestQuestion() 
        {
            Responses = new List<StudentTestQuestionResponse>();
        }

        public StudentTestQuestion(int questionId, ICollection<StudentTestQuestionResponse> responses)
        {
            QuestionId = questionId;
            Responses = responses;
            Score = Responses == null || Responses.Count == 0 ? 0 : Responses.Sum(r => r.ResponseScore); 
        }

    }
}
