using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.ApplicationServices
{
    public class UserService
    {
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public async Task<int> CreateUserAsync(string email, string password, UserRoleType userRole)
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

            return user.UserAccountId;

        }

        public async Task UpdateUserAccountIdAsync(string userId, int userAccountId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            user.SetUserAccountId(userAccountId);
            await UserManager.UpdateAsync(user);
        }

        public async Task UpdateUserEmailAsync(string userId, string email)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            user.UpdateEmail(email);
            await UserManager.UpdateAsync(user);
        }

       
        public async Task ChangeUserAccountIdAsync(string userId, int userAccountId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            user.SetUserAccountId(userAccountId);
            await UserManager.UpdateAsync(user);
        }

        public async Task ResetPasswordByUserIdAsync(string userId, string newPassword)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            await UserManager.RemovePasswordAsync(user);
            IdentityResult result = await UserManager.AddPasswordAsync(user, newPassword);
            CheckFalseIdentityResult(result);
        }

        public async Task ResetPasswordByUsernameAndTokenAsync(string username, string newPassword, string token)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            IdentityResult result = await UserManager.ResetPasswordAsync(user, token, newPassword);
            CheckFalseIdentityResult(result);
        }

        public async Task AddRoleToUser(string userId, string roleName)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
           
            IdentityRole role = await RoleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentNullException($"{nameof(User)} role is null");
            }
            IdentityResult result = await UserManager.AddToRoleAsync(user, roleName);
            CheckFalseIdentityResult(result);
        }

        public async Task RemoveRoleFromUser(string userId, string roleName)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            IdentityRole role = await RoleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentNullException($"{nameof(User)} role is null");
            }
            IdentityResult result = await UserManager.RemoveFromRoleAsync(user, roleName);
            CheckFalseIdentityResult(result);
        }

        public async Task DeleteUserAsync(string userId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            IdentityResult result = await UserManager.DeleteAsync(user);
            CheckFalseIdentityResult(result);
            if (user.UserAccountId == 0)
            {
                return;
            }
            //TODO: Delete Student or Examiner according to role
        }


        public async Task CheckEmailAndPasswordAsync(string email, string password)
        {
            User user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            bool correctPassword = await UserManager.CheckPasswordAsync(user, password);
            if (correctPassword == false)
            {
                return; //TODO:
            }
            //TODO:

        }

        public async Task SignInUserAsync(string email, bool rememberMe)
        {
            User user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            await SignInManager.SignInAsync(user, rememberMe);
        }

        public async Task SignOutCurrentUserAsync()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<string> CreateResetPasswordTokenAsync(string userName)
        {
            User user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            string resetPasswordToken = await UserManager.GeneratePasswordResetTokenAsync(user);
            return resetPasswordToken;
        }

        public async Task<bool> HasRoleAsync(string userId, string roleName)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }
            IdentityRole role = await RoleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentNullException($"{nameof(User)}");
            }

            bool result = await UserManager.IsInRoleAsync(user, roleName);
            return result;
        }


        #region IdentityRole
        //TODO: 
        public async Task CreateNewRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException();
            }
            IdentityRole role = new IdentityRole(roleName);
            IdentityResult result = await RoleManager.CreateAsync(role);
            CheckFalseIdentityResult(result);
        }

        #endregion

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
    }
}
