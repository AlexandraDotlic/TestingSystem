using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
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
