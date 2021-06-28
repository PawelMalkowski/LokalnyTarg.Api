using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;

namespace LokalnyTarg.Api.Mappers
{
    public class EditProfileToEditUserServiceMapper
    {
        public static IServices.Request.EditUser EditUserToEditUserService(EditProfile editProfile)
        {

            var editUserService = editProfile.Address == null
                ? new IServices.Request.EditUser
                {
                    FirstName = editProfile.FirstName,
                    LastName = editProfile.LastName,
                    Description = editProfile.Description,
                    Address = new IServices.Request.Address
                    {
                        City = null,
                        Number = null,
                        Postcode = null,
                        Street = null
                    }

                }
                : new IServices.Request.EditUser
                {
                    FirstName = editProfile.FirstName,
                    LastName = editProfile.LastName,
                    Description = editProfile.Description,
                    Address = new IServices.Request.Address
                    {
                        City = editProfile.Address.City,
                        Number = editProfile.Address.Number,
                        Postcode = editProfile.Address.Postcode,
                        Street = editProfile.Address.Street
                    }
                };
            return editUserService;
        }
    }
}
