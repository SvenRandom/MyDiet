using System;
using Newtonsoft.Json;

namespace MyDiet.Models
{
	public class PackageFoodDatabase
    {
		//id is barcode
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
       
        [JsonProperty(PropertyName = "Weight")]
		public string Weight { get; set; }

		[JsonProperty(PropertyName = "Energy")]
		public string Energy { get; set; }

		[JsonProperty(PropertyName = "Protein")]
		public string Protein { get; set; }

		[JsonProperty(PropertyName = "Carbs")]
		public string Carbs { get; set; }
  
		[JsonProperty(PropertyName = "Sodium")]
		public string Sodium { get; set; }

		[JsonProperty(PropertyName = "Fat")]
		public string Fat { get; set; }

		[JsonProperty(PropertyName = "Satfat")]
		public string Satfat { get; set; }

		[JsonProperty(PropertyName = "Salt")]
		public string Salt { get; set; }

		[JsonProperty(PropertyName = "Sugar")]
		public string Sugar { get; set; }

		[JsonProperty(PropertyName = "Fibre")]
		public string Fibre { get; set; }

  
    }
}
