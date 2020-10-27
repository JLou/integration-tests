using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Brenda.IntegrationTests.Infrastructure
{

    public static class StubJwtTokens
    {
        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        public const string Audience = "my-testing-audience";
        private readonly static JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private readonly static RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private readonly static byte[] _key = new byte[32];

        static StubJwtTokens()
        {
            _rng.GetBytes(_key);
            SecurityKey = new SymmetricSecurityKey(_key) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public static string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            return _tokenHandler.WriteToken(new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(20),
                SigningCredentials));
        }
    }

}
