using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.DTOs
{
    public class TestStatisticDTO
    {
        public TestStatisticDTO(
            long id, 
            short testId,
            string testTitle,
            int examinerId, 
            int percentageOfStudentsWhoPassedTheTest, 
            int numberOfStudentsWhoTookTheTest, 
            DateTime date)
        {
            Id = id;
            TestId = testId;
            TestTitle = testTitle;
            ExaminerId = examinerId;
            PercentageOfStudentsWhoPassedTheTest = percentageOfStudentsWhoPassedTheTest;
            NumberOfStudentsWhoTookTheTest = numberOfStudentsWhoTookTheTest;
            Date = date;
        }

        public long Id { get; set; }
        public short TestId { get; set; }
        public string TestTitle { get; set; }
        public int ExaminerId { get; set; }
        public int PercentageOfStudentsWhoPassedTheTest { get; set; }
        public int NumberOfStudentsWhoTookTheTest { get; set; }
        public DateTime Date { get; set; }
    }
}
