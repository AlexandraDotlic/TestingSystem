using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class StudentGroup
    {
        public int GroupId { get; private set; }
        public int StudentId { get; private set; }
        public string StudentResponse { get; private set; }

        public StudentGroup() { }

        public StudentGroup(string studentResponse) : this()
        {
            StudentResponse = studentResponse;
        }

    }
}
