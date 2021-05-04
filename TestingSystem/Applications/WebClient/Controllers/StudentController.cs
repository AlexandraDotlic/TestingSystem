using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Core.ApplicationServices;
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
    public class StudentController : ControllerBase
    {
        private readonly StudentService StudentService;
        private readonly ILogger<ExaminerController> Logger;

        public StudentController(
           StudentService studentService,
           ILogger<ExaminerController> logger)
        {
            StudentService = studentService;
            Logger = logger;
        }


        [HttpPost]
        [Route("SetStudentFirstName/{studentId}")]
        public async Task<IActionResult> SetStudentFirstName(int studentId, string firstName)
        {
            try
            {
                await StudentService.SetStudentFirstName(studentId, firstName);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        [HttpPost]
        [Route("SetStudentLastName/{studentId}")]
        public async Task<IActionResult> SetStudentLastName(int studentId, string lastName)
        {
            try
            {
                await StudentService.SetStudentLastName(studentId, lastName);
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
