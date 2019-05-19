using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.Helpers
{
    public class AuthenticationInfo
    {
        public GoogleAuthenticationInfo Google { get; set; }
        public FacebookAuthenticationInfo Facebook { get; set; }

    }

    public class GoogleAuthenticationInfo
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class FacebookAuthenticationInfo
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
