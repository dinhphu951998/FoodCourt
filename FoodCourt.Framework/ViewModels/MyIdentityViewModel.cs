using Doitsu.Service.Core;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class LoginViewModel : BaseViewModel<MyIdentity>
    {
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password, ErrorMessage = ErrorMessage.PASSWORD_NOT_VALID)]
        [Required]
        public string Password { get; set; }
    }

    public class RegisterViewModel : BaseViewModel<MyIdentity>
    {
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? Activated { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class MyIdentityViewModel : BaseViewModel<MyIdentity>
    {
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int Id { get; set; }

        public int AccessFailedCount { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? Activated { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class RegisterExternalViewModel : BaseViewModel<MyIdentity>
    {
        public string ProviderIdToken { get; set; }

        public string Provider { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? Activated { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

    }
}
