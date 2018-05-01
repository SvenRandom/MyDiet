using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using MyDiet.Helpers;
using Xamarin.Forms;
using Plugin.Media;
using ZXing.Mobile;
using Plugin.Media.Abstractions;

namespace MyDiet.Views
{
    public partial class DietItemPage : ContentPage
    {      

		bool isNewItem = false;
        public static DietManager dietManager { get; private set; }
        DietItem dietItemCurrent;
		private int numberOfPhoto = 0;
        private String[] imagesPath = new string[3];

        public DietItemPage(DietItem dietItem)
        {
            InitializeComponent();
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


            }

            else
            {
                dietItemCurrent = dietItem;

            }

            BindingContext = dietItemCurrent;
			dietManager = DietManager.DefaultManager;

        }


        async void OnSaveActivated(object sender, EventArgs e)
        {
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
				PhotoSize = PhotoSize.Medium,
				CompressionQuality = 92,
				SaveToAlbum = true,
                Name = "meal.jpg"

            });

            if (file == null)
                return;

			//await DisplayAlert("File Location", file.AlbumPath, "OK");

           

            switch (numberOfPhoto)
            {
                case 0:
                    image1.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                    imagesPath[0] = file.Path;
                    break;
                case 1:
                    image2.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                    imagesPath[1] = file.Path;
                    break;
                case 2:
                    image3.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                    imagesPath[2] = file.Path;
                    break;
                default:

                    break;
            }
            numberOfPhoto++;
			imagesStack.IsVisible = true;
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

            switch (numberOfPhoto)
            {
                case 0:
                    image1.Source = ImageSource.FromStream(() =>
                    {
						var stream = photos.GetStream();
                        return stream;
                    });
					imagesPath[0] = photos.Path;
                    break;
                case 1:
                    image2.Source = ImageSource.FromStream(() =>
                    {
						var stream = photos.GetStream();
                        return stream;
                    });
					imagesPath[1] = photos.Path;
                    break;
                case 2:
                    image3.Source = ImageSource.FromStream(() =>
                    {
						var stream = photos.GetStream();
                        return stream;
                    });
					imagesPath[2] = photos.Path;
                    break;
                default:

                    break;
            }
            numberOfPhoto++;
			imagesStack.IsVisible = true;

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

         
    }
}
