using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Requests
{
    public class SendWelcomeMailRequest
    {
        public string ToEmail { get; set; }
        public string Username { get; set; }
    }
}
