using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using MyDiet.Helpers;
using Xamarin.Forms;
using Plugin.Media;
using ZXing.Mobile;
using Plugin.Media.Abstractions;
using System.IO;
using System.Reflection;
using ZXing.Net.Mobile.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using Org.Json;

namespace MyDiet.Views
{
    public partial class DietItemPage : ContentPage
    {
		


        bool isNewItem = false;
        public static DietManager dietManager { get; private set; }
        DietItem dietItemCurrent;
        private int numberOfPhoto = 0;
        private int existPhotos = -1;
        private bool temp = false;
        MediaFile file0 = null;
        MediaFile file1 = null;
        MediaFile file2 = null;
		List<string> items = new List<string>();
       
        public DietItemPage(DietItem dietItem)
        {
            InitializeComponent();
            temp = true;
            if (dietItem == null)
            {
                isNewItem = true;
                dietItemCurrent = new DietItem
                {
                    Id = Guid.NewGuid().ToString()

                };
                dietItemCurrent.Date = DateTime.Now;
                dietItemCurrent.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                dietItemCurrent.UserId = Settings.AccountEmail;
                dietItemCurrent.Image0LocalPath = null;
                dietItemCurrent.Image1LocalPath = null;
                dietItemCurrent.Image2LocalPath = null;
				dietItemCurrent.ScanItems = "";


            }

            else
            {
                dietItemCurrent = dietItem;
				if(dietItemCurrent.ScanItems!=""){
					string[] str = dietItemCurrent.ScanItems.Split(';');
					for (int i = 0; i < str.Length;i++){
						SetScanItems(str[i]);
						items.Add(str[i]);
					}

				}
				if (dietItemCurrent.Image0LocalPath != null)
                {
                    imagesStack.IsVisible = true;

                }

            }
            

            BindingContext = dietItemCurrent;
            dietManager = DietManager.DefaultManager;

        }

        protected override void OnAppearing()
        {
            if (temp)
            {
                if (dietItemCurrent.Image0LocalPath != null)
                {
                    activityIndicator.IsRunning = true;
                    imagesStack.IsVisible = true;
                    numberOfPhoto = 1;
                    var fileSoure = ImageSource.FromFile(dietItemCurrent.Image0LocalPath);
                    //if(fileSoure.ToString()!="")
                    image00.Source = fileSoure;
                    delete00.IsVisible = false;
					image00.IsVisible = true;
                    //else
                    //{
                    //  var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image0UploadId);
                    //                   image00.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

                    //}

                    //var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image0UploadId);
                    //image0.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

                    if (dietItemCurrent.Image1LocalPath != null)
                    {
                        numberOfPhoto = 2;
                        image11.Source = ImageSource.FromFile(dietItemCurrent.Image1LocalPath);
                        delete11.IsVisible = false;
						image11.IsVisible = true;
                        //imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image1UploadId);
                        //image1.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

                        if (dietItemCurrent.Image2LocalPath != null)
                        {
                            numberOfPhoto = 3;
                            image22.Source = ImageSource.FromFile(dietItemCurrent.Image2LocalPath);
                            delete22.IsVisible = false;
							image22.IsVisible = true;
                            //imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image2UploadId);
                            //image2.Source = ImageSource.FromStream(() => new MemoryStream(imageData));


                        }
                    }
                }
                existPhotos = numberOfPhoto;
                activityIndicator.IsRunning = false;
                temp = false;
            }
        }

		async void OnSaveActivated(object sender, EventArgs e)
		{
			activityIndicator.IsRunning = true;

			cancelButton.IsEnabled = false;
            
			var size = items.Count;
			//System.Diagnostics.Debug.WriteLine("size: "+size);
			string str = "";
			if(size>0){
				for (int i = 0; i < size; i++)
                {
                    str = str + items[i] + ";";
                }
                str = str.Substring(0, str.Length - 2);
                dietItemCurrent.ScanItems = str;
			}


            //imagesPath[0]=await AzureStorage.UploadFileAsync(ContainerType.Image, stream);

            if (numberOfPhoto > 0 && existPhotos <= 0)
            {
                dietItemCurrent.Image0UploadId = await AzureStorage.UploadFileAsync(ContainerType.Image, file0.GetStream());

            }
            if (numberOfPhoto > 1 && existPhotos <= 1)
                dietItemCurrent.Image1UploadId = await AzureStorage.UploadFileAsync(ContainerType.Image, file1.GetStream());

            if (numberOfPhoto > 2 && existPhotos <= 2)
                dietItemCurrent.Image2UploadId = await AzureStorage.UploadFileAsync(ContainerType.Image, file2.GetStream());

            //var uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Image, file1.GetStream());
            //await DisplayAlert("File ID", uploadedFilename, "OK");
            dietItemCurrent.SetTime();
            dietItemCurrent.SetDateToDisplay();
            //file0.Dispose();
            await dietManager.SaveTaskAsync(dietItemCurrent, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "Are you sure to delete it?", "yes", "no");
            if (response)
            {
                await dietManager.DeleteTaskAsync(dietItemCurrent);
                await Navigation.PopAsync();
            }

        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "Are you sure to cancel?", "yes", "no");
            if (response)
            {
                await Navigation.PopAsync();
            }

        }


        async void AddPhotoClicked(object sender, System.EventArgs e)
        {
			if (numberOfPhoto >= 3)
			{
				await DisplayAlert("Notice!", ":( at most three photos.", "OK");

			}
			else
			{
				var response = await DisplayActionSheet("Choose resource", "Cancel", null, "Take a photo", "Choose from album");
				if (response == "Take a photo")
					TakePhotoClicked();
				if (response == "Choose from album")
					PickPhotoClicked();
			}
        }


        async void scanClicked(object sender, EventArgs e)
        {
			
            //MobileBarcodeScanner.Initialize(Application);
            await DisplayAlert("notice", "start scan carcode", "yes");

#if __ANDROID__
    // Initialize the scanner first so it can track the current context
            MobileBarcodeScanner.Initialize (Application);
#endif
            // Initialize the scanner first so it can track the current context
            //if (Device.RuntimePlatform == Device.Android)
            //        {
            //ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            //}
            var scanPage = new ZXingScannerPage();
            //string results="";
            scanPage.Title = "Please Scan barcode";
            scanPage.OnScanResult += (result) => {
                // stop scanning
                scanPage.IsScanning = false;

                // show scan result
                Device.BeginInvokeOnMainThread(() => {
                    App.barcode = result.Text;
                    //DisplayAlert("Scanned Barcode", result.Text, "OK");
                    //DisplayAlert("Scanned Barcode from APP", App.barcode, "OK");
                    Navigation.PopAsync();

                });
            };

            // go to scan page
            await Navigation.PushAsync(scanPage);

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (App.barcode == null)
                    return true;
                else
                {
                    var result = App.barcode;
                    bar.Text = result;
					SearchBarcodeAsync();
                    return false;
                }
            });
            
         

        }

		async void BarTestClicked(object sender, System.EventArgs e)
        {
			HttpClient _client = new HttpClient();
			string quary = Constants.BarcodeEndpointUri + "?json=barcode&q=" + "21045622" + "&apikey=" + Constants.APIKey;


            var response = await _client.GetAsync(quary);
            //var posts = JsonConvert.DeserializeObject<List<BarcodeItem>>(content);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject(content);
                System.Diagnostics.Debug.WriteLine("response:"+Items.ToString());
				System.Diagnostics.Debug.WriteLine("content: "+content);

                await DisplayAlert("barcode item length", content.Length.ToString(), "sure");
				var posts = JsonConvert.DeserializeObject<Items>(content);
				System.Diagnostics.Debug.WriteLine("title: " + posts.title);

            }

        }


		async void SearchBarcodeAsync()
		{
			try{
				
    			
    			activityIndicator.IsRunning = true;
               
    			var client = new RestClient("https://api.upcitemdb.com/prod/trial/");
                // lookup request with GET
    			RestSharp.Portable.IRestRequest request = new RestRequest("lookup", Method.GET);


    			request.AddQueryParameter("upc", App.barcode);
    			//request.AddQueryParameter("upc", "0000093613903");
    			//var response = await client.Execute(request);
    			var response =await client.Execute(request);
    			//System.Diagnostics.Debug.WriteLine("response: " + response.ToString());
    			//System.Diagnostics.Debug.WriteLine("response content: " + response.Content);
                // parsing json
    			var obj = JsonConvert.DeserializeObject<BarcodeItem>(response.Content);
                
    			var a = obj.items.ToString().Trim();
    			//var b = a.Substring(1,a.Length-2);

    			if(obj.total=="0"){
    				await DisplayAlert("notice", "no result", "Sure");
					activityIndicator.IsRunning = false;
    			}
    			else
    			{
    				var c = JsonConvert.DeserializeObject<List<Items>>(a);
                    var title = c[0].title;
    				await DisplayAlert("notice", title, "Sure");
    				items.Add(title);
					SetScanItems(title);

    				activityIndicator.IsRunning = false;

    			}

			}catch{
				await DisplayAlert("notice", "NetWork Error", "Sure");
			}
			//System.Diagnostics.Debug.WriteLine("obj: " + obj.items.ToString());
			//JSONObject a = new JSONObject(response.Content);
			//JsonObjectAttribute json = (Newtonsoft.Json.JsonObjectAttribute)obj;
			//System.Diagnostics.Debug.WriteLine("c: " + c[0].title);
			//JSONObject a =new JSONObject();
			//a.Put("a","b");
			//var c = a.GetString("a");
			//System.Diagnostics.Debug.WriteLine("a: " + a);
			//System.Diagnostics.Debug.WriteLine("obj total: " + obj.total);
     //**********************************       
			//HttpClient _client = new HttpClient();
   //         string quary = Constants.BarcodeEndpointUri + "?json=barcode&q=" + App.barcode + "&apikey=" + Constants.APIKey;


			//var response = await _client.GetAsync(quary);
			////var posts = JsonConvert.DeserializeObject<List<BarcodeItem>>(content);
			//if (response.IsSuccessStatusCode)
    //        {
    //            var content = await response.Content.ReadAsStringAsync();
				//var Items = JsonConvert.DeserializeObject(content);
				//System.Diagnostics.Debug.WriteLine("response:"+Items.ToString());
				//System.Diagnostics.Debug.WriteLine(content);

            //    await DisplayAlert("barcode item title", content.Length.ToString(), "sure");
            //}
			//var posts = JsonConvert.DeserializeObject<BarcodeItem>(content);



            App.barcode = null;

		}

        void OnImage00Tapped(object sender, System.EventArgs e)
        {
            oStackLayout.IsVisible = false;
            image00StackLayout.IsVisible = true;
            image00Display.Source = ImageSource.FromFile(dietItemCurrent.Image0LocalPath);
        }


        void OnImage00ComfirmClicked(object sender, System.EventArgs e)
        {
            oStackLayout.IsVisible = true;
            image00StackLayout.IsVisible = false;
        }

        void OnImage00DeleteClicked(object sender, System.EventArgs e)
        {
            if (file1 != null)
            {
                file0 = file1;
                image00.Source = ImageSource.FromFile(file0.Path);
                dietItemCurrent.Image0LocalPath = dietItemCurrent.Image1LocalPath;
                if (file2 != null)
                {
                    file1 = file2;

                    image11.Source = ImageSource.FromFile(file2.Path);
                    dietItemCurrent.Image1LocalPath = dietItemCurrent.Image2LocalPath;
                    file2 = null;
                    image22.Source = "";
					image22.IsVisible = false;
                    dietItemCurrent.Image2LocalPath = null;
                }
                else
                {
                    file1 = null;
                    image11.Source = "";
					image11.IsVisible = false;
                    dietItemCurrent.Image1LocalPath = null;
                }
            }
            else
            {
                file0 = null;
                image00.Source = "";
				image00.IsVisible = false;
                dietItemCurrent.Image0LocalPath = null;
                imagesStack.IsVisible = false;
            }
            numberOfPhoto--;

            oStackLayout.IsVisible = true;
            image00StackLayout.IsVisible = false;

        }

        void OnImage11Tapped(object sender, System.EventArgs e)
        {
            oStackLayout.IsVisible = false;
            image11StackLayout.IsVisible = true;
            image11Display.Source = ImageSource.FromFile(dietItemCurrent.Image1LocalPath);
        }


        void OnImage11ComfirmClicked(object sender, System.EventArgs e)
        {
            oStackLayout.IsVisible = true;
            image11StackLayout.IsVisible = false;
        }

        void OnImage11DeleteClicked(object sender, System.EventArgs e)
        {
            if (file2 != null)
            {
                file1 = file2;
                image11.Source = ImageSource.FromFile(file2.Path);
                dietItemCurrent.Image1LocalPath = dietItemCurrent.Image2LocalPath;
                file2 = null;
                image22.Source = "";
				image22.IsVisible = false;
                dietItemCurrent.Image2LocalPath = null;
            }
            else
            {
                file1 = null;
                image11.Source = "";
				image11.IsVisible = false;
                dietItemCurrent.Image1LocalPath = null;
            }
            numberOfPhoto--;

            oStackLayout.IsVisible = true;
            image11StackLayout.IsVisible = false;

        }


        void OnImage22Tapped(object sender, System.EventArgs e)
        {
            oStackLayout.IsVisible = false;
            image22StackLayout.IsVisible = true;
            image22Display.Source = ImageSource.FromFile(dietItemCurrent.Image2LocalPath);
        }


        void OnImage22ComfirmClicked(object sender, System.EventArgs e)
        {
            oStackLayout.IsVisible = true;
            image22StackLayout.IsVisible = false;
        }

        void OnImage22DeleteClicked(object sender, System.EventArgs e)
        {
            file2 = null;
            image22.Source = "";
			image22.IsVisible = false;
            numberOfPhoto--;

            oStackLayout.IsVisible = true;
            image22StackLayout.IsVisible = false;

        }




        async public void TakePhotoClicked()
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");

            }


            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {

                SaveToAlbum = true,
                PhotoSize = PhotoSize.MaxWidthHeight,
                CompressionQuality = 92,
                MaxWidthHeight = 600



            });

            if (file != null)
                SetStream(file);
            //else
            //await DisplayAlert("Error", "Something wrong", "Cancel");


        }

        async public void PickPhotoClicked()
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not available", ":( pick photo is not available.", "OK");

            }
            var photos = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                CompressionQuality = 92,
                MaxWidthHeight = 600

            });
            //image.Source = ImageSource.FromFile(photos.Path);
            if (photos != null)
                SetStream(photos);
            //else
            //await DisplayAlert("Error", "Something wrong", "Cancel");


        }

        public void SetStream(MediaFile file)
        {
            imagesStack.IsVisible = true;

            switch (numberOfPhoto)
            {
                case 0:
                    file0 = file;
                    //              image0.Source = ImageSource.FromStream(() =>
                    //              {                  
                    //var stream = file.GetStream();
                    //    //file.Dispose();
                    //    return stream;
                    //});
                    image00.Source = ImageSource.FromFile(file.Path);
					image00.IsVisible = true;
                    dietItemCurrent.Image0LocalPath = file.Path;
                    //stream0 = file.GetStream();
                    //dietItemCurrent.Image0 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream0);

                    break;
                case 1:
                    file1 = file;
                    //              image1.Source = ImageSource.FromStream(() =>
                    //              {   
                    //var stream = file.GetStream();
                    //    //file.Dispose();
                    //    return stream;
                    //});
                    dietItemCurrent.Image1LocalPath = file.Path;
                    image11.Source = ImageSource.FromFile(file.Path);
					image11.IsVisible = true;
                    //stream1 = file.GetStream();
                    //dietItemCurrent.Image1 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream1);

                    break;
                case 2:
                    file2 = file;
                    //               image2.Source = ImageSource.FromStream(() =>
                    //{   
                    //var stream = file.GetStream();
                    //    //file.Dispose();
                    //    return stream;
                    //});
                    dietItemCurrent.Image2LocalPath = file.Path;
                    image22.Source = ImageSource.FromFile(file.Path);
					image22.IsVisible = true;
                    //stream2 = file.GetStream();
                    //dietItemCurrent.Image2 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream2);


                    break;
                default:

                    break;
            }


            numberOfPhoto++;


        }


		public void SetScanItems(string title){

			StackLayout stackLayout = new StackLayout();
            stackLayout.Orientation = StackOrientation.Horizontal;
            Label label1 = new Label();
            label1.Text = title;
            label1.VerticalOptions = LayoutOptions.Center;
            Button button = new Button();
            button.VerticalOptions = LayoutOptions.Center;
            button.Text = "delete";
            button.HorizontalOptions = LayoutOptions.End;
            stackLayout.Children.Add(label1);
            stackLayout.Children.Add(button);

            barItems.Children.Add(stackLayout);
            button.Clicked += (sender, e) =>
            {
                barItems.Children.Remove(stackLayout);
				items.Remove(title);
            };
		}

    }
}
