using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Student
    {

        public int Id { get; private set; }
    
        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        // ICollection<Group> to be added

        public Student()
        { }

        
        public Student(int id, string firstname, string lastname)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
