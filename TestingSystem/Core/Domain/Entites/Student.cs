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

        // ICollection<Group> to be added

        public Student()
        { }

        
        public Student(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
