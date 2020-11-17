using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models
{
    public static class Features
    {
        public const string Promotions = "Promotion";
        public const string PromotionDiscounts = "Promotion.Discounts";
        public const string UserSuggestions = "Suggestion.User";
    }

    public class Model
    {
        public bool IsSuccess { get; set; }
        public AuthToken Token { get; set; }
    }

    public class AuthToken
    {
        public long ExpiresIn { get; set; }

        public string AccessToken { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
    }

    public static class TokenConstants
    {
        public const string Issuer = "thisismeyouknow";
        public const string Audience = "thisismeyouknow";
        public const int ExpiryInMinutes = 10;
        public const string key = "thiskeyisverylargetobreak";
    }

    [DataContract]
    public class Reader
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }
    }

    public class User : Reader
    {
        public string Role { get; set; }
    }

    public class Roles
    {
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Reader = "Reader";
        public const string None = "None";
    }

    public static class DataStore
    {
        public static List<User> Users => new List<User>
        {
            new User {
                Id = 1001,
                UserName = "Admin",
                EmailAddress = "admin@me.com",
                Role = Roles.Admin
            },
            new User {
                Id = 1002,
                UserName = "Reader",
                EmailAddress = "reader@me.com",
                Role = Roles.Reader
            },
            new User {
                Id = 1003,
                UserName = "Editor",
                EmailAddress = "editor@me.com",
                Role = Roles.Editor
            }
        };

        public static List<Reader> Readers => new List<Reader>() {
            new Reader {
                Id = 1003,
                EmailAddress = "reader1003@me.com",
                UserName = "reader1003"
            },
            new Reader {
                Id = 1002,
                EmailAddress = "reader@me.com",
                UserName = "reader1002"
            }
        };
    }
}