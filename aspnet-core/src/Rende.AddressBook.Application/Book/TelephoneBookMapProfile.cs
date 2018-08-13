namespace Rende.AddressBook.Book
{
    using AutoMapper;

    using Rende.AddressBook.Authorization.Users;
    using Rende.AddressBook.Users.Dto;

    public class TelephoneBookMapProfile : Profile
    {
        public TelephoneBookMapProfile()
        {
            CreateMap<TelephoneBook, TelephoneBookDto>();
            CreateMap<TelephoneBook, TelephoneBookListDto>();
            /*
            CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
            CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());*/
        }
    }
}
