using System.ComponentModel.DataAnnotations;

namespace Rende.AddressBook.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}