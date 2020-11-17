namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models
{
    public class Reader
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }

    public class User : Reader
    {
        public string Role { get; set; }
    }
}