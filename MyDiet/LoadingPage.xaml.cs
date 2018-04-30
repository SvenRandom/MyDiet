using System;
using System.Collections.Generic;
using MyDiet.Views;
using SQLite;
using Xamarin.Forms;

namespace MyDiet
{
    public partial class LoadingPage : ContentPage
    {
		//private SQLiteAsyncConnection _connection;

        public LoadingPage()
        {
            InitializeComponent();
			//var app = Application.Current as App;
			//if (Application.Current.Properties.ContainsKey("log"))
			//	isLoggedIn.Text = Application.Current.Properties["log"].ToString();
			//else{
			//	Application.Current.Properties["log"] = false;
			//	//isLoggedIn.Text = Application.Current.Properties["log"].ToString();

			//}
				
				
        }

		void Handle_Clicked(object sender, System.EventArgs e)
        {
           

			if (App.IsUserLoggedIn)
            {
                Navigation.PushModalAsync(new NavigationPage(new MainPage()));
				//App.account;
            }
            else
            {

                Navigation.PushModalAsync(new NavigationPage(new LoginPage()));

            }

        }

    }
}
