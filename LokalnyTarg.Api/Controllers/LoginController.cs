using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;
using LokalnyTarg.Api.Validation;
using LokalnyTarg.Api.ViewModel;
using LokalnyTarg.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LokalnyTarg.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    [EnableCors()]
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _signInManager;


        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManger = userManager;
            _signInManager = signInManager;
        }


        [Route("Register", Name = "RegisterUser")]
        [ValidateModel]
        [HttpPost]

        public async Task<IActionResult> Register([FromBody] CreateUser createUser)
        {
            var user = new IdentityUser
            {
                UserName = createUser.UserName,
                Email = createUser.Email,
            };
            List<string> EroorList = new List<string>();
            var result = await _userManger.CreateAsync(user, createUser.Password);


            foreach (var error in result.Errors)
            {
                EroorList.Add(error.Description);
            }
            var userStatus = new StatusViewModel
            {
                Errors = EroorList.ToArray(),
            };

            if (result.Succeeded)
            {
                userStatus.Status = "Success";
                string token = await _userManger.GenerateEmailConfirmationTokenAsync(user);
                SendEmail.SendRegistrationEmail(user.Email, token, user.UserName);
                return Ok(userStatus);
            }
            else
            {
                userStatus.Status = "Error";
                return BadRequest(userStatus);
            }
        }
        
        [Route("Login", Name = "LoginUser")]
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            var user = await _userManger.FindByNameAsync(loginUser.UserName) ?? await _userManger.FindByEmailAsync(loginUser.UserName);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    var userStatus2 = new StatusViewModel
                    {
                        Status = "Error",
                        Errors = new string[1] { "Email not confirmed" },
                    };
                    return BadRequest(userStatus2);
                }
                var signInResult = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);

                if (signInResult.Succeeded)
                {
                    var userStatus1 = new StatusViewModel
                    {
                        Status = "success",
                    };
                    return Ok(userStatus1);
                }
                else
                {
                    var userStatus3 = new StatusViewModel
                    {
                        Status = "Error",
                        Errors = new string[1] { "Password incorrect" },
                    };
                    return BadRequest(userStatus3);
                }
            }
            var userStatus = new StatusViewModel
            {
                Status = "Error",
                Errors = new string[1] { "user and email not exist" }
            };

            return BadRequest(userStatus);
        }

        [Route("LogOut", Name = "LogOut")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Route("ResetPassword", Name = "ResetPassword")]
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
          
            var user = await _userManger.FindByNameAsync(resetPassword.UserName) ??
                       await _userManger.FindByEmailAsync(resetPassword.UserName);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    var userStatus2 = new StatusViewModel
                    {
                        Status = "Error",
                        Errors = new string[1] { "Email not confirmed" },
                    };
                    return BadRequest(userStatus2);
                }
                string token = await _userManger.GeneratePasswordResetTokenAsync(user);
                SendEmail.SendResetPasswordEmail(user.Email, token, user.UserName);
                var userStatus1 = new StatusViewModel
                {
                    Status = "success",
                };
                return Ok(userStatus1);
            }
            var userStatus = new StatusViewModel
            {
                Status = "Error",
                Errors = new string[1] { "user and email not exist" }
            };

            return BadRequest(userStatus);
        }

        [Route("ConfirmResetPassword", Name = "ConfirmResetPassword")]
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPassword confirmResetPassword)
        {
            confirmResetPassword.Token = confirmResetPassword.Token.Replace(' ', '+');
            var user = await _userManger.FindByNameAsync(confirmResetPassword.UserName);
            var result = await _userManger.ResetPasswordAsync(user, confirmResetPassword.Token,confirmResetPassword.Password);
            List<string> errorList = new List<string>();
            foreach (var error in result.Errors)
            {
                errorList.Add(error.Description);
            }
            var userStatus = new StatusViewModel
            {
                Errors = errorList.ToArray(),
            };

            if (result.Succeeded)
            {
                userStatus.Status = "Success";
                return Ok(userStatus);
            }
            else
            {
                userStatus.Status = "Error";
                return BadRequest(userStatus);
            }
        }

        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmail confirmEmail)
        {
            confirmEmail.Token = confirmEmail.Token.Replace(' ', '+');
            var user = await _userManger.FindByNameAsync(confirmEmail.UserName);
            var result = await _userManger.ConfirmEmailAsync(user, confirmEmail.Token);
            List<string> errorList = new List<string>();
            foreach (var error in result.Errors)
            {
                errorList.Add(error.Description);
            }
            var userStatus = new StatusViewModel
            {
                Errors = errorList.ToArray(),
            };

            if (result.Succeeded)
            {
                userStatus.Status = "Success";
                return Ok(userStatus);
            }
            else
            {
                userStatus.Status = "Error";
                return BadRequest(userStatus);
            }
        }

        [Route("AddNewAdministrator/{userName}", Name = "AddNewAdministrator")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddNewAdministration(string userName)
        {
            var user = await _userManger.FindByNameAsync(userName);
            var result = await _userManger.AddToRoleAsync(user, "Administrator");
            List<string> errorList = new List<string>();
            foreach (var error in result.Errors)
            {
                errorList.Add(error.Description);
            }
            var userStatus = new StatusViewModel
            {
                Errors = errorList.ToArray(),
            };

            if (result.Succeeded)
            {
                userStatus.Status = "Success";
                return Ok(userStatus);
            }
            else
            {
                userStatus.Status = "Error";
                return BadRequest(userStatus);
            }
        }

        [Route("IsLogged", Name = "IsLogged")]
        [HttpGet]
        public async Task<IActionResult> IsLogged()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user == null)
            {
                return Ok(new IsLoggedViewModel {IsLogged = false});
            }
            else
            {
                var logged = new IsLoggedViewModel
                {
                    IsLogged = true,
                    UserName = user.UserName
                };
                return Ok(logged);
            }
        }

        [Route("AddNewAdmin/{userName}", Name = "AddNewAdmin")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddNewAdmin(string userName)
        {
            var user = await _userManger.FindByNameAsync(userName);
            var result = await _userManger.AddToRoleAsync(user, "Admin");

            List<string> errorList = new List<string>();
            foreach (var error in result.Errors)
            {
                errorList.Add(error.Description);
            }
            var userStatus = new StatusViewModel
            {
                Errors = errorList.ToArray(),
            };

            if (result.Succeeded)
            {
                userStatus.Status = "Success";
                return Ok(userStatus);
            }
            else
            {
                userStatus.Status = "Error";
                return BadRequest(userStatus);
            }
        }

        [Route("CheckRole", Name = "CheckRole")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CheckRole()
        {
            var user = await _userManger.GetUserAsync(User);
            var result = await _userManger.GetRolesAsync(user);
            return Ok(result);
        }

        [Route("userExists/{userName}", Name = "UserExist")]
        [HttpGet]
        public async Task<IActionResult> UserExists(string userName)
        {
            var user = await _userManger.FindByNameAsync(userName);
            var userExist = new ExistViewModel
            {
                Exist = user != null,
            };
            return Ok(userExist);
        }
    }
}
