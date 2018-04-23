using System;
using System.Collections.Generic;
using HelloWorld;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            await _connection.CreateTableAsync<User>();

            var currentUser1 = from s in _connection.Table<User>()
                                                   where s.Email.Equals(emailEntry.Text) 
                        select s;

            var currentUser2 = _connection.QueryAsync<User>("SELECT * FROM Items WHERE Email = ?", emailEntry.Text);

            int temp = await currentUser1.CountAsync();

            if(temp!=0){
                var currentUser = await currentUser1.FirstAsync();

                var isValid = AreCredentialsCorrect(currentUser);
                if (isValid)
                {
                    App.user = currentUser;
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    loginFailed();
                }
            }else{
                loginFailed();
            }
        }

        bool AreCredentialsCorrect(User user)
        {
            return user.Password == passwordEntry.Text;
        }


        void OnForgotButtonClicked(object sender, System.EventArgs e)
        {

        }
        void loginFailed(){
            messageLabel.Text = "email not exist or password wrong";
			messageLabel.BackgroundColor = Color.Red;
            passwordEntry.Text = string.Empty;
        }



    }
}
