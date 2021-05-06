using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    [DataContract]
    public class GetStatisticForTestByDateRequest
    {
        public short TestId { get; set; }
        public DateTime Date { get; set; }
    }
}
