using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa koja predstavlja vezu izmedju testa koji je polagao student i pitanja na testu
    /// </summary>
    public class StudentTestQuestion
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Id pitanja sa testa
        /// </summary>
        public int QuestionId { get; private set; }
        public StudentTest StudentTest { get; private set; }
        /// <summary>
        /// Odgovori na pitanje
        /// </summary>
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
