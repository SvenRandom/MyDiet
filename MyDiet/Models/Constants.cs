using System;
namespace MyDiet.Models
{
    public class Constants
    {
        public static string Username = "123@163.com";
        public static string Password = "123123";
        public static string Email = "123@163.com";
		public static readonly string EndpointUri = "https://byan8307.documents.azure.com:443/";
		public static readonly string PrimaryKey = "PX6M7QngYfyvhJvMunl6peFDKN5GqONMeNMM1fnmbKlgBeXAMfpTVDDAh3ZZURXfwUKQ8NfgzFwUS45BlFAroA==";

		//public static readonly string DatabaseName = "TodoList";
  //      public static readonly string CollectionName = "TodoItems";
		//public static readonly string CollectionNameDiet = "DietItem";

        //use Azure mobile and blog storage
		public static string ApplicationURL = @"https://mydietappversion2.azurewebsites.net";
		public const string StorageConnection = "DefaultEndpointsProtocol=https;AccountName=mydietstorageforimages;AccountKey=3LRQGh7vafGp0fCwrG1LyKblH85RKgp5axt2Luu55x4T0dB/VnH+nVczoyuIuAZwSVTOv+UBOFWrJUOogVCXKQ==;EndpointSuffix=core.windows.net";

        //can I eat it API 
		public static readonly string BarcodeEndpointUri = "http://supermarketownbrandguide.co.uk/api/newfeed.php";

		public static readonly string APIKey ="H64a7BuVGPWfwzGCJvGr";
    }
}
