using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Responses
{
    [DataContract]
    public class TakeTheTestResponse
    {
        [DataMember]
        public int StudentScore { get; set; }
        [DataMember]
        public int TotalTestScore { get; set; }
    }
}
