using Microsoft.AspNetCore.Identity;

namespace MoviesApp.Models
{
    //модель для дополнительных пользовательских полей: имя, фамилия
    public class ApplicationUser : IdentityUser //чтобы расширить данные
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}