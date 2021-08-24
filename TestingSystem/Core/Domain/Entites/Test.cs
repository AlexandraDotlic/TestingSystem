using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa koja predstavlja test
    /// </summary>
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
        /// <summary>
        /// Id ispitivaca
        /// </summary>
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
        /// Pocetni datum vazenja testa
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Kolekcija studenata koji polazu ovaj test
        /// </summary>
        public ICollection<StudentTest> StudentTests { get; private set; }


        /// <summary>
        /// Konsturktor testa koji kreira test od strane examiner-a.
        /// U slucaju da se stavi da je trenutak kreiranja trenutak pocetka test je odmah aktivan.
        /// U suprotnom je kreiran neaktivan test
        /// </summary>
        /// <param name="examiner"></param>
        /// <param name="title"></param>
        /// <param name="startDate"></param>
        public Test(Examiner examiner, string title, DateTime startDate)
        {
            Title = title;
            Examiner = examiner;
            ExaminerId = examiner.Id;
            StartDate = startDate;
            Questions = new List<Question>();
            StudentTests = new List<StudentTest>();
            if (StartDate <= DateTime.Now)
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

        /// <summary>
        /// Metoda koja sluzi za aktiviranje neaktivnog testa
        /// </summary>
        public void Activate() {
            IsActive = true;
        }

        /// <summary>
        /// Metoda koja sluzi za deaktiviranje aktivnog testa
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <summary>
        /// Metoda koja sluzi da promeni pocetak perioda kada test moze da se polaze
        /// </summary>
        /// <param name="startDate"></param>
        public void ChangeStartDate(DateTime startDate) {
            StartDate = startDate;
            if (startDate <= DateTime.Now)
            {
                IsActive = true;
            }
            else 
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// Metoda koja sluzi za dodavanje pitanja u test
        /// </summary>
        /// <param name="testQuestion"></param>
        public void AddQuestion(Question testQuestion)
        {
            Questions.Add(testQuestion);
            TestScore += testQuestion.QuestionScore;
        }

        /// <summary>
        /// Metoda koja sluzi za uklanjanje pitanja iz testa
        /// </summary>
        /// <param name="testQuestion"></param>
        public void RemoveQuestion(Question testQuestion)
        {
            Questions.Remove(testQuestion);
            TestScore -= testQuestion.QuestionScore;
        }

    }
}
