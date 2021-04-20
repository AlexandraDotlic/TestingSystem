using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class CreateTestStatisticRequest
    {
        [DataMember]
        public short TestId { get; set; }
        [DataMember]
        public short? GroupId { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
