using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ExternalAuthentication
{
    public abstract class ExternalAuthenticationValidation
    {
        private ExtensionSettings _extensionSettings;
        public ExternalAuthenticationValidation(ExtensionSettings _extensionSettings)
        {
            this._extensionSettings = _extensionSettings;
        }
        public abstract void GetUserDetails(MyIdentity user, string providerToken);


    }
}
