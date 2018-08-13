using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Rende.AddressBook.Authorization;

namespace Rende.AddressBook
{
    using Abp.Cqrs;

    [DependsOn(
        typeof(AddressBookCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AddressBookApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AddressBookAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AddressBookApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            this.IocManager.RegisterAssemblyCqrs<AddressBookApplicationModule>();

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );

            

        }
    }
}
