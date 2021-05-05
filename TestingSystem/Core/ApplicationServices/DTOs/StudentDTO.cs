using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.DTOs
{
    public class StudentDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short? GroupId { get; set; }

        public StudentDTO()
        {

        }


        public StudentDTO(int id, string firstName, string lastName, short? groupId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            GroupId = groupId;
        }
    }

}