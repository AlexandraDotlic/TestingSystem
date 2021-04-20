using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class TestStatistic
    {
        public long Id { get; private set; }
        public int TestId { get; private set; }
        public int ExaminerId { get; private set; }
        public int PercentageOfStudentsWhoPassedTheTest { get; private set; }
        public int NumberOfStudentsWhoTookTheTest { get; private set; }
        public DateTime? FromDate { get; private set; }
        public DateTime? ToDate { get; private set; }
        public TestStatistic()
        {
                
        }

        public TestStatistic(
            int testId, 
            int examinerId, 
            int percentageOfStudentsWhoPassedTheTest, 
            int numberOfStudentsWhoTookTheTest,
            DateTime? fromDate = null, 
            DateTime? toDate = null
            )
        {
            TestId = testId;
            ExaminerId = examinerId;
            PercentageOfStudentsWhoPassedTheTest = percentageOfStudentsWhoPassedTheTest;
            NumberOfStudentsWhoTookTheTest = numberOfStudentsWhoTookTheTest;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
