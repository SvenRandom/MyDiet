using System;
using System.Collections.Generic;
using System.Linq;
using HelloWorld;
using MyDiet.Manager;
using MyDiet.Models;
using MyDiet.Helpers;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class SignUpPage : ContentPage
    {
  //      private SQLiteAsyncConnection _connection;
		private AccountManager accountManager;
		private AccountInfo account;
		bool isNew = true;

        public SignUpPage()
        {
            InitializeComponent();
			accountManager = new AccountManager();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var signUpSucceeded = AreDetailsValid();

            // Sign up logic goes here
            if (signUpSucceeded)
            {
                //_connection = DependencyService.Get<ISQLiteDb>().GetConnection();

                //await _connection.CreateTableAsync<User>();
				signingIndicator.IsRunning = true;
				signingIndicator.IsVisible = true;
				account = new AccountInfo
                {
                    Id = emailEntry.Text,
                    Email = emailEntry.Text,
                    Username = usernameEntry.Text,
                    Password = passwordEntry.Text,
                    Gender = genderPicker.Items[genderPicker.SelectedIndex],
                    DateOfBirth = dateOfBirthPicker.Date,
                    Height = Convert.ToInt32(heightEntry.Text),
                    Weight = Convert.ToInt32(weightEntry.Text),
					TypeOfCuisine = typeOfCuisineEntry.Text

                };
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
					//App.user = new User()
					//{
					//    Username = usernameEntry.Text,
					//    Password = passwordEntry.Text,
					//    Email = emailEntry.Text,
					//    Gender = genderPicker.Items[genderPicker.SelectedIndex],
					//    DateOfBirth = dateOfBirthPicker.Date,
					//    Height = Convert.ToInt32(heightEntry.Text),
					//    Weight = Convert.ToInt32(weightEntry.Text)

					//};


					try{
					    await accountManager.SaveTaskAsync(account, isNew);
                        //await _connection.InsertAsync(App.user);
						Settings.LogStateSettings = true;
						Settings.AccountEmail = account.Email;

						App.account = account;
                        Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                        await Navigation.PopToRootAsync();
					}
					catch{
						SignUpFailed("Sign up failed! \nemail address already exist!");
                        
					}
     
                }
            }
            else
            {
				SignUpFailed("Sign up failed! \nPlease enter valid email address and fill all");

            }

        }

        bool AreDetailsValid()
        {
            
            return (!string.IsNullOrEmpty(usernameEntry.Text) &&
                    !string.IsNullOrWhiteSpace(passwordEntry.Text) &&
                    !string.IsNullOrWhiteSpace(emailEntry.Text) &&
                    emailEntry.Text.Contains("@") &&
                    !string.IsNullOrEmpty(genderPicker.Items[genderPicker.SelectedIndex]) &&
                    int.TryParse(heightEntry.Text, out int i) &&
                    int.TryParse(weightEntry.Text, out i) &&
			        !string.IsNullOrWhiteSpace(typeOfCuisineEntry.Text));
        }

		void SignUpFailed(string message)
        {
			signingIndicator.IsRunning = false;
			signingIndicator.IsVisible = false;
            messageLabel.Text = message;
            messageLabel.BackgroundColor = Color.Red;
            
        }


    }
}
