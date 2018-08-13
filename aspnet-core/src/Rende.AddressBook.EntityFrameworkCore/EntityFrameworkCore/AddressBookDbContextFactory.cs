using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Rende.AddressBook.Configuration;
using Rende.AddressBook.Web;

namespace Rende.AddressBook.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AddressBookDbContextFactory : IDesignTimeDbContextFactory<AddressBookDbContext>
    {
        public AddressBookDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AddressBookDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AddressBookDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AddressBookConsts.ConnectionStringName));

            return new AddressBookDbContext(builder.Options);
        }
    }
}
