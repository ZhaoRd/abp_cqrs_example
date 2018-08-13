namespace Rende.AddressBook.Book
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AutoMapper;
    using Abp.Cqrs.Commands;
    using Abp.Domain.Repositories;

    /// <summary>
    /// 实现电话簿应用层服务
    /// </summary>
    public class TelephoneBookAppService : AddressBookAppServiceBase, ITelephoneBookAppService
    {
        /// <summary>
        /// 命令总线
        /// </summary>
        private readonly ICommandBus _commandBus;

        /// <summary>
        /// 电话簿仓储
        /// </summary>
        private readonly IRepository<TelephoneBook, Guid> _telephoneBookRepository;

        /// <summary>
        /// 初始化<see cref="TelephoneBookAppService"/>实例
        /// </summary>
        /// <param name="commandBus"></param>
        /// <param name="telephoneBookRepository"></param>
        public TelephoneBookAppService(ICommandBus commandBus, IRepository<TelephoneBook, Guid> telephoneBookRepository)
        {
            this._commandBus = commandBus;
            this._telephoneBookRepository = telephoneBookRepository;
        }

        /// <summary>
        /// 创建或更新电话簿信息,使用命令
        /// </summary>
        /// <param name="telephoneBook"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(TelephoneBookDto telephoneBook)
        {
            if (!telephoneBook.Id.HasValue)
            {
                await this._commandBus.Send(new CreateTelephoneBookCommand(telephoneBook));
                return;
            }

            await this._commandBus.Send(new UpdateTelephoneBookCommand(telephoneBook));
        }

        /// <summary>
        /// 根据id获取电话簿
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<TelephoneBookDto> GetForEdit(EntityDto<Guid?> entityId)
        {
            var result = new TelephoneBookDto();
            if (!entityId.Id.HasValue)
            {
                return result;
            }

            var entity = await this._telephoneBookRepository.GetAsync(entityId.Id.Value);
            return this.ObjectMapper.Map<TelephoneBookDto>(entity);
        }

        /// <summary>
        /// 删除电话簿
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task Delete(EntityDto<Guid> entityId)
        {
            await this._commandBus.Send(new DeleteTelephoneBookCommand(entityId));
        }

        /// <summary>
        /// 获取电话簿列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TelephoneBookListDto>> GetAllTelephoneBookList()
        {
            var list = await this._telephoneBookRepository.GetAllListAsync();
            return this.ObjectMapper.Map<IEnumerable<TelephoneBookListDto>>(list);
            // return list.MapTo<IEnumerable<TelephoneBookListDto>>();
        }
    }
}