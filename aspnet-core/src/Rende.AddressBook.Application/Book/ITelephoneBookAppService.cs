namespace Rende.AddressBook.Book
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    /// <summary>
    /// 定义电话簿应用层服务
    /// </summary>
    public interface ITelephoneBookAppService : IApplicationService
    {
        /// <summary>
        /// 创建或更新电话簿
        /// </summary>
        /// <param name="telephoneBook">电话簿</param>
        /// <returns></returns>
        Task CreateOrUpdate(TelephoneBookDto telephoneBook);

        /// <summary>
        /// 根据Id获取电话簿
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<TelephoneBookDto> GetForEdit(EntityDto<Guid?> entityId);

        /// <summary>
        /// 删除一个电话簿
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> entityId);

        /// <summary>
        /// 获取电话簿列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TelephoneBookListDto>> GetAllTelephoneBookList();
    }
}