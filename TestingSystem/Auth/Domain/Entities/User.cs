using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Entities
{
    public class User : IdentityUser
    {

        #region Constructors

        public User()
        {
        }

        public User(string email)
        {
            UserName = email;
            Email = email;
        }

        #endregion


        #region Methods

        public void UpdateUserName(string userName)
        {
            Email = userName;
            UserName = userName;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
            UserName = email;
        }

        #endregion Methods
    }
}