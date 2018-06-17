using System;
using System.Collections.Generic;
using HelloWorld;
using MyDiet.Manager;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;
using MyDiet.Helpers;

namespace MyDiet.Views
{
    public partial class LoginPage : ContentPage
    {
        //private SQLiteAsyncConnection _connection;
		private AccountManager accountManager;
        public LoginPage()
        {
            InitializeComponent();
			accountManager = new AccountManager();

        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
			logingIndicator.IsRunning = true;
			logingIndicator.IsVisible = true;
            
			try{
				var currentAccount = await accountManager.GetAccountInfosAsync(emailEntry.Text);
                if (currentAccount != null)
                {
                    
					if (passwordEntry.Text == currentAccount.Password)
                    {

						App.account = currentAccount;
						App.email = currentAccount.Email;
						Settings.LogStateSettings = true;
						Settings.AccountEmail = currentAccount.Email;
                        Navigation.InsertPageBefore(new MainPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
						loginFailed("email not exist or password wrong");

                    }

                }
                else
                {
					loginFailed("email not exist or password wrong");
                   
                }

			}
			catch{
				loginFailed("something wrong:like no network available");
			}
			      


        }
        


        void OnForgotButtonClicked(object sender, System.EventArgs e)
        {

        }

        void loginFailed(string message){
			logingIndicator.IsRunning = false;
            logingIndicator.IsVisible = false;
			messageLabel.Text = message;
			messageLabel.BackgroundColor = Color.Red;
            passwordEntry.Text = string.Empty;
        }



    }
}
