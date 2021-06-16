using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa studenta
    /// </summary>
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


        public Student(string firstname, string lastname, string email, string externalId)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            StudentTests = new List<StudentTest>();
            ExternalId = externalId;
        }

        /// <summary>
        /// Metoda koja sluzi da se studentu dodeli test
        /// </summary>
        /// <param name="studentTest"></param>
        public void AddStudentTest(StudentTest studentTest)
        {
            StudentTests.Add(studentTest);
        }

        /// <summary>
        /// Metoda za postavljanje imena studenta
        /// </summary>
        /// <param name="firstName"></param>
        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }

        /// <summary>
        /// Metoda koja sluzi za postavljanje prezimena studenta
        /// </summary>
        /// <param name="lastName"></param>
        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }

    }
}
