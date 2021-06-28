using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.Domain.UserProfile;

namespace LokalnyTarg.IData.UserProfile
{
    public interface IUserProfileRepository
    {
        Task<bool> UserExist(string userId);
        Task<bool> UserHasAddress(string userId);
        Task EditUserProfile(string userId, User user);
        Task CreateUserProfile(string userId, User user);
        Task<UserProfile> GetUserProfile(string userid);
    }
}
