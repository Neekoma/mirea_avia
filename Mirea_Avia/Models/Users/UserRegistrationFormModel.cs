namespace Mirea_Avia.Models.Users
{
    public class UserRegistrationFormModel
    {
        public string? Username { get; set; }

        public string? Login { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? RepeatedPassword { get; set; }

        public bool ServiceAgreement { get; set; }
    }
}
