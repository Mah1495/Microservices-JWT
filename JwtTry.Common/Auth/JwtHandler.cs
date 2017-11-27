using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtTry.Common
{
    public class JwtHandler : IJwtHandler
    {
        readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        readonly JwtOptions options;
        protected SecurityKey securityKey;
        protected SigningCredentials signingCredentials;
        readonly JwtHeader jwtHeader;
        protected TokenValidationParameters validationParameters;
        public JwtHandler(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecretKey));
            signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            jwtHeader = new JwtHeader(signingCredentials);
            validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = this.options.Issuer,
                IssuerSigningKey = securityKey
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds;
            var now = (long)new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub", userId},
                {"iss", options.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };

            var jwt = new JwtSecurityToken(jwtHeader, payload);
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}
