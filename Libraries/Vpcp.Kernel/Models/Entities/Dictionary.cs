using System;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Models.Entities;

public class Dictionary : IEntity<Guid>
{
    public Dictionary()
    {
        Word = string.Empty;
    }

    public Guid Id { get; set; }
    public string Word { get; set; }
    public bool Active { get; set; }
}