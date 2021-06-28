using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.IData.UserProfile;
using LokalnyTarg.IServices.Request;
using LokalnyTarg.IServices.UserProfile;
using UserAddress = LokalnyTarg.IData.UserProfile.UserAddress;

namespace LokalnyTarg.Services.UserProfile
{
    public class UserProfileService :IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        
        public async Task EditUser(string userId, EditUser editUser)
        {
            if (!string.IsNullOrWhiteSpace(editUser.Address.Postcode) 
                &&!ValidationPostCode(editUser.Address.Postcode)) throw new Exception("Postcode is invalid. Correct format is XX-XXX");
            bool userExist = await _userProfileRepository.UserExist(userId);
            if(!AddressNotExistOrIsComplete(editUser.Address) && (!userExist ||
                !await _userProfileRepository.UserHasAddress(userId))) throw new Exception("Addres must be complete");
            var user = new Domain.UserProfile.User(editUser.FirstName,editUser.LastName,editUser.Description,
                    editUser.Address.City,editUser.Address.Street,editUser.Address.Number,editUser.Address.Postcode);
            if (userExist) await _userProfileRepository.EditUserProfile(userId, user);
            else await _userProfileRepository.CreateUserProfile(userId, user);
        }

        public async Task<IServices.UserProfile.UserProfile> GetUserProfile(string userId)
        {
            try
            {
                var userProfile = await _userProfileRepository.GetUserProfile(userId);

                return new IServices.UserProfile.UserProfile
                {
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    Description = userProfile.Description,
                    UserId = userProfile.UserId,
                    Address = new IServices.UserProfile.UserAddress
                    {
                        City = userProfile.Address.City,
                        Postcode = userProfile.Address.Postcode,
                        Number = userProfile.Address.Number,
                        Street = userProfile.Address.Street
                    }
                };
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private  bool AddressNotExistOrIsComplete(Address address)
        {
            if (address.City == null &&
                address.Number == null &&
                address.Postcode == null &&
                address.Street == null) return true;
            if (address.City == null ||
                address.Number == null ||
                address.Postcode == null ||
                address.Street == null) return false;
            
            return true;
        }
        private bool ValidationPostCode(string postCode)
        {
            if (postCode[2] != '-') return false;
            string firstPart = postCode.Substring(0, 2);
            if (!UInt16.TryParse(firstPart, out _)) return false;
            string secondPart = postCode[3..];
            if (!UInt16.TryParse(secondPart, out _)) return false;
            return true;
        }
        
    }
}
