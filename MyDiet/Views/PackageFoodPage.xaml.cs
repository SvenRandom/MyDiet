using System;
using System.Collections.Generic;
using MyDiet.Manager;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class PackageFoodPage : ContentPage
    {
        public PackageFoodPage()
        {
            InitializeComponent();
        }
        
		public PackageFoodPage(string bar)
        {
            InitializeComponent();
			SetFood(bar);

        }

		public async void SetFood(string bar)
		{
			try
            {
                PackageFoodDatabaseManager packageFoodDatabaseManager = new PackageFoodDatabaseManager();
                var temp = await packageFoodDatabaseManager.GetFoodAsync(bar);
                if (temp != null)
                {
					BindingContext = temp;
                }
                else
                {
                    await DisplayAlert("Sorry!", "Something wrong with our database", "Fine");
                }

            }
            catch
            {
                await DisplayAlert("notice", "NetWork Error", "Sure");
            }
		}

    }
}
