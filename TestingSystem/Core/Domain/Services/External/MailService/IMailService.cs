using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services.External.MailService
{
    public interface IMailService
    {
        /// <summary>
        /// Metod za slanje email-a
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeMailRequest welcomeMailRequest);
    }
}
