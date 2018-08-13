namespace Rende.AddressBook.Tests.TelephoneBooks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;

    using Microsoft.EntityFrameworkCore;

    using Rende.AddressBook.Book;
    using Rende.AddressBook.Users.Dto;

    using Shouldly;

    using Xunit;

    public class TelephoneBookAppServiceTests : AddressBookTestBase
    {
        private readonly ITelephoneBookAppService _service;

        public TelephoneBookAppServiceTests()
        {
            _service = Resolve<ITelephoneBookAppService>();
        }

        /// <summary>
        /// 获取所有看通讯录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllTelephoneBookList_Test()
        {
            // Act
            var output = await _service.GetAllTelephoneBookList();

            // Assert
            output.Count().ShouldBe(0);
        }

        /// <summary>
        /// 创建通讯录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateTelephoneBook_Test()
        {
            // Act
            await _service.CreateOrUpdate(
                new TelephoneBookDto()
                    {
                        EmailAddress = "yun.zhao@qq.com",
                        Name = "赵云",
                        Tel="12345678901"
                    });

            await UsingDbContextAsync(async context =>
                {
                    var zhaoyun = await context
                                           .TelephoneBooks
                                           .FirstOrDefaultAsync(u => u.Name == "赵云");
                    zhaoyun.ShouldNotBeNull();
                });
        }

        /// <summary>
        /// 更新通讯录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateTelephoneBook_Test()
        {
            // Act
            await _service.CreateOrUpdate(
                new TelephoneBookDto()
                    {
                        EmailAddress = "yun.zhao@qq.com",
                        Name = "赵云",
                        Tel = "12345678901"
                    });

            var zhaoyunToUpdate = await UsingDbContextAsync(async context =>
                {
                    var zhaoyun = await context
                                           .TelephoneBooks
                                           .FirstOrDefaultAsync(u => u.Name == "赵云");
                    return zhaoyun;
                });
            zhaoyunToUpdate.ShouldNotBeNull();

            await _service.CreateOrUpdate(
                new TelephoneBookDto()
                    {
                        Id = zhaoyunToUpdate.Id,
                        EmailAddress = "yun.zhao@sina.com",
                        Name = "赵云",
                        Tel = "12345678901"
                    });

            await UsingDbContextAsync(async context =>
                {
                    var zhaoyun = await context
                                      .TelephoneBooks
                                      .FirstOrDefaultAsync(u => u.Name == "赵云");
                    zhaoyun.ShouldNotBeNull();
                    zhaoyun.EmailAddress.ShouldBe("yun.zhao@sina.com");
                });

        }

        /// <summary>
        /// 删除通讯录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTelephoneBook_Test()
        {
            // Act
            await _service.CreateOrUpdate(
                new TelephoneBookDto()
                    {
                        EmailAddress = "yun.zhao@qq.com",
                        Name = "赵云",
                        Tel = "12345678901"
                    });

            var zhaoyunToDelete = await UsingDbContextAsync(async context =>
                {
                    var zhaoyun = await context
                                      .TelephoneBooks
                                      .FirstOrDefaultAsync(u => u.Name == "赵云");
                    return zhaoyun;
                });
            zhaoyunToDelete.ShouldNotBeNull();

            await _service.Delete(
                new EntityDto<Guid>()
                    {
                        Id = zhaoyunToDelete.Id
                    });

            await UsingDbContextAsync(async context =>
                {
                    var zhaoyun = await context
                                      .TelephoneBooks
                                      .Where(c=>c.IsDeleted == false)
                                      .FirstOrDefaultAsync(u => u.Name == "赵云");
                    zhaoyun.ShouldBeNull();
                });

        }

    }
}