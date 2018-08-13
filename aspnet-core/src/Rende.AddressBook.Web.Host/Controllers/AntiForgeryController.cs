using Microsoft.AspNetCore.Antiforgery;
using Rende.AddressBook.Controllers;

namespace Rende.AddressBook.Web.Host.Controllers
{
    public class AntiForgeryController : AddressBookControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
