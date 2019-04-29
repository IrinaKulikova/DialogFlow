using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebDialog.Models;

namespace WebDialog.Data
{
    public static class Initializer
    {
        public async static Task Seed(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<ApplicationRole> roleManager,
                                IConfiguration configuration)
        {
            if (context.Datas.Any())
            {
                context.Datas.RemoveRange(context.Datas.ToList());
                await context.SaveChangesAsync();
            }
            if (!context.Datas.Any())
            {
                var datas = new List<Models.Data>
                {
                    new Models.Data{ SomeParameter = 70 },
                    new Models.Data{ SomeParameter = 68 },
                    new Models.Data{ SomeParameter = 66 }
                };
                context.Datas.AddRange(datas);
                await context.SaveChangesAsync();
            }

            if (context.Roles.Any() || context.Users.Any() || context.UserRoles.Any())
            {
                context.UserRoles.RemoveRange(context.UserRoles.ToList());
                await context.SaveChangesAsync();
                foreach (var u in userManager.Users.ToList())
                {
                    await userManager.DeleteAsync(u);
                }
                foreach (var r in roleManager.Roles.ToList())
                {
                    await roleManager.DeleteAsync(r);
                }
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any() && !context.Users.Any() && !context.UserRoles.Any())
            {
                var roleAdmin = new ApplicationRole() { Name = nameof(RolesEnum.Admin) };
                var roleUser = new ApplicationRole() { Name = nameof(RolesEnum.User) };
                await roleManager.CreateAsync(roleAdmin);
                await roleManager.CreateAsync(roleUser);
                await context.SaveChangesAsync();

                var users = configuration.GetSection("Users").Get<List<User>>();
                foreach (var userConfig in users)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = userConfig.Login,
                        Email = userConfig.Login,
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };
                    await userManager.CreateAsync(user, userConfig.Password);
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(user, userConfig.Role);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}