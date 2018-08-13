using System.Threading.Tasks;
using Rende.AddressBook.Configuration.Dto;

namespace Rende.AddressBook.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
