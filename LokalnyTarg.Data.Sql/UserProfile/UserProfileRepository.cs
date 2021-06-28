using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.Domain.UserProfile;
using LokalnyTarg.IData.UserProfile;
using Microsoft.EntityFrameworkCore;

namespace LokalnyTarg.Data.Sql.UserProfile
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly LokalnyTargDBContext _context;

        public UserProfileRepository(LokalnyTargDBContext context)
        {
            _context = context;
        }

        public async Task EditUserProfile(string userId, User user)
        {
            var daoUser = await _context.User.FirstAsync(x => x.EntityId == userId);
            
           
            
            daoUser.FirstName = !string.IsNullOrWhiteSpace(user.FirstName) ? user.FirstName: daoUser.FirstName;
            daoUser.LastName = !string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : daoUser.LastName;
            daoUser.Description = !string.IsNullOrWhiteSpace(user.Description) ? user.Description : daoUser.Description;
            if (user.Address.City != null ||
                user.Address.Number != null || 
                user.Address.Postcode != null ||
                user.Address.Street != null)
            {
                DAO.Address address;
                if (daoUser.AddressId != 0) address = await _context.Address.FirstOrDefaultAsync(x => x.AddressId == daoUser.AddressId);
                else
                {
                    address = new DAO.Address();
                    daoUser.Address = address;
                }
                address.City = !string.IsNullOrWhiteSpace(user.Address.City) ? user.Address.City : address.City;
                address.Street = !string.IsNullOrWhiteSpace(user.Address.Street) ? user.Address.Street : address.Street;
                address.Number = !string.IsNullOrWhiteSpace(user.Address.Number) ? user.Address.Number : address.Number;
                address.Postcode = !string.IsNullOrWhiteSpace(user.Address.Postcode) ? user.Address.Postcode : address.Postcode;
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserProfile(string userId, User user)
        {
            var daoUser = user.Address.City != null
                ? new DAO.User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    EntityId = userId,
                    Address = new DAO.Address
                    {
                        City = user.Address.City,
                        Number = user.Address.Number,
                        Postcode = user.Address.Postcode,
                        Street = user.Address.Street
                    }
                }
                : new DAO.User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    EntityId = userId,
                  
                };
           
            await _context.AddAsync(daoUser);
            await _context.SaveChangesAsync();
            var daoSupplier = new DAO.Supplier
            {
                SupplierId= daoUser.UserId,
                UsertId = daoUser.UserId,
                AddressId= daoUser.AddressId,
                Name= daoUser.FirstName,
                Description= daoUser.Description

            };
            await _context.AddAsync(daoSupplier);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UserExist(string userId)
        {
            return await _context.User.AnyAsync(x => x.EntityId == userId);
        }

        public async Task<bool> UserHasAddress(string userId)
        {
            return await _context.User.Join(
                _context.Address,
                user => user.AddressId,
                addres => addres.AddressId,
                (user, addres) => new
                {
                    UserId = user.EntityId,
                    addres.City
                }
                ).AnyAsync(x=>x.UserId==userId);
           
        }

        public async Task<IData.UserProfile.UserProfile> GetUserProfile(string userid)
        {
            var userProfileDAO = await _context.User.Join
            (
                _context.Address,
                user => user.AddressId,
                address => address.AddressId,
                (user, address) => new
                {
                    user.UserId,
                    address.City,
                    address.Street,
                    address.Number,
                    address.Postcode,
                    user.FirstName,
                    user.LastName,
                    user.Description,
                    Entity = user.EntityId
                }
            ).FirstOrDefaultAsync(x => x.Entity == userid);
            if (userProfileDAO == null) throw new Exception("User don't hava data");

                return new IData.UserProfile.UserProfile(userProfileDAO.FirstName, userProfileDAO.LastName,
                    userProfileDAO.Description, userProfileDAO.City, userProfileDAO.Street, userProfileDAO.Number,
                    userProfileDAO.Postcode, userProfileDAO.UserId);
            
        }
    }

}
