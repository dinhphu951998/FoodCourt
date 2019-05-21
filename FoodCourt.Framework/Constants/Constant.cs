using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Constants
{
    public enum RoleType
    {
        [Display(Name = "MEMBER")]
        MEMBER = 1,
    }

    //public class Constant
    //{
    //    public const int TotalCodeLength = 20;
    //    public const int ValidPasswordLength= 10;

    //}

    public class ErrorMessage
    {
        //User
        public const string PASSWORD_NOT_VALID = "Password is not accepted";
        public const string USER_IS_NOT_EXIST = "The account that you've entered doesn't match any account. <b>Sign up for an account.</b>";
        public const string USER_CREATE_FAIL = "There is an error while creating this user";
        public const string LOGIN_WITH_GOOGLE_FAIL = "There is an error while login with Google";

        

    }
}

