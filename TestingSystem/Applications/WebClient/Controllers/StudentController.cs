using Applications.WebClient.DTOs;
using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Applications.WebClient.Responses;
using Core.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Controllers
{
    /// <summary>
    /// Kontroler klasa studenta
    /// </summary>
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


        /// <summary>
        /// Ruta koja se gadja za postavljanje imena studenta
        /// </summary>
        /// <param name="firstName"></param>
        [HttpPost]
        [Route("SetStudentFirstName")]
        [Authorize(Policy = "IsStudent")]
        public async Task<IActionResult> SetStudentFirstName(string firstName)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                await StudentService.SetStudentFirstName(currentUserId, firstName);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Ruta koja se gadja za postavljanje prezimena studenta
        /// </summary>
        /// <param name="lastName"></param>
        [HttpPost]
        [Route("SetStudentLastName")]
        [Authorize(Policy = "IsStudent")]
        public async Task<IActionResult> SetStudentLastName(string lastName)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                await StudentService.SetStudentLastName(currentUserId, lastName);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Ruta koja se gadja za dobijanje liste svih studenata iz baze
        /// </summary>
        [HttpGet]
        [Route("GetAllStudents")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<GetAllStudentsResponse>> GetAllStudents()
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.StudentDTO> students = await StudentService.GetAllStudents();
                var response = new GetAllStudentsResponse
                {
                    Students = students.Select(s => new StudentDTO(s.Id, s.FirstName, s.LastName, s.GroupId)).ToList()
                };
                return response;

            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Ruta koja se gadja za dobijanje rezultata studenta na testu
        /// </summary>
        /// <param name="testId"></param>
        [HttpGet]
        [Route("GetStudentTestResult/{testId}")]
        [Authorize(Policy = "IsStudent")]
        public async Task<ActionResult<StudentTestResultDTO>> GetStudentTestResult(short testId)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                Core.ApplicationServices.DTOs.StudentTestScoreDTO studentTestResult = await StudentService.GetStudentTestResult(currentUserId, testId);
                var response = studentTestResult == null
                    ? null
                    : new StudentTestResultDTO
                    {
                        TestId = studentTestResult.TestId,
                        StudentTestScore = studentTestResult.StudentTestScore,
                        TotalTestScore = studentTestResult.TotalTestScore
                    };
                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        ///Ruta koja se gadja za dobijanje rezultata studenta na svim testovima koje je polagao
        /// </summary>
        [HttpGet]
        [Route("GetStudentResultsForAllTakenTests")]
        [Authorize(Policy = "IsStudent")]
        public async Task<ActionResult<GetStudentResultsForAllTakenTestsResponse>> GetStudentResultsForAllTakenTests()
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                ICollection<Core.ApplicationServices.DTOs.StudentTestScoreDTO> studentResults = await StudentService.GetStudentResultsForAllTakenTests(currentUserId);
                var response = studentResults == null || studentResults.Count == 0
                    ? null
                    : new GetStudentResultsForAllTakenTestsResponse
                    {
                        StudentResults = studentResults.Select(sr => new StudentTestResultDTO
                        {
                            StudentTestScore = sr.StudentTestScore,
                            TotalTestScore = sr.TotalTestScore,
                            TestId = sr.TestId,
                            TestName = sr.TestName
                        }).ToList()
                    };
                return Ok(response);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }
    }


}
