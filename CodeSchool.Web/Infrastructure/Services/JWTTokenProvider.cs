using System;
using System.Collections.Generic;
using System.Security.Claims;
using CodeSchool.BusinessLogic.Extensions;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure.AppSettings;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;

namespace CodeSchool.Web.Infrastructure.Services
{
    public class JWTTokenProvider
    {
        private readonly JWTSettings _jwtSettings;

        public JWTTokenProvider(IOptions<JWTSettings> optionsAccessor)
        {
            _jwtSettings = optionsAccessor.Value;
        }

        public string GetIdToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                {"username", user.UserName},
                {"email", user.Email},
                {"isAdmin", user.IsAdmin}
            };
            return GetToken(payload);
        }

        public string GetAccessToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                { ClaimTypes.Email, user.Email },
                { ClaimTypes.NameIdentifier, user.Id },
                { "companyId", user.CompanyId },
                { ClaimTypes.Role, user.IsAdmin ? new [] { "Admin" } : new string [] {}}
            };
            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _jwtSettings.SecretKey;

            payload.Add("iss", _jwtSettings.Issuer);
            payload.Add("aud", _jwtSettings.Audience);
            payload.Add("iat", DateTime.Now.ConvertToUnixTimestamp());
            payload.Add("exp", DateTime.Now.AddDays(30).ConvertToUnixTimestamp());

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }
    }
}
