using Applications.WebClient.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class GetQuestionAndAnswersRequest
    {
        [DataMember]
        public short testId { get; set; }
        [DataMember]
        public short questionId { get; set; }
    }
}
