

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Doitsu.Service.Core.AuthorizeBuilder;

namespace Doitsu.Service.Core.IdentitiesExtension
{
    public class DoitsuUser : IdentityUser
    {
        public DoitsuUser()
        {
        }

        public static async Task<TokenAuthorizeModel> AuthorizeAsync(UserManager<DoitsuUser> userManager, DoitsuUser user)
        {
            var userRoles = (await userManager.GetRolesAsync(user));
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Default.GetBytes(DoitsuJWTValidators.SecretKey);
            var issuer = DoitsuJWTValidators.Issuer;
            var audience = DoitsuJWTValidators.Audience;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.ToVietnamDateTime().AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return new TokenAuthorizeModel
            {
                Token = tokenString,
                ValidTo = token.ValidTo,
                Email = user.Email,
                Roles = await userManager.GetRolesAsync(user)
            };
        }

    }
}