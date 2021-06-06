using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class Student
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
        /// email
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// Grupa kojoj pripada
        /// </summary>
        public short? GroupId { get; private set; }
        public Group Group { get; private set; }
        public ICollection<StudentTest> StudentTests { get; private set; }

        /// <summary>
        /// Id Account-a
        /// </summary>
        public string ExternalId { get; private set; }
        public Student()
        {
            StudentTests = new List<StudentTest>();
        }

        
        public Student(string firstname, string lastname, string externalId)
        {
            FirstName = firstname;
            LastName = lastname;
            StudentTests = new List<StudentTest>();
            ExternalId = externalId;
        }

        public void AddStudentTest(StudentTest studentTest)
        {
            StudentTests.Add(studentTest);
        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }
    }
}
