using System;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Panel.Api.Kernel.Models.Entities;

public class User : IEntity<Guid>
{
    public User()
    {
        Mobile = string.Empty;
        Email = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
    }

    public Guid Id { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}