using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class TestStatistic
    {
        public long Id { get; private set; }
        public short TestId { get; private set; }
        public string TestTitle { get; private set; }
        public int ExaminerId { get; private set; }
        public int PercentageOfStudentsWhoPassedTheTest { get; private set; }
        public int NumberOfStudentsWhoTookTheTest { get; private set; }
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
