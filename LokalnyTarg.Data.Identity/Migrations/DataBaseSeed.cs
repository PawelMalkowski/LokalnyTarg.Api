using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LokalnyTarg.Data.Identity.Migrations
{
    public class DataBaseSeed:IdentityDbContext
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManger;
        public DataBaseSeed(UserManager<IdentityUser> userManger, RoleManager<IdentityRole> roleManger)
        {
            _userManger = userManger;
            _roleManger = roleManger;
        }
        public async Task Seed()
        {
            string[] roleNames = {"Administrator", "Admin", "User"};
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManger.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManger.CreateAsync(new IdentityRole(roleName));
                }
            }
            var user = new IdentityUser
            {
                UserName = "Admin",
                Email = "abcd1@gmail.com",
                EmailConfirmed = true,
            };
            var user1 = new IdentityUser
            {
                UserName = "Test",
                Email = "test@gmail.com",
                EmailConfirmed = true,
            };
            await _userManger.CreateAsync(user, "Admin1234,");
            await _userManger.CreateAsync(user1, "Admin1234,");
            await _userManger.AddToRoleAsync(user, "Administrator");
        }
    }
}
