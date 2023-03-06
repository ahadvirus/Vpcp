using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Models.Contracts;
using Vpcp.Kernel.Models.DataObjects;
using Vpcp.Kernel.Models.Entities;

namespace Vpcp.Kernel.Databases.Seeds;

public class AdminSeed : ISeed<Admin, Guid>
{
    public async Task Invoke(IRepository<Admin, Guid> repository)
    {
        if (await repository.Query().AnyAsync())
        {
            return;
        }

        Guid super = await GenerateIdAsync(repository);
        
        List<AdminDTO> admins = new List<AdminDTO>()
        {

            new AdminDTO()
            {
                Id = super,
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
                Id = await GenerateIdAsync(repository),
                Name = "Ahad",
                Family = "Abbasi",
                Company = "IWave",
                VpnName = "iwave",
                CreationDate = DateTime.Now.AddMinutes(10),
                Email = "ahadabbasi@gmail.com",
                Mobile = "09120276307",
                Username = "ahadabbasi",
                AdminId = super
            },
            new AdminDTO()
            {
                Id = await GenerateIdAsync(repository),
                Name = "Mehdi",
                Family = "Bazgard",
                Company = "Int",
                VpnName = "int",
                CreationDate = DateTime.Now.AddMinutes(20),
                Email = "bazgard@hotmail.com",
                Mobile = "09375163298",
                Username = "persia",
                AdminId = super
            },
            new AdminDTO()
            {
                Id = await GenerateIdAsync(repository),
                Name = "Mohammad",
                Family = "Yousefi",
                Company = "Insert",
                VpnName = "insert",
                CreationDate = DateTime.Now.AddMinutes(30),
                Email = "krm.insert@yahoo.com",
                Mobile = "09120276307",
                Username = "insert",
                AdminId = super
            }
        };

        foreach (AdminDTO admin in admins)
        {
            Guid userId = admin.Id;

            foreach (PropertyInfo property in typeof(AdminDTO).GetProperties())
            {
                string? value = Convert.ToString(property.GetValue(admin));

                Admin entity =  new Admin()
                {
                    Id = await GenerateIdAsync(repository),
                    Key = property.Name,
                    Value = !string.IsNullOrEmpty(value) ? value : string.Empty,
                    UserId = userId
                };

                await repository.CreateAsync(entity);
            }
            
        }
    }

    private async Task<Guid> GenerateIdAsync(IRepository<Admin, Guid> repository)
    {
        Guid result;

        do
        {
            result = Guid.NewGuid();
        } while (await repository.Query().AnyAsync(admin => admin.UserId == result));

        return result;
    }
}