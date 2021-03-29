using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Domain.Entities
{
    public class User : IdentityUser
    {

        #region Properties

        public int UserAccountId { get; private set; }


        #endregion

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

        public void SetUserAccountId(int userAccountId)
        {
            UserAccountId = userAccountId;
        }

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