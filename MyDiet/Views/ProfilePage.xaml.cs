using System;
using System.Collections.Generic;
using HelloWorld;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class ProfilePage : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public ProfilePage()
        {
            InitializeComponent();

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            App.IsUserLoggedIn = false;
           
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            await _connection.CreateTableAsync<User>();

            await _connection.DeleteAsync(App.user);


            App.user = new User();

            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
    }
}
