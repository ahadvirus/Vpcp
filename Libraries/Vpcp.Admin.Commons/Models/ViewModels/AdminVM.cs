using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Vpcp.Admin.Commons.Infrastructures;
using AdminEntity = Vpcp.Kernel.Models.Entities.Admin;

namespace Vpcp.Admin.Commons.Models.ViewModels
{
    public class AdminVM : ClaimObservableVM<AdminEntity>
    {
        public static AdminVM Instance()
        {
            List<AdminEntity> claims = new List<AdminEntity>();

            foreach (PropertyInfo property in typeof(AdminVM).GetProperties())
            {
                claims.Add(new AdminEntity() { Key = property.Name, Value = string.Empty });
            }

            return new AdminVM(claims);
        }

        public AdminVM(List<AdminEntity> claims) : base(claims)
        {
        }

        private string _name;

        [Required]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PropertyChanged(nameof(Name));
            }
        }

        private string _family;
        [Required]
        public string Family
        {
            get
            { 
                return _family; 
            }
            set
            {
                _family = value;
                PropertyChanged(nameof(Family));
            }
        }

        private string _company;
        [Required]
        public string Company
        {
            get
            { 
                return _company; 
            }
            set
            {
                _company = value;
                PropertyChanged(nameof(Company));
            }
        }

        private string _vpnName;
        [Required]
        public string VpnName
        {
            get
            {
                return _vpnName;
            }
            set
            {
                _vpnName = value;
                PropertyChanged(nameof(VpnName));
            }
        }

        private string _startDate;

        public string StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                PropertyChanged(nameof(StartDate));
            }
        }

        private int _credit;
        
        public int Credit
        {
            get
            {
                return _credit;
            }
            set
            {
                _credit = value;
                PropertyChanged(nameof(Credit));
            }
        }

        private string _mobile;
        
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                PropertyChanged(nameof(Mobile));
            }
        }

        private string _userName;
        
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                PropertyChanged(nameof(UserName));
            }
        }

        private string _adminId;

        public string AdminId
        {
            get
            {
                return _adminId;
            }
            set
            {
                _adminId = value;
                PropertyChanged(nameof(AdminId));
            }
        }
    }
}
