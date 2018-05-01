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

namespace MyDiet.Views
{
    public partial class DietItemPage : ContentPage
    {      
		bool isNewItem = false;
        public static DietManager dietManager { get; private set; }
        DietItem dietItemCurrent;
		private int numberOfPhoto = 0;
		private int existPhotos=-1;
		private bool temp = false;
        private String[] imagesPath = new string[3];

		//List<Stream> listStreams = new List<Stream>();
		//Stream[] streams = new Stream[3];

		Stream stream0=null;
		Stream stream1=null;
		Stream stream2=null;

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
				dietItemCurrent.Image0 = null;
				dietItemCurrent.Image1 = null;
				dietItemCurrent.Image2 = null;


            }

            else
            {
                dietItemCurrent = dietItem;
	           
            }

            BindingContext = dietItemCurrent;
			dietManager = DietManager.DefaultManager;

        }
        
		async protected override void OnAppearing()
		{
			if (temp)
			{
				if (dietItemCurrent.Image0 != null)
				{
					activityIndicator.IsRunning = true;
					imagesStack.IsVisible = true;
					numberOfPhoto = 1;
					var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image0);
					image0.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

					if (dietItemCurrent.Image1 != null)
					{
						numberOfPhoto = 2;
						imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image1);
						image1.Source = ImageSource.FromStream(() => new MemoryStream(imageData));
						if (dietItemCurrent.Image2 != null)
						{
							numberOfPhoto = 3;
							imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image2);
							image2.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

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
			deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = false;

			//imagesPath[0]=await AzureStorage.UploadFileAsync(ContainerType.Image, stream);

			//if(numberOfPhoto>0 && existPhotos<=0){
   // 				dietItemCurrent.Image0 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream0);

   // 			}	
			//if (numberOfPhoto>1&& existPhotos <= 1)
    				
			//if (numberOfPhoto>2&& existPhotos <= 2)
    				
		              
			dietItemCurrent.SetTime();

			await dietManager.SaveTaskAsync(dietItemCurrent,isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
			var response =await DisplayAlert("Warning", "Are you sure to delete it?", "yes", "no");
			if(response){
				await dietManager.DeleteTaskAsync(dietItemCurrent);
                await Navigation.PopAsync();
			}

        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
			var response =await DisplayAlert("Warning", "Are you sure to cancel?", "yes", "no");
            if(response){
				await Navigation.PopAsync();
            }
           
        }

       


        async void takePhotoClicked(object sender, System.EventArgs e)
        {
            if (numberOfPhoto == 3)
            {
                await DisplayAlert("Notice!", ":( at most three photos.", "OK");
                return;
            }
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }


            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
				PhotoSize = PhotoSize.Small,
				CompressionQuality = 92,
				SaveToAlbum = true,
               

            });

            if (file == null)
                return;

			//await DisplayAlert("File Location", file.AlbumPath, "OK");

			SetStream(file);

           
        }

		async void pickPhotoClicked(object sender, System.EventArgs e)
        {
            if (numberOfPhoto == 3)
            {
                await DisplayAlert("Notice!", ":( at most three photos.", "OK");
                return;
            }
            await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not available", ":( pick photo is not available.", "OK");
                return;
            }
            var photos = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                
            });
			if (photos == null)
				return;
			SetStream(photos);
            

        }

        async void scanClicked(object sender, System.EventArgs e)
        {
            //MobileBarcodeScanner.Initialize(Application);

#if __ANDROID__
    // Initialize the scanner first so it can track the current context
            MobileBarcodeScanner.Initialize (Application);
#endif
            // Initialize the scanner first so it can track the current context
            //if (Device.RuntimePlatform == Device.Android)
            //        {
            //ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            //}

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            try
            {

                var result = await scanner.Scan();
                bar.Text = result.Text;
            }
            catch
            {
                bar.Text = "no result";
            }


        }

		async public void SetStream(MediaFile file){
			activityIndicator.IsRunning = true;
            deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
			imagesStack.IsVisible = true;
			switch (numberOfPhoto)
            {
                case 0:
                    image0.Source = ImageSource.FromStream(() =>
                    {                  
						return file.GetStream();
                    });
					stream0 = file.GetStream();
					dietItemCurrent.Image0 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream0);
					file.Dispose();
                    break;
                case 1:
                    image1.Source = ImageSource.FromStream(() =>
                    {
                       
						return file.GetStream();;
                    });
					stream1 = file.GetStream();
					dietItemCurrent.Image1 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream1);
					file.Dispose();
                    break;
                case 2:
                    image2.Source = ImageSource.FromStream(() =>
                    {
                        
						return file.GetStream();
                    });
					stream2 = file.GetStream();
					dietItemCurrent.Image2 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream2);
					file.Dispose();
                    break;
                default:

                    break;
            }
            numberOfPhoto++;
			bar.Text = numberOfPhoto.ToString();
			activityIndicator.IsRunning = false;
			deleteButton.IsEnabled = true;
			cancelButton.IsEnabled = true;

		}


		void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
			bar.Text = "first image is focused";
        }


    }
}
