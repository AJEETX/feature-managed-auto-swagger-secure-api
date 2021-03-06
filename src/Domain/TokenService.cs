﻿using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain
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
                issuer: Constants.Issuer,
                audience: Constants.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Constants.ExpiryInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.key)), SecurityAlgorithms.HmacSha256)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthToken()
            {
                AccessToken = "Bearer " + accessToken,
                ExpiresIn = Constants.ExpiryInMinutes
            };
        }
    }
}