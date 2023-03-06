using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Admin.Commons.Infrastructures
{
    public class ClaimObjectVM<T> where T : IClaim
    {
        public ClaimObjectVM(string key, T claim)
        {
            Key = key;
            Claim = claim;
            Changed = false;
        }

        public string Key { get; set; }
        public T Claim { get; set; }
        public bool Changed { get; set; }
    }
}
