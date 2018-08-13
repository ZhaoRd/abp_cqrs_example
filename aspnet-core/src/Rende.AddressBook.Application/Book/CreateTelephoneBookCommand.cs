namespace Rende.AddressBook.Book
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AutoMapper;
    using Abp.Cqrs.Commands;
    using Abp.Cqrs.Events;
    using Abp.Domain.Repositories;

    using MediatR;

    /// <summary>
    /// 创建电话簿命令
    /// </summary>
    public class CreateTelephoneBookCommand:Command
    {
        public TelephoneBookDto TelephoneBook { get;private set; }

        public CreateTelephoneBookCommand(TelephoneBookDto book)
        {
            this.TelephoneBook = book;
        }
    }

    /// <summary>
    /// 更新电话命令
    /// </summary>
    public class UpdateTelephoneBookCommand : Command
    {
        public TelephoneBookDto TelephoneBook { get; private set; }

        public UpdateTelephoneBookCommand(TelephoneBookDto book)
        {
            this.TelephoneBook = book;
        }
    }

    /// <summary>
    /// 删除电话簿命令
    /// </summary>
    public class DeleteTelephoneBookCommand : Command
    {
        public EntityDto<Guid> TelephoneBookId { get; private set; }

        public DeleteTelephoneBookCommand(EntityDto<Guid> id)
        {
            this.TelephoneBookId = id;
        }
    }
    
    /// <summary>
    /// 更新电话簿命令处理
    /// </summary>
    public class UpdateTelephoneBookCommandHandler : ICommandHandler<UpdateTelephoneBookCommand>
    {
        
        private readonly IRepository<TelephoneBook, Guid> _telephoneBookRepository;

        public UpdateTelephoneBookCommandHandler(IRepository<TelephoneBook, Guid> telephoneBookRepository)
        {
            this._telephoneBookRepository = telephoneBookRepository;
        }

        public async Task<Unit> Handle(UpdateTelephoneBookCommand request, CancellationToken cancellationToken)
        {
            var tenphoneBook = await this._telephoneBookRepository.GetAsync(request.TelephoneBook.Id.Value);
            tenphoneBook.Change(request.TelephoneBook.EmailAddress,request.TelephoneBook.Tel);
            return Unit.Value;
        }
    }

    /// <summary>
    /// 删除电话簿命令
    /// </summary>
    public class DeleteTelephoneBookCommandHandler : ICommandHandler<DeleteTelephoneBookCommand>
    {
        private readonly IRepository<TelephoneBook, Guid> _telephoneBookRepository;
        public DeleteTelephoneBookCommandHandler(
            IRepository<TelephoneBook, Guid> telephoneBookRepository)
        {
            this._telephoneBookRepository = telephoneBookRepository;
        }

        public async Task<Unit> Handle(DeleteTelephoneBookCommand request, CancellationToken cancellationToken)
        {
            await this._telephoneBookRepository.DeleteAsync(request.TelephoneBookId.Id);

            return Unit.Value;
        }
    }

    /// <summary>
    /// 创建电话簿命令
    /// </summary>
    public class CreateTelephoneBookCommandHandler : ICommandHandler<CreateTelephoneBookCommand>
    {
        private readonly IRepository<TelephoneBook, Guid> _telephoneBookRepository;
        
        public CreateTelephoneBookCommandHandler(IRepository<TelephoneBook, Guid> telephoneBookRepository)
        {
            this._telephoneBookRepository = telephoneBookRepository;
        }

        public async Task<Unit> Handle(CreateTelephoneBookCommand request, CancellationToken cancellationToken)
        {
            var telephoneBook = new TelephoneBook(request.TelephoneBook.Name, request.TelephoneBook.EmailAddress, request.TelephoneBook.Tel);
            await this._telephoneBookRepository.InsertAsync(telephoneBook);
            
            return Unit.Value;
        }
    }


    [Abp.AutoMapper.AutoMap(typeof(TelephoneBook))]
    public class TelephoneBookDto : EntityDto<Guid?>
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress { get;  set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Tel { get;  set; }

    }

    [Abp.AutoMapper.AutoMap(typeof(TelephoneBook))]
    public class TelephoneBookListDto : FullAuditedEntityDto<Guid>
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Tel { get; set; }

    }

}