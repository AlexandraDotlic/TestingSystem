using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa statistika
    /// </summary>
    public class TestStatistic
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// Id testa
        /// </summary>
        public short TestId { get; private set; }
        /// <summary>
        /// Naziv testa
        /// </summary>
        public string TestTitle { get; private set; }
        /// <summary>
        /// Id ispitivaca
        /// </summary>
        public int ExaminerId { get; private set; }
        /// <summary>
        /// Procenat studenata koji su polozili test
        /// </summary>
        public int PercentageOfStudentsWhoPassedTheTest { get; private set; }
        /// <summary>
        /// Broj studenata koji su polagali test
        /// </summary>
        public int NumberOfStudentsWhoTookTheTest { get; private set; }
        /// <summary>
        /// Datum kreiranja statistike
        /// </summary>
        public DateTime Date { get; private set; }
        public TestStatistic()
        {
                
        }

        public TestStatistic(
            short testId, 
            string testTitle,
            int examinerId, 
            int percentageOfStudentsWhoPassedTheTest, 
            int numberOfStudentsWhoTookTheTest
            )
        {
            TestId = testId;
            TestTitle = testTitle;
            ExaminerId = examinerId;
            PercentageOfStudentsWhoPassedTheTest = percentageOfStudentsWhoPassedTheTest;
            NumberOfStudentsWhoTookTheTest = numberOfStudentsWhoTookTheTest;
            Date = DateTime.Now;
        }
    }
}
