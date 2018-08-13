using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Rende.AddressBook.Configuration.Dto;

namespace Rende.AddressBook.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AddressBookAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
