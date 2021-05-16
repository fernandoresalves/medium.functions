using System;

namespace Medium.Application
{
    public class UserService : IUserService
    {
        public User Register(User user)
        {
            user.Id = Guid.NewGuid();
            return user;
        }
    }
}
