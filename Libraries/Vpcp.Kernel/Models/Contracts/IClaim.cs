namespace Vpcp.Kernel.Models.Contracts;

public interface IClaim<T> : IEntity<T> where T : struct
{
    string Key { get; set; }
    string Value { get; set; }
}