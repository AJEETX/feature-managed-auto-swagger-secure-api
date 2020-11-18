using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain
{
    public interface IUserService
    {
        List<User> Users { get; }

        User GetById(int id);

        void Add(User user);

        void Update(User user);

        void Delete(int id);
    }

    internal class UserService : IUserService
    {
        public List<User> Users => DataStore.Users;

        public void Add(User user)
        {
            Users.ToList().Add(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            var user = Users.FirstOrDefault(user => user.Id == id);

            return user;
        }

        public void Update(User user)
        {
            var actualUser = GetById(user.Id);

            actualUser.EmailAddress = user.EmailAddress;
            actualUser.Role = user.Role;
        }
    }
}