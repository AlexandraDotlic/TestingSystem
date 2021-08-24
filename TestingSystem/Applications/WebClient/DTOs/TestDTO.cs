using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class TestDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember] 
        public DateTime StartDate { get; set; }
        [DataMember] 
        public int ExaminerId { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int TestScore { get; set; }

        public TestDTO()
        {

        }

        public TestDTO(int id, string title, int examinerId, DateTime startDate, bool isActive, int score)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            ExaminerId = examinerId;
            IsActive = isActive;
            TestScore = score;

        }
    }


}