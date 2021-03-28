using System;
using System.Collections.Generic;

namespace Core.Domain.Entites
{
    public class Test
    {
        /// <summary>
        /// Id
        /// </summary>
        public short Id { get; private set; }
        /// <summary>
        /// Naziv
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Kolekcija pitanja
        /// </summary>
        public ICollection<Question> Questions { get; private set; }
      
        public int ExaminerId { get; private set; }
        public Examiner Examiner { get; private set; }
        public short TestScore { get; private set; }

        public Test(string title) 
        {
            Title = title;
            Questions = new List<Question>();
        }

        private Test()
        {
            Questions = new List<Question>();
        }
        public void AddTestQuestion(Question testQuestion)
        {
            Questions.Add(testQuestion);
            TestScore += testQuestion.QuestionScore; 
        }

    }
}
