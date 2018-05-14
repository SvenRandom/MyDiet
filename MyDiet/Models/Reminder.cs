using System;
using Newtonsoft.Json;

namespace MyDiet.Models
{
    public class Reminder
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

		[JsonProperty(PropertyName = "UnChecked")]
        public bool UnChecked { get; set; }

        [JsonProperty(PropertyName = "Time")]
        public TimeSpan Time { get; set; }
        
        public void SetTimeToDisplay()
        {
            TimeToDisplay = Time.ToString(@"hh\:mm");
        }

		public void SetUnChecked()
        {
			UnChecked=!Checked;
        }

    }
}
