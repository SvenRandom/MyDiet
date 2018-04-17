using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            currentUser.Text = App.user.Username;
        }
    }
}
