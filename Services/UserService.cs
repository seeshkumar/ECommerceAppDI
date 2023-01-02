using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    class UserService : IUserService
    {
        public bool ValidDetails(List<User> users, User user)
        {
            if (users.Find(u => u.username == user.username && u.password == user.password) == null)
            {
                return false;
            }
            return true;
        }

    }
}
