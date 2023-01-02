using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Interfaces
{
    interface IUserService
    {

        public bool ValidDetails(List<User> users, User user);

    }
}
