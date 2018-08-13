using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Rende.AddressBook.Authorization.Users;
using Rende.AddressBook.Editions;

namespace Rende.AddressBook.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
