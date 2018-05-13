using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddReminderPage : ContentPage
    {
        public AddReminderPage()
        {
            InitializeComponent();
        }

		void DoneClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
