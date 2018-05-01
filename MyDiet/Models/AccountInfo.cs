using System;
using Newtonsoft.Json;

namespace MyDiet.Models
{
    public class AccountInfo
    {
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		[JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

		[JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

		[JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

		[JsonProperty(PropertyName = "Gender")]
        public string Gender { get; set; }

		[JsonProperty(PropertyName = "Height")]
        public int Height { get; set; }

		[JsonProperty(PropertyName = "Weight")]
        public int Weight { get; set; }

		[JsonProperty(PropertyName = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

		[JsonProperty(PropertyName = "typeOfCuisine")]
		public string typeOfCuisine { get; set; }


    }
}
