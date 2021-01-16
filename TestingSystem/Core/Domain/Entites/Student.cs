using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Student
    {
        public int Id { get; private set; }    
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public ICollection<StudentGroup> StudentGroups { get; private set; }
        public Student()
        {
            StudentGroups = new List<StudentGroup>();
        }

        
        public Student(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
            StudentGroups = new List<StudentGroup>();
        }
    }
}
