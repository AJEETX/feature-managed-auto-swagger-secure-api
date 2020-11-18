using System.Collections.Generic;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models
{
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

        public static List<Product> Products => new List<Product>{
            new Product
            {
                Id=101,
                 Name="Product1",
                 Description="Description1"
            },
            new Product
            {
                Id=102,
                 Name="Product2",
                 Description="Description2"
            },
            new Product
            {
                Id=103,
                 Name="Product3",
                 Description="Description3"
            }
            };
    }
}