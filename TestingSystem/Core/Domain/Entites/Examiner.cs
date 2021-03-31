using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Examiner
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Ime
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// Prezime
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// Kolekcija grupa koje je napravio ispitivac
        /// </summary>
        public ICollection<Group> Groups { get; private set; }
        /// <summary>
        /// Kolekcija testova koje je napravio ispitivac
        /// </summary>
        public ICollection<Test> Tests { get; private set; }

        /// <summary>
        /// Id Account-a
        /// </summary>
        public string ExternalId { get; private set; }


        public Examiner()
        {
            Groups = new List<Group>();
            Tests = new List<Test>();
        }

        public Examiner(string firstname, string lastname, string externalId)
        {
            Groups = new List<Group>();
            Tests = new List<Test>();
            FirstName = firstname;
            LastName = lastname;
            ExternalId = externalId;
        }
    }
}
