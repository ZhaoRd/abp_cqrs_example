using Abp.Authorization;
using Rende.AddressBook.Authorization.Roles;
using Rende.AddressBook.Authorization.Users;

namespace Rende.AddressBook.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
