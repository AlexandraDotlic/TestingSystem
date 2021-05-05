using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class StudentDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get;  set; }
        [DataMember]
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

