using Microsoft.IdentityModel.Tokens;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Services
{
    public interface ITokenService
    {
        AuthToken Generate(User user);
    }

    internal class TokenService : ITokenService
    {
        public AuthToken Generate(User user)
        {
            var claims = new List<Claim>() {
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: TokenConstants.Issuer,
                audience: TokenConstants.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(TokenConstants.ExpiryInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConstants.key)), SecurityAlgorithms.HmacSha256)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthToken()
            {
                AccessToken = "Bearer "+ accessToken,
                ExpiresIn = TokenConstants.ExpiryInMinutes
            };
        }
    }
}