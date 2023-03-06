using System;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Models.Entities;

public class Admin : IClaim<Guid>
{
    public Admin()
    {
        Key = string.Empty;
        Value = string.Empty;
    }

    public Guid Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public Guid UserId { get; set; }
}