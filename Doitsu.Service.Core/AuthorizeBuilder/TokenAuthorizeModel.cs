using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doitsu.Service.Core.AuthorizeBuilder
{
    /// <summary>
    /// The model present to the response when client request authorize to your web app
    /// </summary>
    public class TokenAuthorizeModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("validTo")]
        public DateTime ValidTo { get; set; }
        [JsonProperty("validFrom")]
        public DateTime ValidFrom { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("roles")]
        public IList<string> Roles { get; set; }
    }
}
