using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Vpcp.Admin.Data;
using Vpcp.Kernel.Database.Contexts;
using Vpcp.Kernel.Models.Contracts;
using Vpcp.Kernel.Models.DataObjects;
using AdminEntity = Vpcp.Kernel.Models.Entities.Admin;

namespace Vpcp.Admin
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Startup.ConfigurationService(builder.Services, builder.Configuration);

            WebApplication app = builder.Build();

            Startup.Configuration(app);

            try
            {

                using (IServiceScope scope = app.Services.CreateScope())
                {
                    
                    IdentityContext? identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();

                    if (identityContext != null)
                    {
                        await identityContext.Database.MigrateAsync();
                    }


                    KernelContext? kernelContext = scope.ServiceProvider.GetRequiredService<KernelContext>();

                    if (kernelContext != null)
                    {
                        await kernelContext.Database.MigrateAsync();

                        Guid super = Guid.NewGuid();


                        List<AdminDTO> admins = new List<AdminDTO>()
                        {
                            new AdminDTO()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Masih",
                                Family = "Nazari",
                                Company = "Nashenas",
                                VpnName = "nashenas",
                                CreationDate = DateTime.Now,
                                Email = "masih.enter@gmail.com",
                                Mobile = "09112546315",
                                Username = "admin",
                                AdminId = null
                            },
                            new AdminDTO()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Ahad",
                                Family = "Abbasi",
                                Company = "IWave",
                                VpnName = "iwave",
                                CreationDate = DateTime.Now,
                                Email = "ahadabbasi@gmail.com",
                                Mobile = "09120276307",
                                Username = "ahadabbasi",
                                AdminId = super
                            },
                            new AdminDTO()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Mehdi",
                                Family = "Bazgard",
                                Company = "Int",
                                VpnName = "int",
                                CreationDate = DateTime.Now,
                                Email = "bazgard@hotmail.com",
                                Mobile = "09375163298",
                                Username = "persia",
                                AdminId = super
                            },
                            new AdminDTO()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Mohammad",
                                Family = "Yousefi",
                                Company = "Insert",
                                VpnName = "insert",
                                CreationDate = DateTime.Now,
                                Email = "krm.insert@yahoo.com",
                                Mobile = "09120276307",
                                Username = "insert",
                                AdminId = super
                            }
                        };

                        if (!await kernelContext.Admins.AnyAsync())
                        {
                            foreach (AdminDTO admin in admins)
                            {
                                List<AdminEntity> entities = new List<AdminEntity>();

                                Guid userId = admin.Id;

                                foreach (PropertyInfo property in typeof(AdminDTO).GetProperties())
                                {
                                    string? value = Convert.ToString(property.GetValue(admin));

                                    entities.Add(new AdminEntity()
                                    {
                                        Id = await Generator(kernelContext.Admins),
                                        Key = property.Name,
                                        Value = !string.IsNullOrEmpty(value) ? value : string.Empty,
                                        UserId = userId
                                    });
                                }

                                await kernelContext.Admins.AddRangeAsync(entities);
                            }

                            await kernelContext.SaveChangesAsync();

                            ConsoleColor defaultColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Done");
                            Console.ForegroundColor = defaultColor;
                        }

                    }

                }

            }
            catch (Exception e)
            {
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = defaultColor;
                Debug.WriteLine(e.Message);
                //throw;
            }

            app.Run();

        }

        private static async Task<Guid> Generator<T>(DbSet<T> set) where T : class, IEntity
        {
            Guid result;

            do
            {
                result = Guid.NewGuid();
            } while (await set.AnyAsync(model => model.Id == result));

            return result;
        }
    }
}