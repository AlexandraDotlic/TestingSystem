using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.DTOs
{
    public class StudentTestScoreDTO
    {
        public int StudentId { get; set; }
        public short TestId { get; set; }
        public int StudentTestScore { get; set; }
        public int TotalTestScore { get; set; }
    }
}
