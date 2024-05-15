using System.ComponentModel.DataAnnotations;


namespace Mirea_Avia.Models.Users
{
    public class UserLoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
