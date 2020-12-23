using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Examiner
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

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
