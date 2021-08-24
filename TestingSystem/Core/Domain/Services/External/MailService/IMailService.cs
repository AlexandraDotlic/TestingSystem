using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services.External.MailService
{
    /// <summary>
    /// Interfejs mail servisa
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Metod za slanje email-a
        /// </summary>
        /// <param name="mailRequest">Klasa koja sadrzi email, naziv emaila, body i attachment-e</param>
        /// <returns></returns>
        Task SendEmailAsync(MailRequest mailRequest);
        /// <summary>
        /// Metod za slanje email-a
        /// </summary>
        /// <param name="welcomeMailRequest">Klasa koja sadrzi email i username</param>
        /// <returns></returns>
        Task SendWelcomeEmailAsync(WelcomeMailRequest welcomeMailRequest);
    }
}
