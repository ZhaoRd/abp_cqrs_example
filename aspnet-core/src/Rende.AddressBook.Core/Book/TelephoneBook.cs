namespace Rende.AddressBook.Book
{
    using System;

    using Abp.Domain.Entities.Auditing;

    using JetBrains.Annotations;

    /// <summary>
    /// 电话簿
    /// </summary>
    public class TelephoneBook : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 初始化<see cref="TelephoneBook"/>实例
        /// </summary>
        public TelephoneBook()
        {

        }

        /// <summary>
        /// 初始化<see cref="TelephoneBook"/>实例
        /// </summary>
        public TelephoneBook([NotNull]string name, string emailAddress, string tel)
        {
            this.Name = name;
            this.EmailAddress = emailAddress;
            this.Tel = tel;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress { get; protected set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Tel { get; protected set; }

        /// <summary>
        /// 修改联系方式
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="tel"></param>
        public void Change(string emailAddress,string tel)
        {
            this.EmailAddress = emailAddress;
            this.Tel = tel;
        }

        /// <summary>
        /// 修改姓名
        /// </summary>
        /// <param name="name"></param>
        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}