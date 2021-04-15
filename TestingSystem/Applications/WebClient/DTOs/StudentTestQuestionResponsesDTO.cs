using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class StudentTestQuestionResponsesDTO
    {
        [DataMember]
        public int QuestionId { get; set; }
        [DataMember]
        public ICollection<string> Responses { get; set; }
    }
}
