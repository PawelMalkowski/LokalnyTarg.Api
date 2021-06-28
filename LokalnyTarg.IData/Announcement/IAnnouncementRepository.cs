using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.Domain.Announcement;

namespace LokalnyTarg.IData.Announcement
{
    public interface IAnnouncementRepository
    {
        public Task AddAnnouncement(string userId,Domain.Announcement.AddAnnouncement addAnnouncement);
        public Task<List<Annoucment>> SearchAnnouncement(string searchPhrase, int? category, int? userid, int? annoucmentid);
        public Task<bool> CategoryExist(uint categoryId);
    }
}
