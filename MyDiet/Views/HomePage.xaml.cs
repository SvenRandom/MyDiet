using System;

using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
			//currentUser.Text = App.account.Email;
			date.Text = DateTime.Now.ToString("dd MMM yyyy dddd");
        }
    }
}
