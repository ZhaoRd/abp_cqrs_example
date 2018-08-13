using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Rende.AddressBook.MultiTenancy.Dto;

namespace Rende.AddressBook.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
