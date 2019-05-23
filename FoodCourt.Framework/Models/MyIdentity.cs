using Doitsu.Service.Core.AuthorizeBuilder;
using Doitsu.Service.Core.IdentitiesExtension;
using Doitsu.Service.Core.ModelBase;
using FoodCourt.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Models
{
    public partial class MyIdentity : DoitsuUserInt, IActivable
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool Activated { get; set; }
        public DateTime? BirthDate { get; set; }
         
        public async Task<TokenAuthorizeModel> AuthorizeAsync(MyUserManager userManager, AppSettings settings)
        {
            var roles = await userManager.GetRolesAsync(this);
            return base.AuthorizeAsync(roles, settings.SecretKey, settings.Issuer, settings.Audience);
        }
    }
}
