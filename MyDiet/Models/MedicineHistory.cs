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

		[JsonProperty(PropertyName = "Quantity")]
		public string Quantity { get; set; }

		[JsonProperty(PropertyName = "Unit")]
		public string Unit { get; set; }

		[JsonProperty(PropertyName = "TimeToDisplay")]
		public string TimeToDisplay { get; set; }

		[JsonProperty(PropertyName = "Checked")]
        public bool Checked { get; set; }
        

        [JsonProperty(PropertyName = "Time")]
		public DateTime Time { get; set; }
        
        public void SetTimeToDisplay()
        {
			TimeToDisplay = Time.ToString("HH:mm dd/MMM/yyyy dddd");
        }
        

    }
}
