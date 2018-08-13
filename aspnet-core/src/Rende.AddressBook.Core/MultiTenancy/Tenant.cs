using Abp.MultiTenancy;
using Rende.AddressBook.Authorization.Users;

namespace Rende.AddressBook.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
