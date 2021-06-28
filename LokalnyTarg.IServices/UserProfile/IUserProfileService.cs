using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.IServices.Request;

namespace LokalnyTarg.IServices.UserProfile
{
    public interface IUserProfileService
    {
        public Task EditUser(string userId, EditUser editUser);
        public Task<UserProfile>GetUserProfile(string userId);
    }
}
