namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models
{
    public class AuthToken
    {
        public long ExpiresIn { get; set; }

        public string AccessToken { get; set; }
    }
}