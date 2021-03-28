﻿using System;
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
        /// Grupa kojoj pripada
        /// </summary>
        public int GroupId { get; private set; }
        public Group Group { get; private set; }
        public ICollection<StudentTestQuestion> StudentTestQuestions { get; private set; }
        public Student()
        {
            StudentTestQuestions = new List<StudentTestQuestion>();
        }

        
        public Student(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
            StudentTestQuestions = new List<StudentTestQuestion>();
        }
    }
}
