namespace Vpcp.Kernel.Models.Contracts;

public interface IDataObject<TKey> where TKey : struct
{
    TKey Id { get; set; }
}