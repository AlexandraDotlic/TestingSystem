using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Responses
{
    [DataContract]
    public class GetStudentTestResultResponse
    {
        [DataMember]
        public short TestId { get; set; }
        [DataMember]
        public int StudentTestScore { get; set; }
        [DataMember]
        public int TotalTestScore { get; set; }
    }
}
