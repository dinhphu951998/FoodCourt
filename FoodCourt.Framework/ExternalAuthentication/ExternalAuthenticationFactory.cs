using FoodCourt.Framework.Enums;
using FoodCourt.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ExternalAuthentication
{
    public class ExternalAuthenticationFactory
    {
        private ExtensionSettings _extensionSetting;
        public ExternalAuthenticationFactory(ExtensionSettings _extensionSetting)
        {
            this._extensionSetting = _extensionSetting;
        }

        public ExternalAuthenticationValidation Create(ExternalAuthenticationType type)
        {
            switch (type)
            {
                case ExternalAuthenticationType.Google:
                    return new GoogleApiTokenValidation(_extensionSetting);
                default:
                    return new GoogleApiTokenValidation(_extensionSetting);
            }
        }
    }
}

