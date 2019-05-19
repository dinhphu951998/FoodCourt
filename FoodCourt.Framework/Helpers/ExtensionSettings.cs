using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Helpers
{
    public class ExtensionSettings
    {
        public IConfiguration configuration { get; private set; }

        public AppSettings appSettings
        {
            get
            {
                var appSettingsSection = this.configuration.GetSection("AppSettings");
                var appSettings = appSettingsSection.Get<AppSettings>();
                return appSettings;
            }
        }
        public AuthenticationInfo AuthenticationInfo
        {
            get
            {
                var authenSettingsSection = this.configuration.GetSection("Authentication");
                var authenSettings = authenSettingsSection.Get<AuthenticationInfo>();
                return authenSettings;
            }
        }

        public ExtensionSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
