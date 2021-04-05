using Applications.WebClient.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class AddStudentToGroupRequest
    {
        [DataMember]
        public short GroupId { get; set; }
        [DataMember]
        public short StudentId { get; set; }
    }
}