using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DataAccess.EfCoreDataAccess.Seeds
{
    public static class UserRolesDatabaseSeed
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            List<IdentityRole> roles = roleManager.Roles.ToList();
            if (roles.Count() == 0)
            {
                string student = Enum.GetName(typeof(UserRoleType), UserRoleType.Student);
                string examiner = Enum.GetName(typeof(UserRoleType), UserRoleType.Examiner);

                var studentRole = new IdentityRole(student);
                roleManager.CreateAsync(studentRole).Wait();
                var exaimerRole = new IdentityRole(examiner);
                roleManager.CreateAsync(exaimerRole).Wait();

            }

        }
    }
}