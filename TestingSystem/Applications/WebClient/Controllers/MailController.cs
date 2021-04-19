using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
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
    [Route("[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService MailService;
        private readonly ILogger<MailController> Logger;

        public MailController(
            IMailService mailService,
            ILogger<MailController> logger)
        {
            MailService = mailService;
            Logger = logger;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(SendMailRequest sendMailRequest)
        {
            try
            {
                var request = new MailRequest
                {
                    ToEmail = sendMailRequest.ToEmail,
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
