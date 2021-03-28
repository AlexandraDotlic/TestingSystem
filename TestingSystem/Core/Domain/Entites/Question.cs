using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Question
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; protected set; }
        /// <summary>
        /// Tekst pitanja
        /// </summary>
        public string QuestionText { get; protected set; }
        /// <summary>
        /// Ponudjeni odgovori
        /// </summary>
        public ICollection<AnswerOption> AnswerOptions { get; private set; }
        /// <summary>
        /// Test kome pitanje pripada
        /// </summary>
        public Test Test { get; private set; }
        public short TestId { get; private set; }

        public byte QuestionScore { get; private set; }

        protected Question()
        {
            AnswerOptions = new List<AnswerOption>();
        }

        public Question(string questionText, ICollection<AnswerOption> answerOptions)
        {
            QuestionText = questionText;
            AnswerOptions = answerOptions;
            foreach (var option in AnswerOptions)
            {
                if(option.IsCorrect == true)
                {
                    QuestionScore++;
                }
                
            }
        }
    }
}
