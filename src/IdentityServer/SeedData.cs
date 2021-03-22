// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;//get current assembly
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(connectionString,
                             b => b.MigrationsAssembly(migrationAssembly)));//migrationAssembly or IdentityServerAspNetIdentity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var alice = userMgr.FindByNameAsync("alice").Result;
                    var aliceClaim = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Gender,"女人"),
                        new Claim(JwtClaimTypes.Role,"Student"),
                        new Claim(JwtClaimTypes.Address,"重庆"),
                    };
                    if (alice == null)
                    {
                        alice = new ApplicationUser
                        {
                            UserName = "alice",
                            Email = "AliceSmith@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, aliceClaim).GetAwaiter().GetResult();
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("alice created");
                    }
                    else
                    {
                        Log.Information("alice already exists");
                        Log.Information("updating alice...");
                        var result = userMgr.RemoveClaimsAsync(alice, userMgr.GetClaimsAsync(alice).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                        var resultAddClaim = userMgr.AddClaimsAsync(alice, aliceClaim).GetAwaiter().GetResult();
                        if (!result.Succeeded && !resultAddClaim.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("alice updated");
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    var bobClaim = new Claim[]
                    {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Gender,"男人"),
                            new Claim(JwtClaimTypes.Role,"Student"),
                            new Claim(JwtClaimTypes.Address,"四川"),
                    };
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            UserName = "bob",
                            Email = "BobSmith@email.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, bobClaim).GetAwaiter().GetResult();
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("bob created");
                    }
                    else
                    {
                        Log.Information("bob already exists");
                        Log.Information("updating bob...");
                        var result = userMgr.RemoveClaimsAsync(bob, userMgr.GetClaimsAsync(bob).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                        var resultAddClaim = userMgr.AddClaimsAsync(bob, bobClaim).GetAwaiter().GetResult();
                        if (!result.Succeeded && !resultAddClaim.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("bob updated");
                    }

                    var teacher1 = userMgr.FindByNameAsync("teacher1").Result;
                    var teacher1Claim = new Claim[]
                    {
                            new Claim(JwtClaimTypes.Name, "Teacher Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Teacher"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("course", "语文"),
                            new Claim(JwtClaimTypes.Gender,"男人"),
                            new Claim(JwtClaimTypes.Role,"Teacher"),
                            new Claim(JwtClaimTypes.Address,"四川"),
                    };
                    if (teacher1 == null)
                    {
                        teacher1 = new ApplicationUser
                        {
                            UserName = "teacher1",
                            Email = "TeacherSmith@email.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(teacher1, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(teacher1, teacher1Claim).GetAwaiter().GetResult();
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("Teacher created");
                    }
                    else
                    {
                        Log.Information("Teacher already exists");
                        Log.Information("updating Teacher...");
                        var result = userMgr.RemoveClaimsAsync(bob, userMgr.GetClaimsAsync(bob).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                        var resultAddClaim = userMgr.AddClaimsAsync(bob, bobClaim).GetAwaiter().GetResult();
                        if (!result.Succeeded && !resultAddClaim.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("Teacher updated");
                    }

                    var teacher2 = userMgr.FindByNameAsync("teacher2").Result;
                    var teacher2Claim = new Claim[]
                    {
                            new Claim(JwtClaimTypes.Name, "teacher2 Smith"),
                            new Claim(JwtClaimTypes.GivenName, "teacher2"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("course", "数学"),
                            new Claim(JwtClaimTypes.Gender,"男人"),
                            new Claim(JwtClaimTypes.Role,"Teacher"),
                            new Claim(JwtClaimTypes.Address,"四川"),
                    };
                    if (teacher2 == null)
                    {
                        teacher2 = new ApplicationUser
                        {
                            UserName = "teacher2",
                            Email = "teacher2Smith@email.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(teacher2, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(teacher2, teacher2Claim).GetAwaiter().GetResult();
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("teacher2 created");
                    }
                    else
                    {
                        Log.Information("teacher2 already exists");
                        Log.Information("updating teacher2...");
                        var result = userMgr.RemoveClaimsAsync(bob, userMgr.GetClaimsAsync(bob).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                        var resultAddClaim = userMgr.AddClaimsAsync(bob, bobClaim).GetAwaiter().GetResult();
                        if (!result.Succeeded && !resultAddClaim.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Information("teacher2 updated");
                    }
                }
            }
        }
    }
}
