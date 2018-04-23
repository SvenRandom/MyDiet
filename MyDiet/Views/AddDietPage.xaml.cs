using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HelloWorld;
using MyDiet.Models;
using Plugin.Media;
using SQLite;
using Xamarin.Forms;


namespace MyDiet.Views
{
    public partial class AddDietPage : ContentPage
    {


        private SQLiteAsyncConnection _connection;
        private int numberOfPhoto = 0;
        private String[] imagesPath = new string[3];

      



        public AddDietPage()
        {
            InitializeComponent();
            timePicker.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);

        }


        async public void OnDoneClicked(object sender, EventArgs e)
        {


            var addSucceed = IsAddValid();
            if (addSucceed)
            {

                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

                await _connection.CreateTableAsync<DietRecord>();

                //var dietRecords = await _connection.Table<DietRecord>().ToListAsync();
                //_dietRecords = new ObservableCollection<DietRecord>(dietRecords);

                //dietRecordListView.ItemsSource = _dietRecords;


                var newDiet = new DietRecord
                {
                    Description = descriptionEditor.Text,
                    Calories = Convert.ToInt32(calEntry.Text),
                    Image = imagesPath[0] + ";" + imagesPath[1] + ";" + imagesPath[2],
                    Date = datePicker.Date,
                    Time = timePicker.Time,
                    UserId = App.user.UserId
                };

                newDiet.SetTime();

                await _connection.InsertAsync(newDiet);

                //_dietRecords.Add(newDiet);


                await Navigation.PopToRootAsync();

            }
            else
            {
                message.Text = "please enter description and enter numbers in calories";
                message.BackgroundColor = Color.Red;

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

                Name = "meal.jpg"

            });

            if (file == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");

            //var photos = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions{});

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


        bool IsAddValid()
        {

            return (!string.IsNullOrEmpty(descriptionEditor.Text) &&
                    (int.TryParse(calEntry.Text, out int i) || string.IsNullOrEmpty(calEntry.Text)));
        }
    }
}
