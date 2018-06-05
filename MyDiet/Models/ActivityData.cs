using System;
using Newtonsoft.Json;

namespace MyDiet.Models
{
    public class ActivityData
    {
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "walkedkm")]
		public double walkedkm { get; set; }

        [JsonProperty(PropertyName = "steps")]
		public int steps { get; set; }

		[JsonProperty(PropertyName = "climbedFloor")]
		public int climbedFloor { get; set; }

		[JsonProperty(PropertyName = "date")]
		public DateTime date { get; set; }


    }
}
