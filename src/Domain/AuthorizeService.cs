using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using System.Linq;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain
{
    public interface IAuthorizeService
    {
        LoginResponse Authenticate(LoginRequest credentials);
    }

    internal class AuthorizeService : IAuthorizeService
    {
        private readonly ITokenService _tokenService;

        public AuthorizeService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public virtual LoginResponse Authenticate(LoginRequest credentials)
        {
            var user = DataStore.Users.FirstOrDefault(x => x.EmailAddress == credentials.Email);

            if (user != null)
            {
                return new LoginResponse
                {
                    IsSuccess = true,
                    Token = _tokenService.Generate(user)
                };
            }

            return new LoginResponse { IsSuccess = false };
        }
    }
}