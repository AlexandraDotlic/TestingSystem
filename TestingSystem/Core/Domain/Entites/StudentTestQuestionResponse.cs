using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class StudentTestQuestionResponse
    {
        public int Id { get; private set; }
        public string Response { get; private set; }
        public int ResponseScore { get; private set; }
        public int StudentTestQuestionId { get; private set; }
        public StudentTestQuestion StudentTestQuestion { get; private set; }
    }
}
