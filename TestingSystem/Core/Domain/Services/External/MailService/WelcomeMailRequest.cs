using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Services.External.MailService
{
    public class WelcomeMailRequest
    {
        public string ToEmail { get; set; }
        public string Username { get; set; }
    }
}
