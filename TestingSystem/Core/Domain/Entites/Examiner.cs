using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Examiner
    {
        public int Id { get; protected set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public Examiner()
        {
        }

        public Examiner(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
