﻿using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ExternalAuthentication
{
    public class GoogleApiTokenValidation : ExternalAuthenticationValidation
    {
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";
        private ExtensionSettings extensionSettings;
        private string clientId;

        public GoogleApiTokenValidation(ExtensionSettings extensionSettings) : base(extensionSettings)
        {
            this.extensionSettings = extensionSettings;
            this.clientId = extensionSettings.AuthenticationInfo.Google.ClientId;
        }

        public override void GetUserDetails(MyIdentity user, string providerToken)
        {
            using (var httpClient = new HttpClient())
            {

                var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, providerToken));

                HttpResponseMessage httpResponseMessage;
                try
                {
                    httpResponseMessage = httpClient.GetAsync(requestUri).Result;
                }
                catch
                {
                    throw;
                }

                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    throw new FoodCourtException(ErrorMessage.LOGIN_WITH_GOOGLE_FAIL);
                }

                var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(response);

                //Thêm đoạn code check clientId có trùng không?
                
                //if (!string.Equals(clientId, googleApiTokenInfo.aud))
                //{
                //    throw new FoodCourtException(ErrorMessage.LOGIN_WITH_GOOGLE_FAIL);
                //}

                user.Email = googleApiTokenInfo.email;
                user.FullName = googleApiTokenInfo.name;

                //return new ProviderUserDetails
                //{

                //    FirstName = googleApiTokenInfo.given_name,
                //    LastName = googleApiTokenInfo.family_name,
                //    Locale = googleApiTokenInfo.locale,
                //    Name = googleApiTokenInfo.name,
                //    ProviderUserId = googleApiTokenInfo.sub
                //};
            }
        }
    }

    public class GoogleApiTokenInfo
    {
        /// <summary>
        /// The Issuer Identifier for the Issuer of the response. Always https://accounts.google.com or accounts.google.com for Google ID tokens.
        /// </summary>
        public string iss { get; set; }

        /// <summary>
        /// Access token hash. Provides validation that the access token is tied to the identity token. If the ID token is issued with an access token in the server flow, this is always
        /// included. This can be used as an alternate mechanism to protect against cross-site request forgery attacks, but if you follow Step 1 and Step 3 it is not necessary to verify the 
        /// access token.
        /// </summary>
        public string at_hash { get; set; }

        /// <summary>
        /// Identifies the audience that this ID token is intended for. It must be one of the OAuth 2.0 client IDs of your application.
        /// </summary>
        public string aud { get; set; }

        /// <summary>
        /// An identifier for the user, unique among all Google accounts and never reused. A Google account can have multiple emails at different points in time, but the sub value is never
        /// changed. Use sub within your application as the unique-identifier key for the user.
        /// </summary>
        public string sub { get; set; }

        /// <summary>
        /// True if the user's e-mail address has been verified; otherwise false.
        /// </summary>
        public string email_verified { get; set; }

        /// <summary>
        /// The client_id of the authorized presenter. This claim is only needed when the party requesting the ID token is not the same as the audience of the ID token. This may be the
        /// case at Google for hybrid apps where a web application and Android app have a different client_id but share the same project.
        /// </summary>
        public string azp { get; set; }

        /// <summary>
        /// The user's email address. This may not be unique and is not suitable for use as a primary key. Provided only if your scope included the string "email".
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// The time the ID token was issued, represented in Unix time (integer seconds).
        /// </summary>
        public string iat { get; set; }

        /// <summary>
        /// The time the ID token expires, represented in Unix time (integer seconds).
        /// </summary>
        public string exp { get; set; }

        /// <summary>
        /// The user's full name, in a displayable form. Might be provided when:
        /// The request scope included the string "profile"
        /// The ID token is returned from a token refresh
        /// When name claims are present, you can use them to update your app's user records. Note that this claim is never guaranteed to be present.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The URL of the user's profile picture. Might be provided when:
        /// The request scope included the string "profile"
        /// The ID token is returned from a token refresh
        /// When picture claims are present, you can use them to update your app's user records. Note that this claim is never guaranteed to be present.
        /// </summary>
        public string picture { get; set; }

        public string given_name { get; set; }

        public string family_name { get; set; }

        public string locale { get; set; }

        public string alg { get; set; }

        public string kid { get; set; }
    }
}
