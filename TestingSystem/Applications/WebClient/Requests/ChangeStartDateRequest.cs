using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class ChangeStartDateRequest
    {
        [DataMember]
        public short TestId { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
    }
}
