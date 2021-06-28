using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;

namespace LokalnyTarg.Api.Mappers
{
    public class AddAnnouncementToAddAnnouncementServiceMapper
    {
        public static IServices.Request.AddAnnouncement AddAnnouncementToAddAnnouncementService(AddAnnouncement addAnnouncement)
        {
            var addAnnouncementService = new IServices.Request.AddAnnouncement
            {
                CategoryId = addAnnouncement.CategoryId,
                Description = addAnnouncement.Description,
                Price = addAnnouncement.Price,
                ProductDescriptions = addAnnouncement.ProductDescription,
                ProductName = addAnnouncement.ProductName,
                Title = addAnnouncement.Title,
                Photo= addAnnouncement.Photo
            };
            return addAnnouncementService;
        }
    }
}
