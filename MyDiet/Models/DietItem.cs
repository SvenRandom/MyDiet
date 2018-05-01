using System;
using Newtonsoft.Json;

namespace MyDiet.Models
{
    public class DietItem
    {
             
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		[JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

		[JsonProperty(PropertyName = "Description")]
        public String Description { get; set; }

		[JsonProperty(PropertyName = "Calories")]
        public int Calories { get; set; }
        
		[JsonProperty(PropertyName = "Image0")]
        public String Image0 { get; set; }

		[JsonProperty(PropertyName = "Image1")]
        public String Image1 { get; set; }

		[JsonProperty(PropertyName = "Image2")]
        public String Image2 { get; set; }


		[JsonProperty(PropertyName = "Date")]
		public DateTime Date { get; set; }

		[JsonProperty(PropertyName = "Time")]
		public TimeSpan Time { get; set; }
        
		public void SetTime()
        {

            Date = Date.AddHours(Time.Hours);
            Date = Date.AddMinutes(Time.Minutes);

        }
    }
}
