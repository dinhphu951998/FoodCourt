using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class LoginViewModel : BaseViewModel<MyIdentity>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel : BaseViewModel<MyIdentity>
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
    }

    public class MyIdentityViewModel : BaseViewModel<MyIdentity>
    {
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int Id { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
