using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa pitanja koja mogu da se nadju na nekom testu
    /// </summary>
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
        /// <summary>
        /// Tip pitanja
        /// </summary>
        public QuestionType Type { get; private set; }
        /// <summary>
        /// Maksimalan broj poena koji se moze ostvariti na tom pitnju u zavisnosti od tipa mogucih odgovora
        /// </summary>
        public byte QuestionScore { get; private set; }

        protected Question()
        {
            AnswerOptions = new List<AnswerOption>();
        }

        /// <summary>
        /// Konstruktor pitanja u zavisnosti od tipa odgovora
        /// Tip odgovora moze biti: Free text, Yes/No tip ili Multiple choice
        /// Free Text jedno polje za tekstualni odgovor, Yes/No dve moguce opcije, a Mutiple choice proizvoljan broj opcija za odgovor
        /// </summary>
        /// <param name="questionText"></param>
        /// <param name="answerOptions"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Question(string questionText, ICollection<AnswerOption> answerOptions)
        {
            QuestionText = questionText;
            AnswerOptions = answerOptions;
            if (answerOptions == null || answerOptions.Count == 0)
                throw new ArgumentNullException($"{answerOptions}");
            if (answerOptions.Count == 1)
                Type = QuestionType.FreeText;
            else if (answerOptions.Count == 2)
                Type = QuestionType.YesNo;
            else
                Type = QuestionType.MultipleChoice;
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
