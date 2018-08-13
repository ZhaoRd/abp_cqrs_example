using System.Threading.Tasks;
using Abp.Application.Services;
using Rende.AddressBook.Sessions.Dto;

namespace Rende.AddressBook.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
