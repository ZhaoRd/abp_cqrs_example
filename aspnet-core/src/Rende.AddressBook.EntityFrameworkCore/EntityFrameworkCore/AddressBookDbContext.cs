using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Rende.AddressBook.Authorization.Roles;
using Rende.AddressBook.Authorization.Users;
using Rende.AddressBook.MultiTenancy;

namespace Rende.AddressBook.EntityFrameworkCore
{
    using Rende.AddressBook.Book;

    public class AddressBookDbContext : AbpZeroDbContext<Tenant, Role, User, AddressBookDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<TelephoneBook> TelephoneBooks { get; set; }
        
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options)
            : base(options)
        {
        }
    }
}
