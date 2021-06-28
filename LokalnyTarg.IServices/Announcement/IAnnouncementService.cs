using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.IServices.Request;

namespace LokalnyTarg.IServices.Announcement
{
    public interface IAnnouncementService
    {
        public Task<ErrorTemplate> AddAnnouncement(string userId,AddAnnouncement addAnnouncement);
        public Task<List<Annoucment>> SearchAnnouncement(string phrase);
    }
}
