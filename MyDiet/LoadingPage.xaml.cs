using System;
using System.Collections.Generic;
using MyDiet.Views;
using MyDiet.Helpers;
using SQLite;
using Xamarin.Forms;
using MyDiet.Manager;

namespace MyDiet
{
    public partial class LoadingPage : ContentPage
    {
		//private SQLiteAsyncConnection _connection;
		private AccountManager accountManager;

        public LoadingPage()
        {
            InitializeComponent();
			accountManager = new AccountManager();
			//var app = Application.Current as App;
			//if (Application.Current.Properties.ContainsKey("log"))
			//	isLoggedIn.Text = Application.Current.Properties["log"].ToString();
			//else{
			//	Application.Current.Properties["log"] = false;
			//	//isLoggedIn.Text = Application.Current.Properties["log"].ToString();

			//}
				
				
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
        {
           

			if (Settings.LogStateSettings)
            {
				var account = await accountManager.GetAccountInfosAsync(Settings.AccountSettings);
				App.account = account;
				await Navigation.PushModalAsync(new NavigationPage(new MainPage()));

				//App.account;
            }
            else
            {

				await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));

            }

        }

    }
}
