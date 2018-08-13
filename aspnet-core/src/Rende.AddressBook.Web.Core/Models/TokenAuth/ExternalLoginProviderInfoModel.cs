using Abp.AutoMapper;
using Rende.AddressBook.Authentication.External;

namespace Rende.AddressBook.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
