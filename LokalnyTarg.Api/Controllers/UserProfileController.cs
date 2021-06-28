using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;
using LokalnyTarg.Api.Mappers;
using LokalnyTarg.Api.Validation;
using LokalnyTarg.Api.ViewModel;
using LokalnyTarg.IServices.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LokalnyTarg.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/UserProfile")]
    [EnableCors()]
    public class UserProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly IUserProfileService _userProfile;

        public UserProfileController(UserManager<IdentityUser> userManager,IUserProfileService userProfile)
        {
            _userManger = userManager;
            _userProfile = userProfile;
        }

        [Route("", Name = "EditProfile")]
        [ValidateModel]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromBody] EditProfile editProfile)
        {
            var user = await _userManger.GetUserAsync(User);
            var userService = EditProfileToEditUserServiceMapper.EditUserToEditUserService(editProfile);
            try
            {
                await _userProfile.EditUser(user.Id, userService);
            }
            catch (Exception exception)
            {
                var error = new ErrorViewModel
                {
                    Status = "Error",
                    ErrorDescription = exception.Message
                };
                return BadRequest(error);
            }

            return Ok();




        }
        [Route("", Name = "GetUserProfile")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile() 
        {
            try
            {
                var user = await _userManger.GetUserAsync(User);
                var userProfile = await _userProfile.GetUserProfile(user.Id);
                return Ok(userProfile);
            }
            catch (Exception exeption)
            {
                var error = new ErrorViewModel
                {
                    Status = "error",
                    ErrorDescription = exeption.Message
                };
                return BadRequest(error);
            }

            
        }
        [Route("id/{id}", Name = "GetUserProfileId")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfileId(string id)
        {
            try
            {
                var user = await _userManger.GetUserAsync(User);
                var userProfile = await _userProfile.GetUserProfile(user.Id);
                return Ok(userProfile);
            }
            catch (Exception exeption)
            {
                var error = new ErrorViewModel
                {
                    Status = "error",
                    ErrorDescription = exeption.Message
                };
                return BadRequest(error);
            }


        }
    }
}

