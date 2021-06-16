using Applications.WebClient.Helpers;
using Applications.WebClient.Requests;
using Auth.Domain.Entities;
using Core.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Applications.WebClient.Controllers
{
    /// <summary>
    /// Kontroler klasa za registraciju naloga
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly StudentService StudentService;
        private readonly ExaminerService ExaminerService;
        private readonly ILogger Logger;
        private readonly SignInManager<User> SignInManager;
        private readonly UserManager<User> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _validFor;
        private readonly SymmetricSecurityKey _signingKey;

        public AccountController(
            StudentService studentService,
            ExaminerService examinerService,
            ILogger<AccountController> logger,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            Logger = logger;
            StudentService = studentService;
            ExaminerService = examinerService;

            SignInManager = signInManager;
            UserManager = userManager;
            RoleManager = roleManager;

            var secretKey = configuration["Security:SecretKey"];
            _issuer = configuration["Security:Issuer"];
            _audience = configuration["Security:Audience"];
            _validFor = configuration.GetValue<int>("Security:ValidFor");
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }

        /// <summary>
        /// Kontroler za registraciju korisnika - ispitivaca ili studenta
        /// </summary>
        /// <param name="registerUserRequest"></param>

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
        {
            UserRoleType userRoleType = (UserRoleType)registerUserRequest.UserRoleType;
            string accountId;
            try
            {
                accountId = await CreateUserAsync(registerUserRequest.Email, registerUserRequest.Password, userRoleType);
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
                    int studentId = await StudentService.CreateStudent(registerUserRequest.FirstName, registerUserRequest.LastName, registerUserRequest.Email, accountId);
                    return Ok(studentId);
                }
                catch(Exception e)
                {
                    Logger.LogError(e, e.Message);
                    return BadRequest(ResponseHelper.ClientErrorResponse(e.Message, e.InnerException));
                }
          
            }
        }

        /// <summary>
        /// Kontroler za kreiranje korisnickog tokena na osnovu podataka za registraciju
        /// </summary>
        /// <param name="model"></param>

        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] LogInUserRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(ResponseHelper.ClientErrorResponse("Required fields not filled!"));
            }

            var sigInResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!sigInResult.Succeeded) return BadRequest(ResponseHelper.ClientErrorResponse("Login attempt failed!"));

            var user = await UserManager.FindByEmailAsync(model.Email);

            var user_role = await UserManager.IsInRoleAsync(user, "Examiner") ? "Examiner" : "Student";

            return Ok(new
            {
                Token = GenerateJWT(user, user_role),
                UserRole = user_role,
                User = user
            });
        }

        #region private methods
        private string GenerateJWT(User user, string user_roles)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), new[]
            {
                new Claim("id", user.Id),
                new Claim("rol", "api_access")
            });

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                    ClaimValueTypes.Integer64),
                new Claim("user_roles", user_roles),
                identity.FindFirst("rol"),
                identity.FindFirst("id")
            };

            var jwt = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_validFor),
                new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                                      new DateTimeOffset(1970, 1, 1, 0, 0, 0,
                                          TimeSpan.Zero))
                .TotalSeconds);
        }

        private async Task<string> CreateUserAsync(string email, string password, UserRoleType userRole)
        {
            var user = new User(email);
            IdentityResult result = await UserManager.CreateAsync(user, password);
            CheckFalseIdentityResult(result);

            string roleName = Enum.GetName(typeof(UserRoleType), userRole);
            IdentityRole role = await RoleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentNullException($"Role is null");
            }
            result = await UserManager.AddToRoleAsync(user, roleName);
            CheckFalseIdentityResult(result);

            return user.Id;

        }

        private void CheckFalseIdentityResult(IdentityResult result)
        {
            if (result.Succeeded == false)
            {
                var messageErrorSb = new StringBuilder();
                foreach (IdentityError error in result.Errors)
                {
                    messageErrorSb.Append(error.Description);
                    messageErrorSb.Append("\n");
                }
                throw new InvalidOperationException(messageErrorSb.ToString());
            }
        }

        #endregion
    }
}
