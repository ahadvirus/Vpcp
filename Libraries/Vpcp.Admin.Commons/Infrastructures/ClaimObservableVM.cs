using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Admin.Commons.Infrastructures
{
    public abstract class ClaimObservableVM<TClass> where TClass : IClaim
    {
        protected readonly List<ClaimObjectVM<TClass>> _data;

        public ClaimObservableVM(List<TClass> claims)
        {
            _data = new List<ClaimObjectVM<TClass>>();

            foreach (PropertyInfo property in typeof(TClass).GetProperties())
            {
                TClass? claim = claims.Where(t => string.Compare(t.Key.ToLower(), property.Name.ToLower(), StringComparison.CurrentCultureIgnoreCase) == 0)
                    .FirstOrDefault();

                if(claim != null && !string.IsNullOrEmpty(claim.Value))
                {
                    property.SetValue(this, Convert.ChangeType(claim.Value, property.PropertyType));
                    _data.Add(new ClaimObjectVM<TClass>(property.Name, claim));
                }
            }
        }

        public Task<List<TClass>> PropertyChanged()
        {
            return Task.Run(() => _data.Where(t => t.Changed).Select(t => t.Claim).ToList());
        }

        public void PropertyChanged(string name)
        {
            PropertyInfo? property = GetType().GetProperty(name);
            if (property != null)
            {
                foreach (ClaimObjectVM<TClass> claim in _data)
                {
                    if (string.Compare(claim.Key.ToLower(), name.ToLower(), StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        string? value = Convert.ToString(property.GetValue(this));
                        claim.Changed = true;
                        claim.Claim.Value = string.IsNullOrEmpty(value) ? string.Empty : value;
                        break;
                    }
                }
            }
        }
    }
}
