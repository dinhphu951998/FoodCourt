using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Doitsu.Service.Core.IdentitiesExtension
{
    public class DoitsuUserManager : UserManager<DoitsuUser>
    {
        public DoitsuUserManager(IUserStore<DoitsuUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<DoitsuUser> passwordHasher, IEnumerable<IUserValidator<DoitsuUser>> userValidators, IEnumerable<IPasswordValidator<DoitsuUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<DoitsuUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }

    public class DoitsuUserManager<T> : UserManager<T> where T : DoitsuUser
    {
        public DoitsuUserManager(IUserStore<T> store, IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<T> passwordHasher, IEnumerable<IUserValidator<T>> userValidators, 
            IEnumerable<IPasswordValidator<T>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<T>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, 
                  keyNormalizer, errors, services, logger)
        {
        }
    }
}
