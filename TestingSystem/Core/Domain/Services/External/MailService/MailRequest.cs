using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Services.External.MailService
{
    public class MailRequest
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
