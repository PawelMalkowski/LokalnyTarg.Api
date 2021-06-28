using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.Domain.Announcement;
using LokalnyTarg.IData.Announcement;
using LokalnyTarg.IData.Category;
using LokalnyTarg.IServices;
using LokalnyTarg.IServices.Announcement;

namespace LokalnyTarg.Services.Announcement
{
    public class AnnouncementService:IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }
        public async Task<ErrorTemplate> AddAnnouncement(string userId, IServices.Request.AddAnnouncement addAnnouncement)
        {
            if (addAnnouncement.Price * 100 % 1 != 0)
            {
                return new ErrorTemplate
                {
                    Status = "Error",
                    ErrorDescription = "Price is not correct format"
                };
            }
            else if (!await _announcementRepository.CategoryExist(addAnnouncement.CategoryId))
            {
                return new ErrorTemplate
                {
                    Status = "Error",
                    ErrorDescription = "Category not exist"
                };
            }
            else
            {
                var addAnnouncementData = new AddAnnouncement(addAnnouncement.Title, addAnnouncement.Description,
                    addAnnouncement.ProductName, addAnnouncement.ProductDescriptions, addAnnouncement.Price,
                    addAnnouncement.CategoryId,addAnnouncement.Photo);

                await _announcementRepository.AddAnnouncement(userId, addAnnouncementData);
                return new ErrorTemplate
                {
                    Status = "Success"
                };
            }
        }

        public async Task<List<IServices.Announcement.Annoucment>> SearchAnnouncement(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                throw new Exception("Searched phrase can not be empty");
            }
            else
            {
                string[] parsePhrase= ParseString(phrase);
                int? category = Int32.TryParse(parsePhrase[1], out var tempVal) ? tempVal : (int?)null;
                int? userId = Int32.TryParse(parsePhrase[2], out var tempVal2) ? tempVal2 : (int?)null;
                int? annoucmentid = Int32.TryParse(parsePhrase[3], out var tempVal3) ? tempVal3 : (int?)null;
                var productList=await  _announcementRepository.SearchAnnouncement(parsePhrase[0], category,userId,annoucmentid);
                var annoucments =productList.Select(x => new IServices.Announcement.Annoucment
                {
                    
                    AnnoucementId= x.AnnoucementId,
                    Description= x.Description,
                    SuplierName= x.SuplierName,
                    SuppilerId= x.SuppilerId,
                    Title= x.Title,
                    Product= new IServices.Announcement.Product
                    {
                        CategoryId= x.Product.CategoryId,
                        Description= x.Product.Description,
                        Name= x.Product.Name,
                        Price= x.Product.Price,
                        ProductId= x.Product.ProductId,
                        Photo= x.Product.Photo
                    }
                    
                }).ToList();
                return annoucments;
            }
        }

        private string[] ParseString(string phrase)
        {
            List<string> listAllString = phrase.Split('&').ToList();
            string[] listAttribute = new string[4];
            foreach (var value in listAllString)
            {
                if (value.ToLower().Contains("searchphrase=")) listAttribute[0] = value[13..];
                if (value.ToLower().Contains("category=")) listAttribute[1] = value[9..];
                if (value.ToLower().Contains("userid=")) listAttribute[2] = value[7..];
                if (value.ToLower().Contains("annoucmentid=")) listAttribute[3] = value[13..];
            }
            return listAttribute;
        }
    }
}
