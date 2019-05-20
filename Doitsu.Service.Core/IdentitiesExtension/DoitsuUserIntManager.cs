using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Doitsu.Service.Core.IdentitiesExtension
{
    public class DoitsuUserIntManager : UserManager<DoitsuUserInt>
    {
        public DoitsuUserIntManager(IUserStore<DoitsuUserInt> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<DoitsuUserInt> passwordHasher, IEnumerable<IUserValidator<DoitsuUserInt>> userValidators, IEnumerable<IPasswordValidator<DoitsuUserInt>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<DoitsuUserInt>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }

    public class DoitsuUserIntManager<T> : UserManager<T> where T : DoitsuUserInt
    {
        public DoitsuUserIntManager(IUserStore<T> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<T> passwordHasher, IEnumerable<IUserValidator<T>> userValidators,
            IEnumerable<IPasswordValidator<T>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<T>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
                  keyNormalizer, errors, services, logger)
        {
        }
    }
}
