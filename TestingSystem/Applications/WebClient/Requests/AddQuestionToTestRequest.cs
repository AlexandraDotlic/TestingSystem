using Applications.WebClient.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class AddQuestionToTestRequest
    {
        [DataMember]
        public short TestId { get; set; }
        [DataMember]
        public string QuestionText { get; set; }
        [DataMember]
        public ICollection<AnswerOptionDTO> AnswerOptions { get; set; }
    }
}
