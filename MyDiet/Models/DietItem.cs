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
        
		[JsonProperty(PropertyName = "Image0UploadId")]
        public String Image0UploadId { get; set; }

		[JsonProperty(PropertyName = "Image1UploadId")]
		public String Image1UploadId { get; set; }

		[JsonProperty(PropertyName = "Image2UploadId")]
		public String Image2UploadId { get; set; }

		[JsonProperty(PropertyName = "Image0LocalPath")]
        public String Image0LocalPath { get; set; }

		[JsonProperty(PropertyName = "Image1LocalPath")]
		public String Image1LocalPath { get; set; }

		[JsonProperty(PropertyName = "Image2LocalPath")]
		public String Image2LocalPath { get; set; }

		[JsonProperty(PropertyName = "ScanItems")]
        public String ScanItems { get; set; }

		[JsonProperty(PropertyName = "Date")]
		public DateTime Date { get; set; }

		[JsonProperty(PropertyName = "DateToDisplay")]
		public string DateToDisplay { get; set; }

		//Date.ToString("yyyy-MM-dd HH:mm dddd")
		[JsonProperty(PropertyName = "Time")]
		public TimeSpan Time { get; set; }
        
		[JsonProperty(PropertyName = "Version")]
        public string Version { get; set; }

		public void SetTime()
        {

            Date = Date.AddHours(Time.Hours);
            Date = Date.AddMinutes(Time.Minutes);

        }

		public void SetDateToDisplay(){
			DateToDisplay = Date.ToString("HH:mm dd/MMM/yyyy dddd");
		}
    }
}
