

using System;
using System.Collections.Generic;
using HelloWorld;
using MyDiet.Models;
using MyDiet.Helpers;
using SQLite;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using MyDiet.Manager;

namespace MyDiet.Views
{
    public partial class ProfilePage : ContentPage
    {
 
		

        //private SQLiteAsyncConnection _connection;

        public ProfilePage()
        {
			InitializeComponent();


        }

		async void LogOutClicked(object sender, System.EventArgs e)
        {
            
			Settings.LogStateSettings = false;
			Settings.AccountEmail = String.Empty;
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

		async void OnModifyTapped(object sender, System.EventArgs e)
        {
		         
			await Navigation.PushAsync(new ModifyPersonalInfoPage());
        }

		async void OnPasswordTapped(object sender, System.EventArgs e)
        {
			await Navigation.PushAsync(new ChangePasswordPage());
        }


		void DataSourceTapped(object sender, System.EventArgs e)
        {
           
        }

		void AboutTapped(object sender, System.EventArgs e)
        {

        }


		async void SendNotiClicked(object sender, System.EventArgs e)
        {
			//CrossLocalNotifications.Current.Show("Medicine Notification", "Now: "+DateTime.Now+" is time to take pill" , 1);
			//CrossLocalNotifications.Current.Show("Medicine Notification", 
			//"10 second later is time to take pill", 1, 
			//DateTime.Now.AddSeconds(10));
			//MedicineDatabase medicineDatabase = new MedicineDatabase
			//{
			//	Id="9311770597111",
			//	MedicineName="Swisse Liver Detox",
			//	Description="Swisse Ultiboost Liver Detox is a premium quality formula containing herbs traditionally " +
			//		"used to help support liver function and provide relief from indigestion and bloating.",
			//	Directions="Two tablets daily, during or immediately after a meal, or as directed by a healthcare professional.",
			//	Duration="30",
			//	TimesPerDay=1,
			//	Unit="Two tablets"
			//};

			//MedicineDatabaseManager medicineDatabaseManager = new MedicineDatabaseManager();
			//await medicineDatabaseManager.SaveTaskAsync(medicineDatabase, true);
			PackageFoodDatabase medicineDatabase = new PackageFoodDatabase
            {
                Id="9310761130269",
				Title="Cheese Burger",
				Weight="161g",
				Energy="1090kJ",
				Description = "Tihs product contains gluten(wheat), milk, soy&sesame. May contain peanuts, and tree nuts duo to shared equipment.",
				Protein="13.1g",
				Fat="10.7g",
				Satfat = "4.5g",
				Carbs="27.6g",
				Sugar ="4.0g",
				Sodium="357mg"
              
            };

			PackageFoodDatabaseManager packageFoodDatabaseManager  = new PackageFoodDatabaseManager();
			await packageFoodDatabaseManager.SaveTaskAsync(medicineDatabase, true);
			await DisplayAlert("ok", "creat succeed", "ok");
			PackageFoodDatabase Database = new PackageFoodDatabase
            {
                Id = "7622300689582",
                Title = "belVita Breakfast biscuits",
				Description = "belVita Breakfast biscuits are a convenient way to include quality wholegrains as parts of a delicious" +
					"breakfast for when you have a lot on your plate.",
                Weight = "50g",
                Energy = "1830kJ",
                Protein = "7.5g",
                Fat = "14.1g",
                Satfat = "1.4g",
                Carbs = "65.3g",
                Sugar = "22.2g",
				Fibre="8.9g",
                Sodium = "118mg"

            };

			await packageFoodDatabaseManager.SaveTaskAsync(Database, true);
            await DisplayAlert("ok", "creat succeed", "ok");
        }

             


    }
}
