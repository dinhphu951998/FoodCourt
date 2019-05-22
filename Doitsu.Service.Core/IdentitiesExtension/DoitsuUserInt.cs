using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Doitsu.Service.Core.AuthorizeBuilder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Doitsu.Service.Core.IdentitiesExtension
{
    public class DoitsuUserInt : IdentityUser<int>
    {
        public DoitsuUserInt()
        {
        }

        public virtual async Task<TokenAuthorizeModel> AuthorizeAsync(UserManager<DoitsuUserInt> userManager)
        {
            var userRoles = (await userManager.GetRolesAsync(this));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, this.Id.ToString()),
                new Claim(ClaimTypes.Name, this.UserName),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(userRoles))
            };

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
                Email = this.Email,
                Roles = await userManager.GetRolesAsync(this)
            };
        }

        public virtual async Task<TokenAuthorizeModel> AuthorizeAsync(DoitsuUserIntManager userManager)
        {
            var userRoles = (await userManager.GetRolesAsync(this));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, this.Id.ToString()),
                new Claim(ClaimTypes.Name, this.UserName),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(userRoles))
            };

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
                Email = this.Email,
                Roles = await userManager.GetRolesAsync(this)
            };
        }

        public virtual TokenAuthorizeModel AuthorizeAsync(List<string> userRoles, string secretKey, string issuer, string audience)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, this.Id.ToString()),
                new Claim(ClaimTypes.Name, this.UserName),
                
            };
            userRoles.ForEach(ur =>
            {
                claims.Add(new Claim(ClaimTypes.Role, ur));
            });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Default.GetBytes(secretKey);

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
                Email = this.Email,
                Roles = userRoles
            };
        }
    }
}
