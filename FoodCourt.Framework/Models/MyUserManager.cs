using Doitsu.Service.Core.IdentitiesExtension;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Models
{
    public class MyUserManager : DoitsuUserIntManager<MyIdentity>
    {
        public MyUserManager(IUserStore<MyIdentity> store, IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<MyIdentity> passwordHasher, IEnumerable<IUserValidator<MyIdentity>> userValidators, 
            IEnumerable<IPasswordValidator<MyIdentity>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<MyIdentity>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, 
                  errors, services, logger)
        {
        }
    }
}
