using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites.Examiner
{
    class Examiner
    {
        public int Id { get; private set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public Examiner()
        { }

        public Examiner(int id, string firstname, string lastname)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
