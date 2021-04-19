using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class CreateGroupRequest
    {
        [DataMember]
        public string Title { get; set; }

    }
}