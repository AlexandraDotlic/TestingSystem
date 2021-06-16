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
    /// Kontroler klasa grupe
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase

    {

        private readonly GroupService GroupService;
        private readonly StudentService StudentService;
        private readonly ILogger<GroupController> Logger;

        public GroupController(
            GroupService groupService,
            StudentService studentService,
            ILogger<GroupController> logger)
        {
            GroupService = groupService;
            StudentService = studentService;
            Logger = logger;
        }

        /// <summary>
        /// Kontroler za kreiranje grupe od strane ispitivaca
        /// </summary>
        /// <param name="createGroupRequest"></param>
        [HttpPost]
        [Route("CreateGroup")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest createGroupRequest)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                var result = await GroupService.CreateGroup(createGroupRequest.Title, currentUserId);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler za dodavanje studenta u grupu od strane ispitivaca
        /// </summary>
        /// <param name="addStudentToGroupRequest"></param>
        [HttpPost]
        [Route("AddStudentToGroup")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> AddStudentToGroup(AddStudentToGroupRequest addStudentToGroupRequest)
        {
            try
            {
                await GroupService.AddStudentToGroup(addStudentToGroupRequest.GroupId, addStudentToGroupRequest.StudentId);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
            }
        }

        /// <summary>
        /// Kontroler koji vraca kolekciju svih studenata koji pripadaju odredjenoj grupi
        /// </summary>
        /// <param name="groupId"></param>
        [HttpGet]
        [Route("GetAllStudentsForGroup/{groupId}")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<GetAllStudentsForGroupResponse>> GetAllStudentsForGroup(short groupId)
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.StudentDTO> students = await StudentService.GetAllStudentsForGroup(groupId);
                var response = new GetAllStudentsForGroupResponse
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
        /// Kontroler koji vraca sve kreirane grupe iz baze
        /// </summary>
        [HttpGet]
        [Route("GetAllGroups")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<ActionResult<GetAllGroupsResponse>> GetAllGroups()
        {
            try
            {
                ICollection<Core.ApplicationServices.DTOs.GroupDTO> groups = await GroupService.GetAllGroups();
                var response = new GetAllGroupsResponse
                {
                    Groups = groups.Select(g => new GroupDTO(g.Id, g.Title, g.ExaminerId)).ToList()

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
        /// Kontroler koji postavlja naziv grupe
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="title"></param>
        [HttpPost]
        [Route("SetGroupTitle/{groupId}")]
        [Authorize(Policy = "IsExaminer")]
        public async Task<IActionResult> SetGroupTitle(short groupId, string title)
        {
            try
            {
                await GroupService.SetGroupTitle(groupId, title);
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
