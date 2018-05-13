using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddMedicinePage : ContentPage
    {
        public AddMedicinePage()
        {
            InitializeComponent();
        }

		void DoneClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
    }
}
