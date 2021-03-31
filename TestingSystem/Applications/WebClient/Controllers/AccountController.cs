using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Authentication.ApplicationServices;
using Authentication.Domain.Entities;
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
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController: ControllerBase
    {
        private readonly UserService UserService;
        private readonly StudentService StudentService;
        private readonly ExaminerService ExaminerService;
        private readonly ILogger Logger;

        public AccountController(
            UserService userService,
            StudentService studentService,
            ExaminerService examinerService,
            ILogger<AccountController> logger)
        {
            UserService = userService;
            Logger = logger;
            StudentService = studentService;
            ExaminerService = examinerService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
        {
            UserRoleType userRoleType = (UserRoleType)registerUserRequest.UserRoleType;
            string accountId;
            try
            {
                accountId = await UserService.CreateUserAsync(registerUserRequest.Email, registerUserRequest.Password, userRoleType);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }

            if(userRoleType == UserRoleType.Examiner)
            {
                try
                {
                    int examinerId = await ExaminerService.CreateExaminer(registerUserRequest.FirstName, registerUserRequest.LastName, accountId);
                    return Ok(examinerId);
                }
                catch(Exception e)
                {
                    Logger.LogError(e, e.Message);
                    return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
                }
                
            }
            else
            {
                try
                {
                    int studentId = await StudentService.CreateStudent(registerUserRequest.FirstName, registerUserRequest.LastName, accountId);
                    return Ok(studentId);
                }
                catch(Exception e)
                {
                    Logger.LogError(e, e.Message);
                    return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
                }
          
            }
        }
        [HttpPost]
        [Route("LogIn")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogInUserRequest logInUserRequest)
        {
            try
            {
                await UserService.CheckEmailAndPasswordAsync(logInUserRequest.Email, logInUserRequest.Password);
                await UserService.SignInUserAsync(logInUserRequest.Email, logInUserRequest.RememberMe);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }

        }
        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await UserService.SignOutCurrentUserAsync();
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
