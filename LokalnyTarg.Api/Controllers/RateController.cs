using LokalnyTarg.Api.Mappers;
using LokalnyTarg.Api.Validation;
using LokalnyTarg.IServices.Ratio;
using LokalnyTarg.IServices.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LokalnyTarg.Api.BindingModels
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Rate")]
    [EnableCors()]
    public class RateController : Controller
    {
        private readonly IRateService _rate;
        private readonly UserManager<IdentityUser> _userManger;

        public RateController( IRateService rate, UserManager<IdentityUser> userManager)
        {
            _userManger = userManager;
            _rate = rate;
        }

        [Route("", Name = "AddRate")]
        [ValidateModel]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRate(AddRate addRatio)
        {
            var addRatioService = AddRateToAddRateServiceMapper.AddRateToAddRateService(addRatio);
            var user = await _userManger.GetUserAsync(User);
            try
            {
                await _rate.AddRatio(user.Id, addRatioService);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
        [Route("{userId}", Name = "GetRate")]
        [ValidateModel]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRate(string searchedPhrase)
        {
            
            var user = await _userManger.GetUserAsync(User);
            try
            {
           //     await _rate.AddRatio(user.Id, addRatioService);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
