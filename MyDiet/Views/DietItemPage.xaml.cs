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
		MediaFile file0;
		MediaFile file1;
		MediaFile file2;
        private String[] imagesPath = new string[3];

		//List<Stream> listStreams = new List<Stream>();
		//Stream[] streams = new Stream[3];

		//Stream stream0=null;
		//Stream stream1=null;
		//Stream stream2=null;

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


            }

            else
            {
                dietItemCurrent = dietItem;
	           
            }
			if (dietItemCurrent.Image0LocalPath != null){
				imagesStack.IsVisible = true;

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

						image00.Source = fileSoure;

						//var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image0UploadId);
                        //image00.Source = ImageSource.FromStream(() => new MemoryStream(imageData));


					//var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image0UploadId);
					//image0.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

					if (dietItemCurrent.Image1LocalPath != null)
					{
						numberOfPhoto = 2;
						image11.Source = ImageSource.FromFile(dietItemCurrent.Image1LocalPath);
						//imageData = await AzureStorage.GetFileAsync(ContainerType.Image, dietItemCurrent.Image1UploadId);
						//image1.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

						if (dietItemCurrent.Image2LocalPath != null)
						{
							numberOfPhoto = 3;
							image22.Source = ImageSource.FromFile(dietItemCurrent.Image2LocalPath);
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
			deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
            
			//imagesPath[0]=await AzureStorage.UploadFileAsync(ContainerType.Image, stream);

			if(numberOfPhoto>0 && existPhotos<=0){
				dietItemCurrent.Image0UploadId = await AzureStorage.UploadFileAsync(ContainerType.Image, file0.GetStream());
                
    			}	
			if (numberOfPhoto>1&& existPhotos <= 1)
				dietItemCurrent.Image1UploadId = await AzureStorage.UploadFileAsync(ContainerType.Image, file1.GetStream());

			if (numberOfPhoto>2&& existPhotos <= 2)
				dietItemCurrent.Image2UploadId = await AzureStorage.UploadFileAsync(ContainerType.Image, file2.GetStream());

			//var uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Image, file1.GetStream());
			//await DisplayAlert("File ID", uploadedFilename, "OK");
			dietItemCurrent.SetTime();
			dietItemCurrent.SetDateToDisplay();
			//file0.Dispose();
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
            if (numberOfPhoto >= 3)
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
				
				SaveToAlbum = true,
				PhotoSize = PhotoSize.Small,
                CompressionQuality = 92,
                
				AllowCropping = true,

               

            });

            if (file == null)
                return;

			//await DisplayAlert("File Location", file.AlbumPath, "OK");

			SetStream(file);

           
        }

		async void pickPhotoClicked(object sender, System.EventArgs e)
        {
            if (numberOfPhoto >= 3)
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
				PhotoSize = PhotoSize.Small,
                CompressionQuality = 92,
                
            });
			//image.Source = ImageSource.FromFile(photos.Path);
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

		public void SetStream(MediaFile file){
			activityIndicator.IsRunning = true;
            deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
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
					//stream2 = file.GetStream();
					//dietItemCurrent.Image2 = await AzureStorage.UploadFileAsync(ContainerType.Image, stream2);

					               
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
