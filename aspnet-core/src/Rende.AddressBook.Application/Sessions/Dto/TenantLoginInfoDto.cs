using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Rende.AddressBook.MultiTenancy;

namespace Rende.AddressBook.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
