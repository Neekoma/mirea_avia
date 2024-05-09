using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace Mirea_Avia.Models.Users
{
    /**
     * <summary>Класс модели данных пользователя</summary>
     */
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
