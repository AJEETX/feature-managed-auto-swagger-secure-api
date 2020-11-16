using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using System.Linq;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Services
{
    public interface IAuthorizeService
    {
        Model Authenticate(LoginModel credentials);
    }

    internal class AuthorizeService : IAuthorizeService
    {
        private readonly ITokenService _tokenService;

        public AuthorizeService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public virtual Model Authenticate(LoginModel credentials)
        {
            var user = ReaderStore.Users.FirstOrDefault(x => x.EmailAddress == credentials.Email);

            if (user != null)
            {
                return new Model
                {
                    IsSuccess = true,
                    Token = _tokenService.Generate(user)
                };
            }

            return new Model { IsSuccess = false };
        }
    }
}