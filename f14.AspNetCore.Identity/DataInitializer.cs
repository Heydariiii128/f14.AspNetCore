using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace f14.AspNetCore.Engine
{
    public class DataInitializer
    {
        private struct UserRecord
        {
            public string Login;
            public string Password;
            public string Email;
            public string[] Roles;
        }

        protected IServiceProvider _serviceProvider;
        protected ILogger _logger;
        private readonly List<UserRecord> _userRecords;

        public DataInitializer(IServiceProvider services)
        {
            _serviceProvider = services;
            _userRecords = new List<UserRecord>();
            _logger = services.GetService<ILoggerFactory>().CreateLogger<DataInitializer>();
        }

        #region Builder

        public static DataInitializer Create(IServiceProvider services) => new DataInitializer(services);

        //public DataInitializer ExtendRole(string role, string claimType, string claimValue)
        //{
        //    RoleData.ExtendRole(role, claimType, claimValue);
        //    return this;
        //}

        //public DataInitializer ExtendRole(string role, params KeyValuePair<string, string>[] claims)
        //{
        //    RoleData.ExtendRole(role, claims);
        //    return this;
        //}

        //public DataInitializer AddUser(string login, string password, string email, string role)
        //{
        //    _userRecords.Add(new UserRecord { Email = email, Login = login, Password = password, Roles = new string[] { role } });
        //    return this;
        //}

        //public DataInitializer AddUser(string login, string password, string email, string[] roleNames)
        //{
        //    _userRecords.Add(new UserRecord { Email = email, Login = login, Password = password, Roles = roleNames });
        //    return this;
        //}

        #endregion

        #region API

        public async Task InitializeAsync()
        {
            // Обновляем данные ролей.
            await UpdateRolesAsync();
            // Добавляем или обновляем данные об администраторских записях.
            foreach (var r in _userRecords)
            {
                await UpdateUserRecordsAsync(r);
            }
        }

        private async Task UpdateRolesAsync()
        {
            var roleManager = _serviceProvider.GetService<RoleManager<IdentityRole>>();
            foreach (string rName in RoleData.Collection)
            {
                bool exist = await roleManager.RoleExistsAsync(rName);
                if (!exist)
                {
                    var role = new IdentityRole(rName);
                    await roleManager.CreateAsync(role);

                    foreach (var pair in RoleData.GetClaimsForRole(rName))
                    {
                        await roleManager.AddClaimAsync(role, new Claim(pair.Key, pair.Value));
                    }
                }
                else
                {
                    var role = await roleManager.FindByNameAsync(rName);
                    var claims = await roleManager.GetClaimsAsync(role);

                    foreach (var kv in RoleData.GetClaimsForRole(rName))
                    {
                        if (claims.Any(x => x.Type == kv.Key && x.Value == kv.Value))
                            continue;
                        else
                            await roleManager.AddClaimAsync(role, new Claim(kv.Key, kv.Value));
                    }
                }
            }
        }

        private async Task UpdateUserRecordsAsync(UserRecord record)
        {
            var userManager = _serviceProvider.GetService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync(record.Login);

            if (user == null)
            {
                user = new AppUser { UserName = record.Login, Email = record.Email, RegistrationDate = DateTime.Now, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, record.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Created new user: {record.Login}");
                }
                else
                {
                    _logger.LogError("Cannot create user for: " + record.Login);
                    foreach (var e in result.Errors)
                    {
                        _logger.LogError($"Code: {e.Code} Description: {e.Description}");
                    }
                }
                await UpdateUserRolesAsync(user, userManager, record.Roles);
            }
            else
            {
                await UpdateUserRolesAsync(user, userManager, record.Roles);
            }
        }

        private async Task UpdateUserRolesAsync(AppUser user, UserManager<AppUser> userManager, string[] roles)
        {
            List<string> rList = new List<string>();
            foreach (string role in roles)
            {
                if (!(await userManager.IsInRoleAsync(user, role)))
                {
                    rList.Add(role);
                }
            }
            await userManager.AddToRolesAsync(user, rList);
        }

        #endregion
    }
}
