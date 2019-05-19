using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Enums
{
    public class EnumType
    {
    }

    public enum ExternalAuthenticationType
    {
        [Display(Name = "Google")]
        Google = 1
    }
}
