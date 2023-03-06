namespace Vpcp.Kernel.Models.Contracts;

public interface IEntity<T> where T : struct
{
    T Id { get; set; }
}