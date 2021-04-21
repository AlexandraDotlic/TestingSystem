using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class TestStatisticDTO
    {
        public TestStatisticDTO(
            long id, 
            short testId,
            string testTitle,
            int percentageOfStudentsWhoPassedTheTest, 
            int numberOfStudentsWhoTookTheTest, 
            DateTime date)
        {
            Id = id;
            TestId = testId;
            TestTitle = testTitle;
            PercentageOfStudentsWhoPassedTheTest = percentageOfStudentsWhoPassedTheTest;
            NumberOfStudentsWhoTookTheTest = numberOfStudentsWhoTookTheTest;
            Date = date;
        }

        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public short TestId { get; set; }
        [DataMember]
        public string TestTitle { get; set; }
        [DataMember]
        public int PercentageOfStudentsWhoPassedTheTest { get; set; }
        [DataMember]
        public int NumberOfStudentsWhoTookTheTest { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
    }
}
