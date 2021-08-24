using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Core.ApplicationServices;
using Core.Domain.Services.External.MailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Controllers
{
    /// <summary>
    /// Kontroler klasa maila
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly StudentService StudentService;
        private readonly IMailService MailService;
        private readonly ILogger<MailController> Logger;

        public MailController(
            IMailService mailService,
            StudentService studentService,
            ILogger<MailController> logger)
        {
            MailService = mailService;
            StudentService = studentService;
            Logger = logger;
        }

        /// <summary>
        /// Ruta koja se gadja za slanje maila korisniku
        /// </summary>
        /// <param name="sendMailRequest"></param>
        [HttpPost("SendMailToUser")]
        public async Task<IActionResult> SendMailToUser(SendMailRequest sendMailRequest)
        {
            try
            {
                string email;
                try
                {
                    email = await StudentService.GetStudentEmail(sendMailRequest.UserId);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, e.Message);
                    return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
                }

                var request = new MailRequest
                {
                    ToEmail = email,
                    Body = sendMailRequest.Body,
                    Subject = sendMailRequest.Subject,
                    Attachments = sendMailRequest.Attachments
                };
                await MailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Ruta koja se gadja za slanje maila dobrodoslice korisniku
        /// </summary>
        /// <param name="welcomeMailRequest"></param>
        [HttpPost("SendWelcomeMail")]
        public async Task<IActionResult> SendWelcomeMail(SendWelcomeMailRequest welcomeMailRequest)
        {
            try
            {
                var request = new WelcomeMailRequest
                {
                    ToEmail = welcomeMailRequest.ToEmail,
                    Username = welcomeMailRequest.Username
                };
                await MailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }
    }
}
