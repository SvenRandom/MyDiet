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
            //get user info from SQlite 
   //         _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
			//await _connection.CreateTableAsync<AccountInfo>();

            //var currentUser1 = from s in _connection.Table<User>()
                        //                           where s.Email.Equals(emailEntry.Text) 
                        //select s;

            //var currentUser2 = _connection.QueryAsync<User>("SELECT * FROM Items WHERE Email = ?", emailEntry.Text);
            //get the number of result
     //       int temp = await currentUser1.CountAsync();

     //       if(temp!=0){
     //           var currentUser = await currentUser1.FirstAsync();

     //           var isValid = AreCredentialsCorrect(currentUser);
     //           if (isValid)
     //           {
     //               App.user = currentUser;
     //               App.IsUserLoggedIn = true;
					//var app = Application.Current as App;
					//Application.Current.Properties["log"] = true;
					//app.IsLoggedIn = true;
					//app.CurrentUser = currentUser;
            //        Navigation.InsertPageBefore(new MainPage(), this);
            //        await Navigation.PopAsync();
            //    }
            //    else
            //    {
            //        loginFailed();
            //    }
            //}else{
            //    loginFailed();
            //}
			try{
				var currentAccount = await accountManager.GetAccountInfosAsync(emailEntry.Text);
                if (currentAccount != null)
                {
                    
					if (passwordEntry.Text == currentAccount.Password)
                    {

						App.account = currentAccount;
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
