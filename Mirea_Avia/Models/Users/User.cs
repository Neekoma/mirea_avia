﻿namespace Mirea_Avia.Models.Users
{
    /**
     * <summary>Класс модели данных пользователя</summary>
     */
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
