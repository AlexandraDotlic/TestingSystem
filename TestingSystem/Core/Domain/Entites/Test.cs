using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <summary>
        /// Indikator da li je test aktivan za polaganje
        /// </summary>
        public bool IsActive { get; private set; }
        /// <summary>
        /// Ukupan rezultat testa
        /// </summary>
        public int TestScore { get; private set; }
        /// <summary>
        /// Krajnji datum vazenja testa
        /// </summary>
        public DateTime? EndDate { get; private set; }
        /// <summary>
        /// Pocetni datum vazenja testa
        /// </summary>
        public DateTime StartDate { get; private set; }


        public Test(Examiner examiner, string title, DateTime startDate, DateTime? endDate = null)
        {
            Title = title;
            Examiner = examiner;
            ExaminerId = examiner.Id;
            Questions = new List<Question>();
            if (startDate >= DateTime.Now
                && (endDate == null || endDate >= startDate)
                )
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
        }

        private Test()
        {
            Questions = new List<Question>();
            IsActive = true;
        }
        public void AddTestQuestion(Question testQuestion)
        {
            Questions.Add(testQuestion);
            TestScore += testQuestion.QuestionScore;
        }

    }
}
