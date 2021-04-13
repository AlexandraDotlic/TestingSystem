using Applications.WebClient.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class TakeTheTestRequest
    {
        [DataMember]
        public short TestId { get; set; }
        public ICollection<StudentTestQuestionResponsesDTO> Response { get; set; }
    }
}
