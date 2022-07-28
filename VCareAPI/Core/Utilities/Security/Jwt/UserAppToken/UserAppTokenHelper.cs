using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt.UserAppToken
{
    public class UserAppTokenHelper : IUserAppTokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public UserAppTokenHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public IConfiguration Configuration { get; }

        public string DecodeToken(string input)
        {
            var handler = new JwtSecurityTokenHandler();
            if (input.StartsWith("Bearer "))
            {
                input = input.Substring("Bearer ".Length);
            }

            return handler.ReadJwtToken(input).ToString();
        }
        public JwtSecurityToken CreateJwtSecurityToken(
         TokenOptions tokenOptions,
         UserAppCore user,
         SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials);
            return jwt;
        }

        public TAccessToken CreateUserAppToken<TAccessToken>(UserAppCore user) where TAccessToken : IAccessToken, new()
        {
            _accessTokenExpiration = DateTime.Now.AddDays(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TAccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }
        private IEnumerable<Claim> SetClaims(UserAppCore user)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.UserId.ToString());

         

            if (!string.IsNullOrEmpty(user.UserName))
            {
                claims.AddName($"{user.UserName}");
            }

          //  claims.Add(new Claim(ClaimTypes.Role, user.ResponseData));
            //  claims.Add(new Claim(ClaimTypes.Sid, user.SiteId.ToString()));

            return claims;
        }
    }
}
