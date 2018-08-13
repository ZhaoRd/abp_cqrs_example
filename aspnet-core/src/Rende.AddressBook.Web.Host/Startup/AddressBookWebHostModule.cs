using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Rende.AddressBook.Configuration;

namespace Rende.AddressBook.Web.Host.Startup
{
    [DependsOn(
       typeof(AddressBookWebCoreModule))]
    public class AddressBookWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AddressBookWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AddressBookWebHostModule).GetAssembly());
        }
    }
}
