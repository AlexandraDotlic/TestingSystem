using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.DTOs
{

    public class TestDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int ExaminerId { get; set; }
        public bool IsActive { get; set; }
        public int TestScore { get; set; }
        public DateTime StartDate { get; set; }

        public TestDTO()
        {

        }

        public TestDTO(int id, string title, int examinerId, DateTime startDate, bool isActive, int score)
        {
            Id = id;
            Title = title;
            ExaminerId = examinerId;
            StartDate = startDate;
            IsActive = isActive;
            TestScore = score;
        }
    }


}


