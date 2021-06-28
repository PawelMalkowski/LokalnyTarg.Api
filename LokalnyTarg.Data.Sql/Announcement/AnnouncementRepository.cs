using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.Data.Sql.DAO;
using LokalnyTarg.Domain.Announcement;
using LokalnyTarg.IData.Announcement;
using Microsoft.EntityFrameworkCore;
using Product = LokalnyTarg.Domain.Announcement.Product;

namespace LokalnyTarg.Data.Sql.Announcement
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly LokalnyTargDBContext _context;
        public AnnouncementRepository(LokalnyTargDBContext context)
        {
            _context = context;
        }

        public async Task AddAnnouncement(string userId, AddAnnouncement addAnnouncement)
        {
            var newUserId =await _context.User.Where(x => x.EntityId == userId).Select(x => x.UserId).FirstOrDefaultAsync();
            var announcement = new DAO.Annoucement
            {
                Description = addAnnouncement.Description,
                Title = addAnnouncement.Title,
                SuppilerId= newUserId,
                Product = new DAO.Product
                {
                    CategoryId = addAnnouncement.CategoryId,
                    Description = addAnnouncement.ProductDescriptions,
                    Name = addAnnouncement.ProductName,
                    Picture= addAnnouncement.Photo,
                    Price = (float) addAnnouncement.Price
                }
            };
            
            await _context.AddAsync(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Annoucment>> SearchAnnouncement(string searchPhrase, int? category, int? userid, int? annoucmentid)
        {
            var anouncmentList = await _context.Annoucement.Join
                (
                    _context.Product,
                    annoucement=> annoucement.ProductId,
                    product=> product.ProductId,
                    (annoucement,product) => new
                    {
                        anouncments= annoucement,
                        products= product
                    }
                ).Join
                (
                _context.Supplier,
                annoucement => annoucement.anouncments.SuppilerId,
                supplier => supplier.SupplierId,
                (annoucement, supplier) => new
                {
                    annoucement,
                    supplierName= supplier.Name
                }

                )
                                       .Where(x => (searchPhrase==null ||(x.annoucement.products.Name.ToLower().Contains(searchPhrase.ToLower())))&& 
                                       (category==null|| x.annoucement.products.CategoryId==category)&&
                                       (userid==null||userid==x.annoucement.anouncments.SuppilerId)&&
                                       (annoucmentid==null || annoucmentid== x.annoucement.anouncments.AnnoucementId)) 
                                       .Select(x => new Annoucment(x.annoucement.anouncments.AnnoucementId,
                                       x.annoucement.anouncments.ProductId,x.supplierName, x.annoucement.anouncments.SuppilerId,
                                       x.annoucement.anouncments.Title,
                                       x.annoucement.anouncments.Description, x.annoucement.products.Name,x.annoucement.products.Description,
                                       x.annoucement.products.Price,x.annoucement.products.CategoryId,x.annoucement.products.Picture))
                                       .ToListAsync();
           
            return anouncmentList;
        }


        public async Task<bool> CategoryExist(uint categoryId)
        {
            return await _context.Category.AnyAsync(x => x.CategoryId == categoryId);
        }

    }
}
