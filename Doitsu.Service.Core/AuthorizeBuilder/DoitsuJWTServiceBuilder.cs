using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doitsu.Service.Core.AuthorizeBuilder
{
    /// <summary>
    /// This class have to build the JWT Authorization to your API or Project MVC
    /// </summary>
    public static class DoitsuJWTServiceBuilder
    {
        public static void BuildJWTService(IServiceCollection services)
        {

            //Authentication Schema
            const string scheme = JwtBearerDefaults.AuthenticationScheme;

            // Add authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = scheme;
                options.DefaultChallengeScheme = scheme;
                options.DefaultScheme = scheme;
                //options.DefaultSignInScheme = scheme;
                //options.DefaultSignOutScheme = scheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = DoitsuJWTValidators.Audience,
                    ValidIssuer = DoitsuJWTValidators.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(DoitsuJWTValidators.SecretKey))
                };
            });

        }

        public static void BuildJWTService(IServiceCollection services, Action<AuthenticationOptions> authenticationOptions,
            Action<JwtBearerOptions> jwtBearerOptions)
        {
            services.AddAuthentication(authenticationOptions).AddJwtBearer(jwtBearerOptions);
        }

    }
}
