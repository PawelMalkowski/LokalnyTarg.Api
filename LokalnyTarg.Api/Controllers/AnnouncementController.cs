using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;
using LokalnyTarg.Api.Mappers;
using LokalnyTarg.Api.Validation;
using LokalnyTarg.IData.Announcement;
using LokalnyTarg.IServices.Announcement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LokalnyTarg.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Announcement")]
    [EnableCors()]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        private readonly UserManager<IdentityUser> _userManger;

        public AnnouncementController(IAnnouncementService announcementService, UserManager<IdentityUser> userManager)
        {
            _announcementService = announcementService;
            _userManger = userManager;
        }

        [Route("search/{searchedPhrase}", Name = "SearchAnnouncement")]
        [HttpGet]
        public async Task<IActionResult> SearchAnnouncement(string searchedPhrase)
        {
            try
            {
                var products =await _announcementService.SearchAnnouncement(searchedPhrase);
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("", Name = "AddAnnouncement")]
        [ValidateModel]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAnnouncement([FromBody]AddAnnouncement addAnnouncement)
        {
            var user = await _userManger.GetUserAsync(User);
            var addAnnouncementService =
                AddAnnouncementToAddAnnouncementServiceMapper.AddAnnouncementToAddAnnouncementService(addAnnouncement);
            var status = await _announcementService.AddAnnouncement(user.Id, addAnnouncementService);
            if (status.Status == "Success") return Ok(status);
            return BadRequest(status);
        }
    }
}
