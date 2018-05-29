using System;
using Newtonsoft.Json;

namespace MyDiet.Models
{
    public class MedicineHistory
    {
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

		[JsonProperty(PropertyName = "MedicineName")]
		public string MedicineName { get; set; }
        
		[JsonProperty(PropertyName = "Directions")]
		public string Directions { get; set; }

		[JsonProperty(PropertyName = "TimeToDisplay")]
		public string TimeToDisplay { get; set; }
        
      
    }
}
