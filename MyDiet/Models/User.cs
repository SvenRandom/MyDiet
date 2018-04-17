using System;
using SQLite;

namespace MyDiet.Models
{
    public class User
    {
        public User()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int UserId {get; set;  }

        [Unique]
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
