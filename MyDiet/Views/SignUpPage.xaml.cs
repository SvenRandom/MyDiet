using System;
using System.Collections.Generic;
using System.Linq;
using HelloWorld;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class SignUpPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public SignUpPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var signUpSucceeded = AreDetailsValid();

            // Sign up logic goes here
            if (signUpSucceeded)
            {
                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

                await _connection.CreateTableAsync<User>();


                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.user = new User()
                    {
                        Username = usernameEntry.Text,
                        Password = passwordEntry.Text,
                        Email = emailEntry.Text,
                        Gender = genderPicker.Items[genderPicker.SelectedIndex],
                        DateOfBirth = dateOfBirthPicker.Date,
                        Height = Convert.ToInt32(heightEntry.Text),
                        Weight = Convert.ToInt32(weightEntry.Text)

                    };
                    await _connection.InsertAsync(App.user);

                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                messageLabel.Text = "Sign up failed! \nPlease enter valid email address and fill all";
				messageLabel.BackgroundColor = Color.Red;
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
                    int.TryParse(weightEntry.Text, out i));
        }
    }
}
