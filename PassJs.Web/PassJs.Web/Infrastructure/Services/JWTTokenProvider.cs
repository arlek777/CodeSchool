using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using PassJs.Core.Extensions;
using PassJs.DomainModels;
using PassJs.Web.Infrastructure.AppSettings;

namespace PassJs.Web.Infrastructure.Services
{
    public class JWTTokenProvider
    {
        private readonly JWTSettings _jwtSettings;

        public JWTTokenProvider(IOptions<JWTSettings> optionsAccessor)
        {
            _jwtSettings = optionsAccessor.Value;
        }

        public string GetIdToken(User user, bool sessionLifetime = false)
        {
            var payload = new Dictionary<string, object>
            {
                {"username", user.UserName},
                {"email", user.Email},
                {"isAdmin", user.IsAdmin}
            };
            return GetToken(payload, sessionLifetime);
        }

        public string GetAccessToken(User user, bool sessionLifetime = false)
        {
            var payload = new Dictionary<string, object>
            {
                { JwtClaimTypes.Email, user.Email },
                { JwtClaimTypes.Subject, user.Id },
                { "companyId", user.CompanyId },
                { "companyName", user.CompanyName },
                { JwtClaimTypes.Role, user.IsAdmin ? new [] { "Admin" } : new[] { "User" }}
            };
            return GetToken(payload, sessionLifetime);
        }

        private string GetToken(Dictionary<string, object> payload, bool sessionLifetime = false)
        {
            var secret = _jwtSettings.SecretKey;

            payload.Add(JwtClaimTypes.Issuer, _jwtSettings.Issuer);
            payload.Add(JwtClaimTypes.Audience, _jwtSettings.Audience);
            payload.Add(JwtClaimTypes.IssuedAt, DateTime.UtcNow.ConvertToUnixTimestamp());

            if (!sessionLifetime)
            {
                payload.Add("exp", DateTime.UtcNow.AddDays(30).ConvertToUnixTimestamp());
            }

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }
    }
}
