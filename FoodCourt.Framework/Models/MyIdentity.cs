using Doitsu.Service.Core.AuthorizeBuilder;
using Doitsu.Service.Core.IdentitiesExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Models
{
    public class MyIdentity : DoitsuUserInt
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool Activated { get; set; }
        public DateTime BirthDate { get; set; }

        public async Task<TokenAuthorizeModel> AuthorizeAsync(MyUserManager userManager)
        {
            var roles = await userManager.GetRolesAsync(this);
            return base.AuthorizeAsync(roles);
        }
    }
}
