using System;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Models.DataObjects;

public class AdminDTO : IDataObject<Guid>
{
    public Guid Id { get; set; }
        
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string? Mobile { get; set; }

    public string? Company { get; set; }
    public string? VpnName { get; set; }
        
    public DateTime CreationDate { get; set; }
        
    public string? Email { get; set; }
    public string? Username { get; set; }

    public Guid? AdminId { get; set; }

}