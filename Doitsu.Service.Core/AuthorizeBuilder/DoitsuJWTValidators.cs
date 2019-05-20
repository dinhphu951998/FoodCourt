using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitsu.Service.Core.AuthorizeBuilder
{
    /// <summary>
    /// This class provide Constant Validators
    /// </summary>
    public static class DoitsuJWTValidators
    {
        public static string Issuer { get { return "http://doitsu.tech"; } }
        public static string Audience { get { return "http://doitsu.tech"; } }
        public static string SecretKey { get { return "@everone:DoitsuSecret!"; } }
    }
}
