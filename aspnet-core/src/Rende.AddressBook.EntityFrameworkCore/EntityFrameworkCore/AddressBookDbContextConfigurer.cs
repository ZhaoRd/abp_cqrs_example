using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Rende.AddressBook.EntityFrameworkCore
{
    public static class AddressBookDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AddressBookDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AddressBookDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
