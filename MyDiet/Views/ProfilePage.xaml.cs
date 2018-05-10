
using System;
using System.Collections.Generic;
using HelloWorld;
using MyDiet.Models;
using MyDiet.Helpers;
using SQLite;
using Xamarin.Forms;

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

    }
}
