using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class GroupDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int ExaminerId { get; set; }
        
        public GroupDTO()
        {

        }
        public GroupDTO(int id, string title, int examinerId)
        {
            Id = id;
            Title = title;
            ExaminerId = examinerId;
        }
    }
}
