using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Databases.Contexts;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Databases.Seeds;
using Vpcp.Kernel.Extensions.DbFunctions;
using Vpcp.Kernel.Models.Databases;
using Vpcp.Kernel.Models.DataObjects;
using Vpcp.Kernel.Models.Entities;
using Vpcp.Kernel.Models.Enums;
using Vpcp.Panel.Api.Kernel.Databases.Contexts;

namespace Vpcp.Panel.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _repository;
    private readonly KernelContext _kernelContext;
    private readonly IdentityContext _identityContext;

    public AdminController(KernelContext kernelContext, IdentityContext identityContext, IAdminRepository repository)
    {
        _kernelContext = kernelContext;
        _identityContext = identityContext;
        _repository = repository;
    }


    [HttpGet("[action]")]
    public async Task<ActionResult<List<AdminDTO>>> SeedAdmin(CancellationToken cancellation)
    {
        AdminSeed seed = new AdminSeed();
        await seed.Invoke(_repository);
        List<AdminDTO> result = new List<AdminDTO>();

        IEnumerable<IGrouping<Guid, Admin>> admins = await _repository.GetAdminsByNameAsync(string.Empty, cancellation);

        foreach (IGrouping<Guid, Admin> admin in admins)
        {
            AdminDTO entity = new AdminDTO();

            foreach (Admin claim in admin)
            {
                PropertyInfo? property = typeof(AdminDTO).GetProperty(claim.Key);

                if (property != null)
                {
                    try
                    {
                        if (property.PropertyType != typeof(Guid?))
                        {
                            property.SetValue(entity,
                                //Convert.ChangeType(claim.Value, property.PropertyType, CultureInfo.CurrentCulture));
                                TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(claim.Value)
                            );
                        }

                        if (property.PropertyType.IsGenericType &&
                            property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            property.SetValue(
                                entity,
                                string.IsNullOrEmpty(claim.Value)
                                    ? null
                                    : TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(claim.Value)
                            );
                        }
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Property = property.Name, ErrorMessage = e.Message });
                    }
                }
            }

            result.Add(entity);
        }

        return Ok(result);
    }


    [HttpGet("[action]")]
    public string MaxQuery()
    {
        return _identityContext.Users.Select(user => EF.Functions.Max(user.Username)).ToQueryString();
    }

    [HttpGet("[action]")]
    public string CaseQuery()
    {
        //_repository.Query().Where()
        
        return _kernelContext.Admins
            .GroupBy(a => a.UserId,a => new AdminDTO()
            {
                Name = EF.Functions.Max(EF.Functions.Case(
                    Operator.Equal(a.Key, nameof(AdminDTO.Name)), 
                    a.Value,
                    null
                ))
            })
            .ToQueryString();
        
        /*
        return (from admin in _kernelContext.Admins
                group new AdminDTO()
                {
                    Name = EF.Functions.Max(
                        EF.Functions.Case(
                            Operator.Equal(admin.Key, nameof(AdminDTO.Name)), admin.Value, null
                            )
                        )
                } by admin.UserId).ToQueryString();
        */
        //return _kernelContext.Admins.GroupBy(admin => admin.UserId).SelectMany(admin => admin).ToQueryString();
    }
}