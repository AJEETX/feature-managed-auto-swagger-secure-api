namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public AuthToken Token { get; set; }
    }
}