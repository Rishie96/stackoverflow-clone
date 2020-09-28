using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Services
{
    public class UserServices
    {
        public UserDetails GetUserDetails(string UserID)
        {
            UserContext user = new UserContext();
            UserDetails userDetails = user.Details.FirstOrDefault(u => u.UserID == UserID);
            return userDetails;
        }
    }
}