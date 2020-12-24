using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SecurityProject.Models;

namespace SecurityProject.Services
{
    public interface IAuthenticationService
    {
        User Login(string email, string password);
        User Register(string name ,string email , string password, string userType);
    }
}
