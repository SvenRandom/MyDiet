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

		[JsonProperty(PropertyName = "DirectionsToDisplay")]
		public string DirectionsToDisplay { get; set; }

		[JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Duration")]
        public string Duration { get; set; }

        [JsonProperty(PropertyName = "TimesPerDay")]
        public int TimesPerDay { get; set; }

        [JsonProperty(PropertyName = "Unit")]
        public string Unit { get; set; }
        
		[JsonProperty(PropertyName = "StartTime")]
        public DateTime StartTime { get; set; }

		[JsonProperty(PropertyName = "TimeToDisplay")]
        public string TimeToDisplay { get; set; }

		[JsonProperty(PropertyName = "IsDone")]
		public bool IsDone { get; set; }

		[JsonProperty(PropertyName = "IsUnDone")]
		public bool IsUnDone { get; set; }

    }
}
